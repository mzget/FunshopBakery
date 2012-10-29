using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BakeryShop : Mz_BaseScene {

	public GameObject bakeryShop_backgroup_group;
	
	//<!-- in game button.
	public GameObject close_button;
	
	//<!-- Miscellaneous game objects.
	public BinBeh bin_behavior_obj;
	public GameObject foodsTray_obj;
	public FoodTrayBeh foodTrayBeh;
    public GameObject calculator_group_instance;
	private Mz_CalculatorBeh calculatorBeh;
    private GameObject cash_obj;
	private tk2dSprite cash_sprite;
    private tk2dTextMesh coin_Textmesh;
    private GameObject packaging_Obj;
    //<!-- Core data
    public enum GamePlayState { none = 0, calculationPrice, receiveMoney, giveTheChange, TradeComplete, };
    public GamePlayState currentGamePlayState;
    public ShopScene_GUIManager gui_manager;
    public GoodDataStore goodDataStore;
    public static List<int> NumberOfCansellItem = new List<int>();
    public List<Goods> CanSellGoodLists = new List<Goods>();

    #region <!-- SouseMachine data fields group. 

//    public GameObject juiceTank_base_Obj;
	public tk2dSprite juiceTank_base_Sprite;
//	public GameObject pineAppple_tank_Obj;
	public GameObject appleTank_Obj;
	public GameObject orangeTank_Obj;
	public GameObject cocoaMilkTank_Obj;
	public GameObject freshMilkTank_Obj;

    #endregion
	
	#region <!-- Icecream data fields group.
	
	const string NameOfBaseTankIcecream_001 = "icecream_lv_1";
	const string NameOfBaseTankIcecream_002 = "icecream_lv_2";
	const string NameOfBaseTankIcecream_003 = "icecream_lv_3";
	public const string icecreamStrawberryTank_name = "Strawberry_machine";
	public const string icecreamVanillaTank_name = "Vanilla_machine";
	public const string icecreamChocolateTank_name = "Chocolate_machine";
	public tk2dSprite icecreamTankBase_Sprite;	
	public GameObject icecreamStrawberryTank_obj;
	public GameObject icecreamVanillaTank_obj;
	public GameObject icecreamChocolateTank_obj;
	
	#endregion

    #region <!-- Toast && Jam Obj group;

    public Transform toastObj_transform_group;
	public ToastBeh[] toasts = new ToastBeh[2]; 
	private Vector3 toast_1_pos = new Vector3(-0.415f, 0.419f, -1);
	private Vector3 toast_2_pos = new Vector3(-0.220f, 0.418f, -1);
    public JamBeh strawberryJam_instance;
    public GameObject blueberryJam_instance;
    public GameObject freshButterJam_instance;
    public GameObject custardJam_instance;
	
	#endregion
	
	#region <!-- Cakes object data. 
	
	public Transform cupcakeBase_transform;
	public Transform miniCakeBase_transform;
	public Transform cakeBase_transform;
	public CakeBeh cupcake;
	public CakeBeh miniCake;
	public CakeBeh cake;
    public GameObject chocolate_cream_Instance;
    public GameObject blueberry_cream_Instance;
    public GameObject strawberry_cream_Instance;
	
	#endregion

    #region <!-- Sandwich && Cookie data fields group.

    public Transform sandwichCookieTray_Transform;
    public SandwichBeh tunaSandwich;
    public SandwichBeh deepFriedChickenSandwich;
    public SandwichBeh hamSandwich;
    public SandwichBeh eggSandwich;
    public CookieBeh chocolateChip_cookie;
    public CookieBeh fruit_cookie;
    public CookieBeh butter_cookie;

    #endregion

    /// <summary>
    /// Hotdog data fields group.
    /// </summary>
    public Transform hotdogTray_transform;
    public HotdogBeh hotdog;

    #region <!-- Customer data group.

    public GameObject customerMenu_group_Obj;
//    public GameObject customerSprite_Instance;
    public CustomerBeh currentCustomer;  
	
    public event EventHandler nullCustomer_event;
    private void OnNullCustomer_event(EventArgs e) {
        if(nullCustomer_event != null) {
            nullCustomer_event(this, e);
		
			Debug.Log("Callback :: nullCustomer_event");
        }
    }
	
	#endregion	

	
	// Use this for initialization
	IEnumerator Start () {
        base.InitializeAudio();
//		Mz_ResizeScale.ResizingScale(bakeryShop_backgroup_group.transform);]
		
		appleTank_Obj.SetActiveRecursively(false);
		orangeTank_Obj.SetActiveRecursively(false);
		cocoaMilkTank_Obj.SetActiveRecursively(false);
		freshMilkTank_Obj.SetActiveRecursively(false);

		blueberryJam_instance.SetActiveRecursively(false);
		freshButterJam_instance.SetActiveRecursively(false);
		custardJam_instance.SetActiveRecursively(false);

        StartCoroutine(CreateToastInstance());
        StartCoroutine(CreateCupcakeInstance());
        StartCoroutine(InitializeMinicakeInstance());
        StartCoroutine(InitializeCakeInstance());
        StartCoroutine(InitializeTunaSandwichInstance());
		
		icecreamVanillaTank_obj.SetActiveRecursively(false);
		icecreamChocolateTank_obj.SetActiveRecursively(false);

		this.CreateDeepFriedChickenSandwich();
		this.CreateHamSanwich();
		this.CreateEggSandwich();
        deepFriedChickenSandwich.gameObject.SetActiveRecursively(false);
        hamSandwich.gameObject.SetActiveRecursively(false);
        eggSandwich.gameObject.SetActiveRecursively(false);

        StartCoroutine(InitializeChocolateChipCookie());
		this.CreateFruitCookie();
		this.CreateButterCookie();
        fruit_cookie.gameObject.SetActiveRecursively(false);
        butter_cookie.gameObject.SetActiveRecursively(false);

        StartCoroutine(InitializeHotdogInstance());

        yield return null;

        calculator_group_instance.SetActiveRecursively(false);

		foodTrayBeh = new FoodTrayBeh();
        goodDataStore = new GoodDataStore();
        // Debug can sell list.
        InitializeCanSellGoodslist();
		Debug.Log("CanSellGoodLists.Count : " + CanSellGoodLists.Count);
		Debug.Log("NumberOfCansellItem.Count : " + NumberOfCansellItem.Count);
		
		this.gameObject.AddComponent<ShopScene_GUIManager>();
		gui_manager = this.gameObject.GetComponent<ShopScene_GUIManager>();

        var coinObj = GameObject.Find("Coin");
        coin_Textmesh = coinObj.GetComponent<tk2dTextMesh>();
        coin_Textmesh.text = StorageManage.Money.ToString();
        coin_Textmesh.Commit();
        
        nullCustomer_event += new EventHandler(BakeryShop_nullCustomer_event);
        OnNullCustomer_event(EventArgs.Empty);
	}

    private IEnumerator InitializeMinicakeInstance()
    {
        yield return StartCoroutine(CreateMiniCakeInstance());	
		miniCake.gameObject.active = false;
    }
    private IEnumerator InitializeCakeInstance()
    {
		yield return StartCoroutine(CreateCakeInstance());			
		cake.gameObject.active = false;
    }
    private IEnumerator InitializeTunaSandwichInstance()
    {
        yield return StartCoroutine(this.CreateTunaSandwich());
        tunaSandwich.gameObject.SetActiveRecursively(false);
    }
    private IEnumerator InitializeChocolateChipCookie()
    {
        yield return StartCoroutine(this.CreateChocolateChip_Cookie());
        chocolateChip_cookie.gameObject.SetActiveRecursively(false);
    }    
    private IEnumerator InitializeHotdogInstance()
    {
        yield return StartCoroutine(this.CreateHotdog());
        hotdog.gameObject.SetActiveRecursively(false);
    }

    private void InitializeCanSellGoodslist()
    {
        foreach (int id in NumberOfCansellItem)
        {
            CanSellGoodLists.Add(goodDataStore.Menu_list[id]);
			
			#region Has page1 upgraded.
			
            if (id == 6)
                blueberryJam_instance.active = true;
            if(id == 10)
                blueberry_cream_Instance.active = true;
            if (id == 12 || id == 13)
                miniCake.gameObject.active = true;
			if(id == 15 || id == 16)
				cake.gameObject.active = true;
			if(id == 19) {
                icecreamTankBase_Sprite.spriteId = icecreamTankBase_Sprite.GetSpriteIdByName(NameOfBaseTankIcecream_002);
				icecreamVanillaTank_obj.SetActiveRecursively(true);
                //break;
			}	
			if(id == 21)
                tunaSandwich.gameObject.SetActiveRecursively(true);
            if (id == 25)
                chocolateChip_cookie.gameObject.SetActiveRecursively(true);
            if (id == 28)
                hotdog.gameObject.SetActiveRecursively(true);
			
			#endregion
			
			#region Has page2 upgraded.
			
			if(id == 1) {
				appleTank_Obj.SetActiveRecursively(true);
				juiceTank_base_Sprite.spriteId = juiceTank_base_Sprite.GetSpriteIdByName("juiceTank_lv_2");
			}
            if(id == 2)
				cocoaMilkTank_Obj.SetActiveRecursively(true);
			if(id == 7)
				freshButterJam_instance.SetActiveRecursively(true);
			if(id == 11 || id == 14 || id == 17)
				strawberry_cream_Instance.SetActiveRecursively(true);
			if(id == 20) {
				icecreamTankBase_Sprite.spriteId = icecreamTankBase_Sprite.GetSpriteIdByName(NameOfBaseTankIcecream_003);
				icecreamChocolateTank_obj.SetActiveRecursively(true);
			}
			if(id == 22)
				deepFriedChickenSandwich.gameObject.SetActiveRecursively(true);
			if(id == 26)
				fruit_cookie.gameObject.SetActiveRecursively(true);
			if(id == 3)
				orangeTank_Obj.SetActiveRecursively(true);
			
			#endregion
			
			#region Has Page 3 Upgraded.

			if(id == 4)
				freshMilkTank_Obj.SetActiveRecursively(true);
			if(id == 8)
				custardJam_instance.SetActiveRecursively(true);
			if(id == 23)
				hamSandwich.gameObject.SetActiveRecursively(true);
			if(id == 24)
				eggSandwich.gameObject.SetActiveRecursively(true);
            if (id == 27)
                butter_cookie.gameObject.SetActiveRecursively(true);
            if (id == 29)
                hotdog.gameObject.SetActiveRecursively(true);

			#endregion
        }

        foreach (Goods item in CanSellGoodLists) {
            Debug.Log("CanSellGoodLists :: " + item.name);
        }
    }
	
	#region <!-- Cake gameobject mechanism section.
	
	IEnumerator CreateCupcakeInstance() {		
		yield return new WaitForFixedUpdate();
		
		if(cupcake == null) {
			GameObject temp_cupcake = Instantiate(Resources.Load(ObjectsBeh.Cakes_ResourcePath + CakeBeh.Cupcake, typeof(GameObject))) as GameObject;
			temp_cupcake.transform.parent = cupcakeBase_transform;
			temp_cupcake.transform.localPosition = new Vector3(0, -.02f, -.1f);
			temp_cupcake.name = CakeBeh.Cupcake;
			cupcake = temp_cupcake.GetComponent<CakeBeh>();
			cupcake.destroyObj_Event += HandleCupcakedestroyObj_Event;
			cupcake.putObjectOnTray_Event += Handle_CupcakeputObjectOnTray_Event;
		}
	}
    void HandleCupcakedestroyObj_Event (object sender, System.EventArgs e)
	{
		foodTrayBeh.goodsOnTray_List.Remove((GoodsBeh)sender);
		ObjectsBeh.ResetData();
		StartCoroutine(CreateCupcakeInstance());
	}
	void Handle_CupcakeputObjectOnTray_Event (object sender, System.EventArgs e)
	{
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		cupcake = null;
		
		StartCoroutine(CreateCupcakeInstance());
	}

    IEnumerator CreateMiniCakeInstance() {
		yield return new WaitForFixedUpdate();
		
		if(miniCake == null) {
			GameObject temp_Minicake = Instantiate(Resources.Load(ObjectsBeh.Cakes_ResourcePath + CakeBeh.MiniCake, typeof(GameObject))) as GameObject;
			temp_Minicake.transform.parent = miniCakeBase_transform;
			temp_Minicake.transform.localPosition = new Vector3(-0.01f, 0.11f, -0.1f);
			temp_Minicake.name = CakeBeh.MiniCake;

			miniCake = temp_Minicake.GetComponent<CakeBeh>();
			miniCake.destroyObj_Event += new EventHandler(miniCake_destroyObj_Event);
            miniCake.putObjectOnTray_Event += new EventHandler(miniCake_putObjectOnTray_Event);
		}
    }
    void miniCake_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();
        StartCoroutine(CreateMiniCakeInstance());
    }
    void miniCake_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		miniCake = null;
		
		StartCoroutine(CreateMiniCakeInstance());
    }
	
    IEnumerator CreateCakeInstance() {
		yield return new WaitForFixedUpdate();
		
		if(cake == null) {
			GameObject temp_cake = Instantiate(Resources.Load(ObjectsBeh.Cakes_ResourcePath + CakeBeh.Cake, typeof(GameObject))) as GameObject;
			temp_cake.transform.parent = cakeBase_transform;
			temp_cake.transform.localPosition = new Vector3(-0.01f, 0.2f, -0.1f);
			temp_cake.name = CakeBeh.Cake;

			cake = temp_cake.GetComponent<CakeBeh>();
			cake.destroyObj_Event += new EventHandler(Cake_destroyObj_Event);
            cake.putObjectOnTray_Event += new EventHandler(Cake_putObjectOnTray_Event);
		}
    }
    void Cake_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();
        StartCoroutine(CreateCakeInstance());
    }
    void Cake_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		cake = null;
		
		StartCoroutine(CreateCakeInstance());
    }
	
	#endregion
	
	#region <!-- Toast gameobject mechanism section.
	
    private IEnumerator CreateToastInstance()
    {
		yield return new WaitForFixedUpdate();
		
        if(toasts[0] == null) {
            GameObject temp_0 = Instantiate(Resources.Load(ObjectsBeh.ToastAndJam_ResourcePath + "breadPure", typeof(GameObject))) as GameObject;
		    toasts[0] = temp_0.GetComponent<ToastBeh>();
		    toasts[0].transform.parent = toastObj_transform_group;
		    toasts[0].transform.localPosition = toast_1_pos;
            toasts[0].destroyObj_Event += new System.EventHandler(DestroyToastEvent);
            toasts[0].putObjectOnTray_Event += new System.EventHandler(PutToastOnTrayEvent);
        }
		
        if(toasts[1] == null) {
		    GameObject temp_1 = Instantiate(Resources.Load(ObjectsBeh.ToastAndJam_ResourcePath + "breadPure", typeof(GameObject))) as GameObject;
		    toasts[1] = temp_1.GetComponent<ToastBeh>();
		    toasts[1].transform.parent = toastObj_transform_group;
		    toasts[1].transform.localPosition = toast_2_pos;
            toasts[1].destroyObj_Event += new System.EventHandler(DestroyToastEvent);
            toasts[1].putObjectOnTray_Event += new System.EventHandler(PutToastOnTrayEvent);
        }
    }
	
	public void DestroyToastEvent(object sender, System.EventArgs e) {
        Debug.Log("DestroyToastEvent");
		
		ObjectsBeh.ResetData();		
        StartCoroutine(CreateToastInstance());
		
		foodTrayBeh.goodsOnTray_List.Remove((GoodsBeh)sender);
	}

    public void PutToastOnTrayEvent(object sender, System.EventArgs e) {
        Debug.Log("PutToastOnTrayEvent");

        ObjectsBeh.ResetData();
		StartCoroutine(CreateToastInstance());
		
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add((GoodsBeh)sender);
    }
	
	#endregion
	
	#region <!-- Sandwich Obj behavior.
	
	/// <summary>
	/// Creates the tuna sandwich.
	/// </summary>
	private IEnumerator CreateTunaSandwich() {
        yield return new WaitForFixedUpdate();

		if(tunaSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "TunaSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(.235f, -.15f, -.1f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.tuna_sandwich.ToString();

            tunaSandwich = sandwich.GetComponent<SandwichBeh>();
            tunaSandwich.putObjectOnTray_Event += new EventHandler(tunaSandwich_putObjectOnTray_Event);
            tunaSandwich.destroyObj_Event += new EventHandler(tunaSandwich_destroyObj_Event);
		}
	}
    void tunaSandwich_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		tunaSandwich = null;

        StartCoroutine(this.CreateTunaSandwich());
    }
    void tunaSandwich_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();      
		
        StartCoroutine(this.CreateTunaSandwich());
    }
	
	/// <summary>
	/// Creates the deep fried chicken sandwich.
	/// </summary>
	void CreateDeepFriedChickenSandwich() {
		if(deepFriedChickenSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "DeepFriedChickenSandwich", typeof(GameObject))) as GameObject;
			sandwich.transform.parent = sandwichCookieTray_Transform;
			sandwich.transform.localPosition = new Vector3(0.105f, -.16f, -.2f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.deep_fried_chicken_sandwich.ToString();
			
			deepFriedChickenSandwich = sandwich.GetComponent<SandwichBeh>();
			deepFriedChickenSandwich.putObjectOnTray_Event += Handle_DeepFriedChickenSandwich_putObjectOnTray_Event;
			deepFriedChickenSandwich.destroyObj_Event += Handle_DeepFriedChickenSandwich_destroyObj_Event;
		}
	}
	void Handle_DeepFriedChickenSandwich_putObjectOnTray_Event(object sender, EventArgs e)
	{
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		deepFriedChickenSandwich = null;

        this.CreateDeepFriedChickenSandwich();
	}
	void Handle_DeepFriedChickenSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();
		
		deepFriedChickenSandwich.putObjectOnTray_Event -= Handle_DeepFriedChickenSandwich_putObjectOnTray_Event;
		deepFriedChickenSandwich.destroyObj_Event -= Handle_DeepFriedChickenSandwich_destroyObj_Event;
		
        this.CreateDeepFriedChickenSandwich();
	}
	
	/// <summary>
	/// Creates the ham sanwich.
	/// </summary>
	void CreateHamSanwich() {
		if(hamSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "HamSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(-.015f, -.17f, -.3f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.ham_sandwich.ToString();

            hamSandwich = sandwich.GetComponent<SandwichBeh>();
            hamSandwich.putObjectOnTray_Event += Handle_HamSandwich_putObjectOnTray_Event;
            hamSandwich.destroyObj_Event += Handle_HamSandwich_destroyObj_Event;
		}
	}
	void Handle_HamSandwich_putObjectOnTray_Event (object sender, EventArgs e)
	{
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		hamSandwich = null;

        this.CreateHamSanwich();
	}
	void Handle_HamSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();
		
		hamSandwich.putObjectOnTray_Event -= Handle_HamSandwich_putObjectOnTray_Event;
		hamSandwich.destroyObj_Event -= Handle_HamSandwich_destroyObj_Event;
		
        this.CreateHamSanwich();
	}
	
	/// <summary>
	/// Creates the egg sandwich.
	/// </summary>
	void CreateEggSandwich() {		
		if(eggSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "EggSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(-.14f, -.17f, -.4f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.egg_sandwich.ToString();

            eggSandwich = sandwich.GetComponent<SandwichBeh>();
            eggSandwich.putObjectOnTray_Event += Handle_EggSandwich_putObjectOnTray_Event;
            eggSandwich.destroyObj_Event += Handle_EggSandwich_destroyObj_Event;
		}
	}
	void Handle_EggSandwich_putObjectOnTray_Event (object sender, EventArgs e)
	{
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		eggSandwich = null;

        this.CreateEggSandwich();
	}
	void Handle_EggSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();
		
		eggSandwich.putObjectOnTray_Event -= Handle_EggSandwich_putObjectOnTray_Event;
		eggSandwich.destroyObj_Event -= Handle_EggSandwich_destroyObj_Event;
		
        this.CreateEggSandwich();
	}
	
	#endregion

    #region <!-- Cookie Object Behavior.
	
	/// <summary>
	/// Creates the chocolate chip_ cookie.
	/// </summary>
    IEnumerator CreateChocolateChip_Cookie() {
        yield return new WaitForFixedUpdate();

        if(chocolateChip_cookie == null) {
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "ChocolateChip_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(-.165f, 0.1f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.chocolate_chip_cookie.ToString();

            chocolateChip_cookie = cookie.GetComponent<CookieBeh>();
            chocolateChip_cookie.putObjectOnTray_Event += new EventHandler(chocolateChip_cookie_putObjectOnTray_Event);
            chocolateChip_cookie.destroyObj_Event += new EventHandler(chocolateChip_cookie_destroyObj_Event);
        }
    }
    void chocolateChip_cookie_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		chocolateChip_cookie = null;

        StartCoroutine(this.CreateChocolateChip_Cookie());
    }
    void chocolateChip_cookie_destroyObj_Event(object sender, EventArgs e) {    
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();

        StartCoroutine(this.CreateChocolateChip_Cookie());
    }
	
    /// <summary>
    /// Create instance of fruit_cookie object and cookie behavior.
    /// </summary>
	void CreateFruitCookie() {
		if(fruit_cookie == null) {			
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "Fruit_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(0.02f, 0.1f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.fruit_cookie.ToString();

            fruit_cookie = cookie.GetComponent<CookieBeh>();
            fruit_cookie.putObjectOnTray_Event += new EventHandler(Handle_FruitCookie_putObjectOnTray_Event);
            fruit_cookie.destroyObj_Event += new EventHandler(Handle_FruitCookie_DestroyObj_Event);
		}
	}
	void Handle_FruitCookie_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		fruit_cookie = null;

        this.CreateFruitCookie();
	}
	void Handle_FruitCookie_DestroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();

        fruit_cookie.putObjectOnTray_Event -= Handle_FruitCookie_putObjectOnTray_Event;
        fruit_cookie.destroyObj_Event -= Handle_FruitCookie_DestroyObj_Event;

        this.CreateFruitCookie();
	}
    
    /// <summary>
    /// Create instance of ButterCookie object and cookie behavior.
    /// </summary>
    private void CreateButterCookie()
    {
        if(butter_cookie == null) {
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "Butter_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(.2f, 0.11f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.butter_cookie.ToString();

            butter_cookie = cookie.GetComponent<CookieBeh>();
            butter_cookie.putObjectOnTray_Event += new EventHandler(butter_cookie_putObjectOnTray_Event);
            butter_cookie.destroyObj_Event += new EventHandler(butter_cookie_destroyObj_Event);
        }
    }
    void butter_cookie_putObjectOnTray_Event(object sender, EventArgs e) {
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		butter_cookie = null;

        this.CreateButterCookie();
    }
    void butter_cookie_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();

        butter_cookie.putObjectOnTray_Event -= butter_cookie_putObjectOnTray_Event;
        butter_cookie.destroyObj_Event -= butter_cookie_destroyObj_Event;

        this.CreateButterCookie();
    }

    #endregion

    #region <!-- Hotdog object behavior.

    private IEnumerator CreateHotdog()
    {
        yield return new WaitForFixedUpdate();

        if(hotdog == null) {
            GameObject hotdog_obj = Instantiate(Resources.Load(ObjectsBeh.Hotdog_ResourcePath + "Hotdog", typeof(GameObject))) as GameObject;
            hotdog_obj.transform.parent = hotdogTray_transform;
            hotdog_obj.transform.localPosition = new Vector3(0.07f, 0.1f, -0.1f);
            hotdog_obj.gameObject.name = "Hotdog";

            hotdog = hotdog_obj.GetComponent<HotdogBeh>();
            hotdog.putObjectOnTray_Event += new EventHandler(hotdog_putObjectOnTray_Event);
            hotdog.destroyObj_Event += new EventHandler(hotdog_destroyObj_Event);
        }
    }
    void hotdog_putObjectOnTray_Event(object sender, EventArgs e) {    
        if(foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            foodTrayBeh.goodsOnTray_List.Add(sender as GoodsBeh);
		else 
			return;
		
		ObjectsBeh.ResetData();
		hotdog = null;

        StartCoroutine(this.CreateHotdog());
    }
    void hotdog_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        ObjectsBeh.ResetData();

        StartCoroutine(this.CreateHotdog());
    }

    #endregion

    #region <!-- Customer section.

    private void BakeryShop_nullCustomer_event(object sender, EventArgs e) {
    	StartCoroutine(CreateCustomer());
    }

    public IEnumerator CreateCustomer() { 
		yield return new WaitForFixedUpdate();
		
        if(currentCustomer == null) {
			var customer = Instantiate(Resources.Load("Customers/CustomerBeh_obj", typeof(GameObject))) as GameObject;
            currentCustomer = customer.GetComponent<CustomerBeh>();
            currentCustomer.manageGoodsComplete_event += new System.EventHandler(currentCustomer_manageGoodsComplete_event);
        }
		
        if(currentCustomer.customerSprite_Obj == null) {
            currentCustomer.customerSprite_Obj = Instantiate(Resources.Load("Customers/Customer_AnimatedSprite", typeof(GameObject))) as GameObject;
            currentCustomer.customerSprite_Obj.transform.parent = customerMenu_group_Obj.transform;
            currentCustomer.customerSprite_Obj.transform.localPosition = new Vector3(0, 0, -.1f);
        }
    }

    public IEnumerator ExpelCustomer() {
		yield return new WaitForEndOfFrame();	
		
	    if(currentCustomer != null) {
	        currentCustomer.Dispose();
	    }
		
		OnNullCustomer_event(EventArgs.Empty);
    }

    public void currentCustomer_manageGoodsComplete_event(object sender, System.EventArgs eventArgs) {
        currentGamePlayState = GamePlayState.calculationPrice;
        currentCustomer.currentCustomerBeh_State = CustomerBeh.CustomerBeh_State.none;

        this.CreateTKCalculator();
    }
    
    #endregion
    
    private void CreateTKCalculator() {
        if(calculator_group_instance) {
            calculator_group_instance.SetActiveRecursively(true);
			
			if(calculatorBeh == null) {
				calculatorBeh = calculator_group_instance.GetComponent<Mz_CalculatorBeh>();
			}
        }
    }
	
    private IEnumerator ReceiveMoneyFromCustomer() {
        currentGamePlayState = GamePlayState.receiveMoney;
        
		if (cash_obj == null)
        {
            cash_obj = Instantiate(Resources.Load("Money/Cash", typeof(GameObject))) as GameObject;
			cash_obj.transform.position = new Vector3(0, 0, -5);
			cash_sprite = cash_obj.GetComponent<tk2dSprite>();
			
            if(currentCustomer.amount <= 20) {
				cash_sprite.spriteId = cash_sprite.GetSpriteIdByName("cash_20");
				currentCustomer.payMoney = 20;
            }
            else if(currentCustomer.amount <= 50) {
				cash_sprite.spriteId = cash_sprite.GetSpriteIdByName("cash_50");
				currentCustomer.payMoney = 50;
            }
            else if(currentCustomer.amount <= 100) {
				cash_sprite.spriteId = cash_sprite.GetSpriteIdByName("cash_100");
				currentCustomer.payMoney = 100;
            }
        }
		
		yield return new WaitForSeconds(3);
		
		Destroy(cash_obj.gameObject);
		calculator_group_instance.SetActiveRecursively(true);
		
		currentGamePlayState = GamePlayState.giveTheChange;
    }

    private void TradingComplete() {
        currentGamePlayState = GamePlayState.TradeComplete;

        foreach(var good in foodTrayBeh.goodsOnTray_List) {
            Destroy(good.gameObject);
        }

        foodTrayBeh.goodsOnTray_List.Clear();
        ObjectsBeh.ResetData();

        StartCoroutine(this.PackagingGoods());
    }

    private IEnumerator PackagingGoods()
    {
        if(packaging_Obj == null) {
            packaging_Obj = Instantiate(Resources.Load(ObjectsBeh.Packages_ResourcePath + "Packages_Sprite", typeof(GameObject))) as GameObject;
            packaging_Obj.transform.parent = foodsTray_obj.transform;
            packaging_Obj.transform.localPosition = new Vector3(0, .1f, -.1f);
        }

        yield return new WaitForSeconds(2);

        StorageManage.Money += currentCustomer.amount;
        coin_Textmesh.text = StorageManage.Money.ToString();
        coin_Textmesh.Commit();

        //<!-- Clare resource data.
        currentCustomer.Dispose();
        OnNullCustomer_event(EventArgs.Empty);
        Destroy(packaging_Obj);
    }
	
    // Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
	
	public override void OnInput(string nameInput)
	{
		base.OnInput (nameInput);
		
        //<!-- Close shop button.
		if(nameInput == close_button.name) {
			if(Application.isLoadingLevel == false) {
                Mz_StorageData.Save();
                ObjectsBeh.ResetData();
				
				Mz_LoadingScreen.LoadSceneName = SceneNames.Town.ToString();
				Application.LoadLevelAsync(SceneNames.LoadingScene.ToString());			
			}
		}
		
		if (calculator_group_instance.active) {
			calculatorBeh.GetInput(nameInput);
			
			if(currentGamePlayState == GamePlayState.calculationPrice) {
				if(nameInput == "ok_button") {
					this.CallCheckAnswerOfTotalPrice();
				}
			}
			else if(currentGamePlayState == GamePlayState.giveTheChange) {
				if(nameInput == "ok_button") {
					this.CallCheckAnswerOfGiveTheChange();
				}
			}
		}
	}

    private void CallCheckAnswerOfTotalPrice() {
        if(currentCustomer.amount == calculatorBeh.GetDisplayResultTextToInt()) {
			calculatorBeh.ClearCalcMechanism();
            calculator_group_instance.SetActiveRecursively(false);

            StartCoroutine(this.ReceiveMoneyFromCustomer());
        }
        else {
            Debug.Log("Wrong answer !. Please recalculate");
        }
    }	
	
	private void CallCheckAnswerOfGiveTheChange() {
		int correct_TheChange = currentCustomer.payMoney - currentCustomer.amount;
		if(correct_TheChange == calculatorBeh.GetDisplayResultTextToInt()) {
			calculatorBeh.ClearCalcMechanism();
			calculator_group_instance.SetActiveRecursively(false);
			
			Debug.Log("give the change :: correct");

            this.TradingComplete();
		}
        else {
            Debug.Log("Wrong answer !. Please recalculate");
        }
	}
}
