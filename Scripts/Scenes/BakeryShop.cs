using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BakeryShop : Mz_BaseScene {

	public GameObject bakeryShop_backgroup_group;
	
	//<!-- in game button.
	public GameObject close_button;
	public GameObject billingMachine;
    private tk2dAnimatedSprite billingAnimatedSprite;
	private AnimationState billingMachine_animState;
	
	//<!-- Miscellaneous game objects.
	
	public BinBeh bin_behavior_obj;
	public GameObject foodsTray_obj;
	public FoodTrayBeh foodTrayBeh;
    public GameObject calculator_group_instance;
    public GameObject receiptGUIForm_groupObj;
    public GameObject giveTheChangeGUIForm_groupObj;
    public tk2dTextMesh totalPrice_textmesh;
    public tk2dTextMesh receiveMoney_textmesh;
    public tk2dTextMesh change_textmesh;
    public tk2dTextMesh displayAnswer_textmesh;
    public GameObject baseOrderUI_Obj;
	public GameObject darkShadowPlane;
	public GameObject rollingDoor_Obj;
	
	public GameObject[] arr_addNotations = new GameObject[2];
	public GameObject[] arr_goodsLabel = new GameObject[3];
	public tk2dSprite[] arr_GoodsTag = new tk2dSprite[3];
	public tk2dTextMesh[] arr_GoodsPrice_textmesh = new tk2dTextMesh[3];
    public GameObject[] arr_orderingBaseItems = new GameObject[3];
	public tk2dSprite[] arr_orderingItems = new tk2dSprite[3];
	private Mz_CalculatorBeh calculatorBeh;
    private GameObject cash_obj;
	private tk2dSprite cash_sprite;
    private tk2dTextMesh coin_Textmesh;
    private GameObject packaging_Obj;
	public CharacterAnimationManager TK_animationManager;
	public tk2dSprite shopLogo_sprite;
    public GoodDataStore goodDataStore;
    public static List<int> NumberOfCansellItem = new List<int>();
    public List<Goods> CanSellGoodLists = new List<Goods>();
    //<!-- Core data
    public enum GamePlayState { 
		none = 0,
        Ordering = 1,
		calculationPrice,
		receiveMoney,
		giveTheChange, 
		TradeComplete,
	};
    public GamePlayState currentGamePlayState;

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

	/// <summary>
	/// <!-- Cakes && CreamBeh data fields.
	/// </summary>
	public Transform cupcakeBase_transform;
	public Transform miniCakeBase_transform;
	public Transform cakeBase_transform;
	public CakeBeh cupcake;
	public CakeBeh miniCake;
	public CakeBeh cake;
    public GameObject chocolate_cream_Instance;
    public GameObject blueberry_cream_Instance;
    public GameObject strawberry_cream_Instance;

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

    /// Hotdog data fields group.
    public Transform hotdogTray_transform;
    public HotdogBeh hotdog;

	#region <!-- Customer data group.

    public GameObject customerMenu_group_Obj;
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
        yield return StartCoroutine(this.InitailizeSceneObject());
        StartCoroutine(this.InitializeGameEffect());
        this.OpenShop();
	}

    private IEnumerator InitailizeSceneObject()
    {
		darkShadowPlane.active = false;
//		Mz_ResizeScale.ResizingScale(bakeryShop_backgroup_group.transform);]
        StartCoroutine(this.SceneInitializeAudio());
		StartCoroutine(this.ChangeShopLogoIcon());
		StartCoroutine(this.InitializeObjectAnimation());
		///<!-- Unactive Souse Tanks. 
		appleTank_Obj.SetActiveRecursively(false);
		orangeTank_Obj.SetActiveRecursively(false);
		cocoaMilkTank_Obj.SetActiveRecursively(false);
		freshMilkTank_Obj.SetActiveRecursively(false);
        //<@-- Unactive Icecream tanks.
		icecreamVanillaTank_obj.SetActiveRecursively(false);
		icecreamChocolateTank_obj.SetActiveRecursively(false);
		///<!-- Manage Jam Instance.
		blueberryJam_instance.SetActiveRecursively(false);
		freshButterJam_instance.SetActiveRecursively(false);
		custardJam_instance.SetActiveRecursively(false);

        StartCoroutine(CreateToastInstance());
        // <@-- Cake && Cream initilaize.
		StartCoroutine(this.InitializeCreamBeh());
        StartCoroutine(CreateCupcakeInstance());
        StartCoroutine(InitializeMinicakeInstance());
        StartCoroutine(InitializeCakeInstance());
        //<@-- Sandwich Initialize.
        StartCoroutine(InitializeTunaSandwichInstance());
		StartCoroutine(this.Initialize_deepFriedChickenSandwich());
		StartCoroutine(this.Initailize_HamSandwich());
		StartCoroutine(this.Initialize_EggSandwich());

        StartCoroutine(this.InitializeChocolateChipCookie());
		StartCoroutine(this.Initializing_FriutCookie());
		StartCoroutine(this.Initializing_ButterCookie());

        StartCoroutine(this.InitializeHotdogInstance());

        yield return null;

		foodTrayBeh = new FoodTrayBeh();
        goodDataStore = new GoodDataStore();

        GameObject coinObj = GameObject.Find("Coin");
        coin_Textmesh = coinObj.GetComponent<tk2dTextMesh>();
        coin_Textmesh.text = Mz_StorageManage.AvailableMoney.ToString();
        coin_Textmesh.Commit();
        
        calculator_group_instance.SetActiveRecursively(false);
        // Debug can sell list.
        StartCoroutine(this.InitializeCanSellGoodslist());
		Debug.Log("CanSellGoodLists.Count : " + CanSellGoodLists.Count);
		Debug.Log("NumberOfCansellItem.Count : " + NumberOfCansellItem.Count);
    }

    private void OpenShop() {
        iTween.MoveTo(rollingDoor_Obj, iTween.Hash("position", new Vector3(0, 2.2f, 1), "islocal", true, "time", 1f, "easetype", iTween.EaseType.easeInSine));
        audioEffect.PlayOnecSound(base.soundEffect_clips[0]);
				
        nullCustomer_event += new EventHandler(BakeryShop_nullCustomer_event);
        OnNullCustomer_event(EventArgs.Empty);
    }
   
	private IEnumerator SceneInitializeAudio ()
	{
        base.InitializeAudio();
		
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();
		
		yield return null;
	}

    private IEnumerator InitializeGameEffect()
    {
        if (gameEffectManager == null) {
            this.gameObject.AddComponent<GameEffectManager>();
            gameEffectManager = this.GetComponent<GameEffectManager>();
        }
        else
            yield return null;
    }

	IEnumerator ChangeShopLogoIcon ()
	{
		shopLogo_sprite.spriteId = shopLogo_sprite.GetSpriteIdByName(InitializeNewShop.shopLogo_NameSpecify[Mz_StorageManage.ShopLogo]);
		shopLogo_sprite.color = InitializeNewShop.shopLogos_Color[Mz_StorageManage.ShopLogoColor];

		yield return 0;
	}

	IEnumerator InitializeObjectAnimation ()
	{
        billingMachine_animState = billingMachine.animation["billingMachine_anim"];
        billingMachine_animState.wrapMode = WrapMode.Once;
        billingAnimatedSprite = billingMachine.GetComponent<tk2dAnimatedSprite>();

		yield return 0;
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

    private IEnumerator InitializeHotdogInstance()
    {
        yield return StartCoroutine(this.CreateHotdog());
        hotdog.gameObject.SetActiveRecursively(false);
    }

    private IEnumerator InitializeCanSellGoodslist()
    {		
        if(BakeryShop.NumberOfCansellItem.Count == 0)
            base.extendsStorageManager.LoadCanSellGoodsListData();

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

        yield return null;
    }
	
	#region <!-- Cake && Cream object mechanism section.

	IEnumerator InitializeCreamBeh ()
	{
		chocolate_cream_Instance.active = true;
		if(CreamBeh.arr_CreamBehs[1] != string.Empty)
			blueberry_cream_Instance.active = true;
		else
			blueberry_cream_Instance.active = false;
		if(CreamBeh.arr_CreamBehs[2] != string.Empty)
			strawberry_cream_Instance.active = true;
		else
			strawberry_cream_Instance.active = false;
		

		yield return null;
	}
	
	IEnumerator CreateCupcakeInstance() {		
		yield return new WaitForFixedUpdate();
		
		if(cupcake == null) {
			GameObject temp_cupcake = Instantiate(Resources.Load(ObjectsBeh.Cakes_ResourcePath + CakeBeh.Cupcake, typeof(GameObject))) as GameObject;
			temp_cupcake.transform.parent = cupcakeBase_transform;
			temp_cupcake.transform.localPosition = new Vector3(0, 0.13f, -.2f);
			temp_cupcake.name = CakeBeh.Cupcake;
			cupcake = temp_cupcake.GetComponent<CakeBeh>();
			cupcake.destroyObj_Event += Handle_CupcakedestroyObj_Event;
			cupcake.putObjectOnTray_Event += Handle_CupcakeputObjectOnTray_Event;
		}
	}
    void Handle_CupcakedestroyObj_Event (object sender, System.EventArgs e) {
		foodTrayBeh.goodsOnTray_List.Remove((GoodsBeh)sender);
        foodTrayBeh.ReCalculatatePositionOfGoods();

		StartCoroutine(CreateCupcakeInstance());
	}
	void Handle_CupcakeputObjectOnTray_Event (object sender, System.EventArgs e)
	{	
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			cupcake = null;			
			StartCoroutine(CreateCupcakeInstance());

		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
	}

    IEnumerator CreateMiniCakeInstance() {
		yield return new WaitForFixedUpdate();
		
		if(miniCake == null) {
			GameObject temp_Minicake = Instantiate(Resources.Load(ObjectsBeh.Cakes_ResourcePath + CakeBeh.MiniCake, typeof(GameObject))) as GameObject;
			temp_Minicake.transform.parent = miniCakeBase_transform;
			temp_Minicake.transform.localPosition = new Vector3(-0.01f, 0.11f, -0.1f);
			temp_Minicake.name = CakeBeh.MiniCake;

			miniCake = temp_Minicake.GetComponent<CakeBeh>();
            miniCake.offsetPos = Vector3.up * -0.05f;
			miniCake.destroyObj_Event += new EventHandler(miniCake_destroyObj_Event);
            miniCake.putObjectOnTray_Event += new EventHandler(miniCake_putObjectOnTray_Event);
		}
    }
    void miniCake_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(CreateMiniCakeInstance());
    }
    void miniCake_putObjectOnTray_Event(object sender, EventArgs e) {
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			miniCake = null;			
			StartCoroutine(CreateMiniCakeInstance());			
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
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
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(CreateCakeInstance());
    }
    void Cake_putObjectOnTray_Event(object sender, EventArgs e) {
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
						
			cake = null;
			StartCoroutine(CreateCakeInstance());
			
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
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
            toasts[0].putObjectOnTray_Event += new System.EventHandler(PutToastOnTrayEvent);
            toasts[0].destroyObj_Event += new System.EventHandler(DestroyToastEvent);
        }
		
        if(toasts[1] == null) {
		    GameObject temp_1 = Instantiate(Resources.Load(ObjectsBeh.ToastAndJam_ResourcePath + "breadPure", typeof(GameObject))) as GameObject;
		    toasts[1] = temp_1.GetComponent<ToastBeh>();
		    toasts[1].transform.parent = toastObj_transform_group;
		    toasts[1].transform.localPosition = toast_2_pos;
            toasts[1].putObjectOnTray_Event += new System.EventHandler(PutToastOnTrayEvent);
            toasts[1].destroyObj_Event += new System.EventHandler(DestroyToastEvent);
        }
    }
	
	public void DestroyToastEvent(object sender, System.EventArgs e) {
        Debug.Log("DestroyToastEvent");
		
		foodTrayBeh.goodsOnTray_List.Remove((GoodsBeh)sender);
        foodTrayBeh.ReCalculatatePositionOfGoods();

		StartCoroutine(CreateToastInstance());
	}

    public void PutToastOnTrayEvent(object sender, System.EventArgs e) {
        Debug.Log("PutToastOnTrayEvent");

		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			StartCoroutine(CreateToastInstance());			
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
    }
	
	#endregion
	
	#region <!-- Sandwich Obj behavior.

	IEnumerator InitializeTunaSandwichInstance()
	{
		yield return StartCoroutine(this.CreateTunaSandwich());
		tunaSandwich.gameObject.SetActiveRecursively(false);
	}
	
	IEnumerator Initialize_deepFriedChickenSandwich()
	{
		yield return StartCoroutine(this.CreateDeepFriedChickenSandwich());
		deepFriedChickenSandwich.gameObject.SetActiveRecursively(false);
	}
	
	IEnumerator Initailize_HamSandwich ()
	{
		yield return StartCoroutine(this.CreateHamSanwich());
		hamSandwich.gameObject.SetActiveRecursively(false);
	}
	
	IEnumerator Initialize_EggSandwich ()
	{
		yield return StartCoroutine(this.CreateEggSandwich());
		eggSandwich.gameObject.SetActiveRecursively(false);
	}
	
	/// <summary>
	/// Creates the tuna sandwich.
	/// </summary>
	IEnumerator CreateTunaSandwich() {
        yield return new WaitForFixedUpdate();

		if(tunaSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "TunaSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(.235f, -.15f, -.1f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.Tuna_sandwich.ToString();

            tunaSandwich = sandwich.GetComponent<SandwichBeh>();
            tunaSandwich.putObjectOnTray_Event += new EventHandler(tunaSandwich_putObjectOnTray_Event);
            tunaSandwich.destroyObj_Event += new EventHandler(tunaSandwich_destroyObj_Event);
		}
	}
    void tunaSandwich_putObjectOnTray_Event(object sender, EventArgs e) {
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			tunaSandwich = null;
			StartCoroutine(this.CreateTunaSandwich());
			
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
    }
    void tunaSandwich_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateTunaSandwich());
    }
	
	/// <summary>
	/// Creates the deep fried chicken sandwich.
	/// </summary>
	IEnumerator CreateDeepFriedChickenSandwich() {
        yield return new WaitForFixedUpdate();

		if(deepFriedChickenSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "DeepFriedChickenSandwich", typeof(GameObject))) as GameObject;
			sandwich.transform.parent = sandwichCookieTray_Transform;
			sandwich.transform.localPosition = new Vector3(0.105f, -.16f, -.2f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.DeepFriedChicken_sandwich.ToString();
			
			deepFriedChickenSandwich = sandwich.GetComponent<SandwichBeh>();
			deepFriedChickenSandwich.putObjectOnTray_Event += Handle_DeepFriedChickenSandwich_putObjectOnTray_Event;
			deepFriedChickenSandwich.destroyObj_Event += Handle_DeepFriedChickenSandwich_destroyObj_Event;
		}
	}
	void Handle_DeepFriedChickenSandwich_putObjectOnTray_Event(object sender, EventArgs e)
	{
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			deepFriedChickenSandwich = null;
			StartCoroutine(this.CreateDeepFriedChickenSandwich());			
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
	}
	void Handle_DeepFriedChickenSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateDeepFriedChickenSandwich());
	}
	
	/// <summary>
	/// Creates the ham sanwich.
	/// </summary>
	IEnumerator CreateHamSanwich() {
        yield return new WaitForFixedUpdate();
        
        if (hamSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "HamSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(-.015f, -.17f, -.3f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.Ham_sandwich.ToString();

            hamSandwich = sandwich.GetComponent<SandwichBeh>();
            hamSandwich.putObjectOnTray_Event += Handle_HamSandwich_putObjectOnTray_Event;
            hamSandwich.destroyObj_Event += Handle_HamSandwich_destroyObj_Event;
		}
	}
	void Handle_HamSandwich_putObjectOnTray_Event (object sender, EventArgs e)
	{
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			hamSandwich = null;
			StartCoroutine(this.CreateHamSanwich());
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
	}
	void Handle_HamSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateHamSanwich());
	}
	
	/// <summary>
	/// Creates the egg sandwich.
	/// </summary>
	IEnumerator CreateEggSandwich() {
        yield return new WaitForFixedUpdate();

		if(eggSandwich == null) {
			GameObject sandwich = Instantiate(Resources.Load(ObjectsBeh.Sandwich_ResourcePath + "EggSandwich", typeof(GameObject))) as GameObject;
            sandwich.transform.parent = sandwichCookieTray_Transform;
            sandwich.transform.localPosition = new Vector3(-.14f, -.17f, -.4f);
            sandwich.gameObject.name = GoodDataStore.GoodsOrderList.Egg_sandwich.ToString();

            eggSandwich = sandwich.GetComponent<SandwichBeh>();
            eggSandwich.putObjectOnTray_Event += Handle_EggSandwich_putObjectOnTray_Event;
            eggSandwich.destroyObj_Event += Handle_EggSandwich_destroyObj_Event;
		}
	}
	void Handle_EggSandwich_putObjectOnTray_Event (object sender, EventArgs e)
	{
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			eggSandwich = null;
			StartCoroutine(this.CreateEggSandwich());	
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
	}
	void Handle_EggSandwich_destroyObj_Event (object sender, EventArgs e)
	{
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateEggSandwich());
	}
	
	#endregion

    #region <!-- Cookie Object Behavior.

	/// Creates the chocolate chip_ cookie.

    IEnumerator InitializeChocolateChipCookie()
    {
        yield return StartCoroutine(this.CreateChocolateChip_Cookie());
        chocolateChip_cookie.gameObject.SetActiveRecursively(false);
    }  
    IEnumerator CreateChocolateChip_Cookie() {
        yield return new WaitForFixedUpdate();

        if(chocolateChip_cookie == null) {
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "ChocolateChip_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(-.165f, 0.1f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.Chocolate_cookie.ToString();

            chocolateChip_cookie = cookie.GetComponent<CookieBeh>();
            chocolateChip_cookie.putObjectOnTray_Event += new EventHandler(chocolateChip_cookie_putObjectOnTray_Event);
            chocolateChip_cookie.destroyObj_Event += new EventHandler(chocolateChip_cookie_destroyObj_Event);
        }
    }
    void chocolateChip_cookie_putObjectOnTray_Event(object sender, EventArgs e) {
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
						
			chocolateChip_cookie = null;
			StartCoroutine(this.CreateChocolateChip_Cookie());	
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
    }
    void chocolateChip_cookie_destroyObj_Event(object sender, EventArgs e) {    
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateChocolateChip_Cookie());
    }
	
	/// Create instance of fruit_cookie object and cookie behavior.
	IEnumerator Initializing_FriutCookie ()
	{
		yield return StartCoroutine(this.CreateFruitCookie());
		fruit_cookie.gameObject.SetActiveRecursively(false);
	}
	IEnumerator CreateFruitCookie() {
		yield return new WaitForFixedUpdate();

		if(fruit_cookie == null) {			
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "Fruit_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(0.02f, 0.1f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.Fruit_cookie.ToString();

            fruit_cookie = cookie.GetComponent<CookieBeh>();
            fruit_cookie.putObjectOnTray_Event += new EventHandler(Handle_FruitCookie_putObjectOnTray_Event);
            fruit_cookie.destroyObj_Event += new EventHandler(Handle_FruitCookie_DestroyObj_Event);
		}
	}
	void Handle_FruitCookie_putObjectOnTray_Event(object sender, EventArgs e) {
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;

			fruit_cookie = null;
			StartCoroutine(this.CreateFruitCookie());		
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
	}
	void Handle_FruitCookie_DestroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateFruitCookie());
	}
    
    /// Create instance of ButterCookie object and cookie behavior.
	IEnumerator Initializing_ButterCookie ()
	{
		yield return StartCoroutine(this.CreateButterCookie());
		butter_cookie.gameObject.SetActiveRecursively(false);
	}
    IEnumerator CreateButterCookie()
    {
		yield return new WaitForFixedUpdate();

        if(butter_cookie == null) {
            GameObject cookie = Instantiate(Resources.Load(ObjectsBeh.Cookie_ResourcePath + "Butter_Cookie", typeof(GameObject))) as GameObject;
            cookie.transform.parent = sandwichCookieTray_Transform;
            cookie.transform.localPosition = new Vector3(.2f, 0.11f, -.1f);
            cookie.gameObject.name = GoodDataStore.GoodsOrderList.Butter_cookie.ToString();

            butter_cookie = cookie.GetComponent<CookieBeh>();
            butter_cookie.putObjectOnTray_Event += new EventHandler(butter_cookie_putObjectOnTray_Event);
            butter_cookie.destroyObj_Event += new EventHandler(butter_cookie_destroyObj_Event);
        }
    }
    void butter_cookie_putObjectOnTray_Event(object sender, EventArgs e) 
	{
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;

			butter_cookie = null;
			StartCoroutine(this.CreateButterCookie());	
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
    }
    void butter_cookie_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateButterCookie());
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
		GoodsBeh obj = sender as GoodsBeh;
		if (foodTrayBeh.goodsOnTray_List.Contains (obj) == false && foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			foodTrayBeh.goodsOnTray_List.Add (obj);
			foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
						
			hotdog = null;
			StartCoroutine(this.CreateHotdog());	
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");
			
			obj.transform.position = obj.originalPosition;
		}
    }
    void hotdog_destroyObj_Event(object sender, EventArgs e) {
        foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
        foodTrayBeh.ReCalculatatePositionOfGoods();

        StartCoroutine(this.CreateHotdog());
    }

    #endregion

    #region <!-- Customer section.

    private void BakeryShop_nullCustomer_event(object sender, EventArgs e) {
    	StartCoroutine(CreateCustomer());
    }

    private IEnumerator CreateCustomer() { 
		yield return new WaitForFixedUpdate();
		yield return new WaitForSeconds(1f);
		
        if(currentCustomer == null) {
			GameObject customer = Instantiate(Resources.Load("Customers/CustomerBeh_obj", typeof(GameObject))) as GameObject;
            currentCustomer = customer.GetComponent<CustomerBeh>();
            currentCustomer.manageGoodsComplete_event += new System.EventHandler(currentCustomer_manageGoodsComplete_event);

            audioEffect.PlayOnecSound(audioEffect.dingdong_clip);
        }
		
        if(currentCustomer.customerSprite_Obj == null) {
            currentCustomer.customerSprite_Obj = Instantiate(Resources.Load("Customers/Customer_AnimatedSprite", typeof(GameObject))) as GameObject;
            currentCustomer.customerSprite_Obj.transform.parent = customerMenu_group_Obj.transform;
            currentCustomer.customerSprite_Obj.transform.localPosition = new Vector3(0, 0, -.1f);

			currentCustomer.customerOrderingIcon_Obj = Instantiate(Resources.Load("Customers/CustomerOrdering_icon", typeof(GameObject))) as GameObject;
			currentCustomer.customerOrderingIcon_Obj.transform.parent = customerMenu_group_Obj.transform;
			currentCustomer.customerOrderingIcon_Obj.transform.localPosition = new Vector3(.35f, .05f, -.2f);
			currentCustomer.customerOrderingIcon_Obj.name = "OrderingIcon";

			currentCustomer.customerOrderingIcon_Obj.active = false;
        }
    }

    private IEnumerator ExpelCustomer() {
        yield return new WaitForSeconds(1f);

	    if(currentCustomer != null) {
	        currentCustomer.Dispose();
            Destroy(currentCustomer.gameObject);
	    }
		
		yield return new WaitForFixedUpdate();	
		
		OnNullCustomer_event(EventArgs.Empty);
    }

    public void currentCustomer_manageGoodsComplete_event (object sender, System.EventArgs eventArgs)
	{
		currentGamePlayState = GamePlayState.calculationPrice;

        TK_animationManager.PlayGoodAnimation();
        currentCustomer.customerOrderingIcon_Obj.active = false;

        StartCoroutine(this.ShowReceiptGUIForm());
    }

    private IEnumerator ShowReceiptGUIForm()
    {
		yield return new WaitForSeconds(0.5f);

		darkShadowPlane.active = true;
        
        audioEffect.PlayOnecSound(audioEffect.receiptCash_clip);
		
		this.CreateTKCalculator();
		calculatorBeh.result_Textmesh = displayAnswer_textmesh;
		receiptGUIForm_groupObj.SetActiveRecursively(true);
		this.DeActiveCalculationPriceGUI();
		this.ManageCalculationPriceGUI();
    }

	void DeActiveCalculationPriceGUI ()
	{
		for (int i = 0; i < arr_addNotations.Length; i++) {
			arr_addNotations[i].active = false;
		}
		for (int i = 0; i < arr_goodsLabel.Length; i++) {
			arr_goodsLabel[i].SetActiveRecursively(false);
		}
	}

	void ManageCalculationPriceGUI ()
	{		
		for (int i = 0; i < currentCustomer.customerOrderRequire.Count; i++) {
			arr_goodsLabel[i].SetActiveRecursively(true);
			arr_GoodsTag[i].spriteId = arr_GoodsTag[i].GetSpriteIdByName(currentCustomer.customerOrderRequire[i].goods.name);
			arr_GoodsPrice_textmesh[i].text = currentCustomer.customerOrderRequire[i].goods.price.ToString();
			arr_GoodsPrice_textmesh[i].Commit();
			if(i != 0)
				arr_addNotations[i - 1].active = true;
		}
	}

    internal void GenerateOrderGUI ()
	{
		foreach (var item in arr_orderingBaseItems) {
			item.SetActiveRecursively (false);
		}

		for (int i = 0; i < currentCustomer.customerOrderRequire.Count; i++) {
			arr_orderingBaseItems[i].SetActiveRecursively(true);	
			arr_orderingItems[i].spriteId = arr_orderingItems[i].GetSpriteIdByName(currentCustomer.customerOrderRequire[i].goods.name);
		}

		StartCoroutine(this.ShowOrderingGUI());
		currentGamePlayState = GamePlayState.Ordering;
    }

	IEnumerator ShowOrderingGUI ()
	{
		iTween.MoveTo(baseOrderUI_Obj.gameObject, 
		              iTween.Hash("position", new Vector3(-0.85f, .06f, 0f), "islocal", true, "time", .5f, "easetype", iTween.EaseType.spring));

		currentCustomer.customerOrderingIcon_Obj.active = false;
		yield return new WaitForSeconds(.5f);
		darkShadowPlane.active = true;
		
		foreach (var item in arr_orderingItems) {
            iTween.MoveTo(item.gameObject, iTween.Hash("y", 0.1f, "islocal", true, "time", .3f, "looptype", iTween.LoopType.pingPong));
		}
	}

	IEnumerator CollapseOrderingGUI ()
	{
		iTween.MoveTo(baseOrderUI_Obj.gameObject, 
		              iTween.Hash("position", new Vector3(-0.85f, -2f, 0f), "islocal", true, "time", 0.5f, "easetype", iTween.EaseType.linear));

		yield return new WaitForSeconds(0.5f);
		
		currentCustomer.customerOrderingIcon_Obj.active = true;
        iTween.PunchPosition(currentCustomer.customerOrderingIcon_Obj, 
            iTween.Hash("x", .1f, "y", .1f, "delay", 1f, "time", .5f, "looptype", iTween.LoopType.pingPong));
		darkShadowPlane.active = false;
	}
    
    #endregion
    
    private void CreateTKCalculator() {
        if(calculator_group_instance) {
            calculator_group_instance.SetActiveRecursively(true);
			
			if(calculatorBeh == null) {
				calculatorBeh = calculator_group_instance.GetComponent<Mz_CalculatorBeh>();
			}
        }

        if (calculatorBeh == null)
            Debug.LogError(calculatorBeh);
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
		this.DeActiveCalculationPriceGUI();

		this.ShowGiveTheChangeForm();
		currentGamePlayState = GamePlayState.giveTheChange;
    }

	void ShowGiveTheChangeForm ()
	{
        giveTheChangeGUIForm_groupObj.SetActiveRecursively(true);
		darkShadowPlane.active = true;
		
		audioEffect.PlayOnecSound(audioEffect.giveTheChange_clip);

        totalPrice_textmesh.text = currentCustomer.amount.ToString();
        totalPrice_textmesh.Commit();
        receiveMoney_textmesh.text = currentCustomer.payMoney.ToString();
        receiveMoney_textmesh.Commit();

        calculatorBeh.result_Textmesh = change_textmesh;
	}

    private void TradingComplete() {
        currentGamePlayState = GamePlayState.TradeComplete;

        foreach(var good in foodTrayBeh.goodsOnTray_List) {
            Destroy(good.gameObject);
        }

        foodTrayBeh.goodsOnTray_List.Clear();

        StartCoroutine(this.PackagingGoods());
    }

    private IEnumerator PackagingGoods()
    {
        if(packaging_Obj == null) {
            packaging_Obj = Instantiate(Resources.Load(ObjectsBeh.Packages_ResourcePath + "Packages_Sprite", typeof(GameObject))) as GameObject;
            packaging_Obj.transform.parent = foodsTray_obj.transform;
            packaging_Obj.transform.localPosition = new Vector3(0, .1f, -.1f);
        }
		
		TK_animationManager.RandomPlayGoodAnimation();

        yield return new WaitForSeconds(2);
        
        StartCoroutine(this.CreateGameEffect());
		audioEffect.PlayOnecSound(audioEffect.longBring_clip);

		TK_animationManager.RandomPlayGoodAnimation();

        billingAnimatedSprite.Play("Thanks");
        billingAnimatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
			billingAnimatedSprite.Play("Billing");
		};
        
        Mz_StorageManage.AvailableMoney += currentCustomer.amount;
        coin_Textmesh.text = Mz_StorageManage.AvailableMoney.ToString();
        coin_Textmesh.Commit();

        //<!-- Clare resource data.
		Destroy(packaging_Obj);
		StartCoroutine(ExpelCustomer());
    }

    private IEnumerator CreateGameEffect()
    {
        //GameObject effect = Instantiate(Resources.Load(GameEffectManager.BLOOMSTAR_EFFECT_PATH, typeof(GameObject))) as GameObject;
        //effect.transform.parent = foodsTray_obj.transform;
        //effect.transform.localPosition = new Vector3(0, 0, -0.2f);
        //effect.GetComponent<tk2dAnimatedSprite>().animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
        //{
        //    Destroy(effect);
        //};

        gameEffectManager.Create2DSpriteAnimationEffect(GameEffectManager.BLOOMSTAR_EFFECT_PATH, foodsTray_obj.transform);

        yield return 0;
    }
	
	public override void OnInput(string nameInput)
	{		
        //<!-- Close shop button.
		if(nameInput == close_button.name) {
			if(Application.isLoadingLevel == false) {
                base.extendsStorageManager.SaveDataToPermanentMemory();
                this.PreparingToCloseShop();		
				
				return;
			}
		}
		
		if (calculator_group_instance.active) 
        {
			if(currentGamePlayState == GamePlayState.calculationPrice) {
				if(nameInput == "ok_button") {
					this.CallCheckAnswerOfTotalPrice();
					return;
				}
			}
			else if(currentGamePlayState == GamePlayState.giveTheChange) {
				if(nameInput == "ok_button") {
					this.CallCheckAnswerOfGiveTheChange();
					return;
				}
			}
			
			calculatorBeh.GetInput(nameInput);
		}


        if (currentGamePlayState == GamePlayState.Ordering)
        {
            switch (nameInput)
            {
                case "OK_button":
                    StartCoroutine(this.CollapseOrderingGUI());
                    //				currentCustomer.CheckGoodsObjInTray();
                    break;
                case "Goaway_button":
                    currentCustomer.PlayRampage_animation();
                    StartCoroutine(this.ExpelCustomer());
                    break;
                case "OrderingIcon": StartCoroutine(this.ShowOrderingGUI());
                    break;
                case "Billing_machine":
                    audioEffect.PlayOnecSound(audioEffect.calc_clip);
                    billingMachine.animation.Play(billingMachine_animState.name);
                    StartCoroutine(this.CheckingUNITYAnimationComplete(billingMachine.animation, billingMachine_animState.name));
                    break;
                default:
                    break;
            }
        }
	}

    private void CallCheckAnswerOfTotalPrice() {
        if(currentCustomer.amount == calculatorBeh.GetDisplayResultTextToInt()) {
			audioEffect.PlayOnecSound(audioEffect.correct_Clip);
		
			calculatorBeh.ClearCalcMechanism();
            calculator_group_instance.SetActiveRecursively(false);
            receiptGUIForm_groupObj.SetActiveRecursively(false);
            darkShadowPlane.active = false;

            StartCoroutine(this.ReceiveMoneyFromCustomer());
        }
        else {
			audioEffect.PlayOnecSound(audioEffect.wrong_Clip);
            calculatorBeh.ClearCalcMechanism();
			
            Debug.LogWarning("Wrong answer !. Please recalculate");
        }
    }	
	
	private void CallCheckAnswerOfGiveTheChange() {
		int correct_TheChange = currentCustomer.payMoney - currentCustomer.amount;
		if(correct_TheChange == calculatorBeh.GetDisplayResultTextToInt()) {
			calculatorBeh.ClearCalcMechanism();
			calculator_group_instance.SetActiveRecursively(false);
            giveTheChangeGUIForm_groupObj.SetActiveRecursively(false);
            darkShadowPlane.active = false;
			
			audioEffect.PlayOnecWithOutStop(audioEffect.correct_Clip);
			
			Debug.Log("give the change :: correct");

            this.TradingComplete();
		}
        else {			
			audioEffect.PlayOnecWithOutStop(audioEffect.wrong_Clip);
			calculatorBeh.ClearCalcMechanism();
            currentCustomer.PlayRampage_animation();
			
            Debug.Log("Wrong answer !. Please recalculate");
        }
	}

    private IEnumerator CheckingUNITYAnimationComplete(Animation targetAnimation, string targetAnimatedName)
    {
        do
        {
            yield return null;
        } while (targetAnimation.IsPlaying(targetAnimatedName));

		Debug.LogWarning(targetAnimatedName + " finish !");

        currentCustomer.CheckGoodsObjInTray();
    }
    
    private void PreparingToCloseShop()
    {
        this.OnDispose();

        iTween.MoveTo(rollingDoor_Obj, iTween.Hash("position", new Vector3(0, 0, 1), "islocal", true, "time", 1f, "easetype", iTween.EaseType.easeOutSine,
            "oncomplete", "RollingDoor_close", "oncompletetarget", this.gameObject));
		audioEffect.PlayOnecWithOutStop(base.soundEffect_clips[0]);
        rollingDoor_Obj.SetActiveRecursively(true);
    }
    private void RollingDoor_close() {
        Mz_LoadingScreen.LoadSceneName = SceneNames.Town.ToString();
        Application.LoadLevel(SceneNames.LoadingScene.ToString());	
    }

    public override void OnDispose()
    {
        base.OnDispose();

		GoodsBeh.StaticDispose();

        Destroy(customerMenu_group_Obj);
        currentCustomer.Dispose();
        
        Destroy(cupcake.gameObject);
        Destroy(miniCake.gameObject);
        Destroy(cake.gameObject);

        Destroy(toasts[0].gameObject);
        Destroy(toasts[1].gameObject);
        
        Destroy(blueberry_cream_Instance.gameObject);
        Destroy(chocolate_cream_Instance.gameObject);
        Destroy(strawberry_cream_Instance.gameObject);

        Destroy(blueberryJam_instance.gameObject);
        Destroy(custardJam_instance);
        Destroy(freshButterJam_instance);
        Destroy(strawberryJam_instance);

        Destroy(hotdog.gameObject);
    }
}
