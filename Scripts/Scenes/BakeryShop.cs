using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BakeryShop : Mz_BaseScene {

	public GameObject bakeryShop_backgroup_group;
	public GUI_manager gui_manager;
	
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
    private GameObject packaging_Obj;

    #region <!-- JuiceTank_group data fields.

    public GameObject juiceTank_base_Obj;
	private tk2dSprite juiceTank_base_Sprite;
	public GameObject pineAppple_tank_Obj;
	public GameObject appleTank_Obj;
	public GameObject orangeTank_Obj;

    #endregion

    #region <!-- Toast Obj group;

    public Transform toastObj_transform_group;
	public ToastBeh[] toasts = new ToastBeh[2]; 
	private Vector3 toast_1_pos = new Vector3(-0.415f, 0.419f, -1);
	private Vector3 toast_2_pos = new Vector3(-0.220f, 0.418f, -1);
    //<!-- Jam Object group;
    public JamBeh strawberryJam_instance;
    public GameObject blueberryJam_instance;
    public GameObject freshButter_instance;
    public GameObject custardJam_instance;
	
	#endregion
	
	#region <!-- Cakes object data. 
	
	public Transform cupcakeBase_transform;
	public Transform miniCakeBase_transform;
	public Transform cakeBase_transform;
	public CakeBeh cupcake;
	public CakeBeh minicake;
	public CakeBeh cake;
	
	#endregion

    #region <!-- Customer data group.
	
    public GameObject customerMenu_group_Obj;
//    public GameObject customerSprite_Instance;
    public CustomerBeh currentCustomer;  
	
	#endregion
	
	//<!-- Core data
    public event EventHandler nullCustomer_event;
    private void OnNullCustomer_event(EventArgs e) {
        if(nullCustomer_event != null) {
            nullCustomer_event(this, e);
		
			Debug.Log("Callback :: nullCustomer_event");
        }
    }

    public enum GamePlayState { none = 0, hasNewCustomer, calculationPrice, receiveMoney, giveTheChange, TradeComplete, };
    public GamePlayState currentGamePlayState;
    public GoodDataStore goodDataStore;
    public static List<Goods> CanSellGoodLists = new List<Goods>();
    public static readonly int gameLevel = 0;
    
    //<!-- Cache data session.

	
	
	// Use this for initialization
	IEnumerator Start () {
        base.InitializeAudio();

//		Mz_ResizeScale.ResizingScale(bakeryShop_backgroup_group.transform);
		foodTrayBeh = new FoodTrayBeh();
        goodDataStore = new GoodDataStore();

        if(CanSellGoodLists.Count == 0) 
            this.InitializeCanSellGoodslist();
        //########
        // Debug can sell list.
        foreach (Goods item in CanSellGoodLists) {
            Debug.Log(item);
        }

        calculator_group_instance.SetActiveRecursively(false);
		
		this.gameObject.AddComponent<GUI_manager>();
		gui_manager = this.gameObject.GetComponent<GUI_manager>();

        StartCoroutine(this.CreateToastInstance());		
		if(gameLevel == 0) {
			StartCoroutine(CreateCupcakeInstance());
		}

        yield return 0;

        nullCustomer_event += new EventHandler(BakeryShop_nullCustomer_event);
        OnNullCustomer_event(EventArgs.Empty);
	}

    private void InitializeCanSellGoodslist()
    {
        CanSellGoodLists.Add(goodDataStore.Menu_list[0]);
        CanSellGoodLists.Add(goodDataStore.Menu_list[5]);
        CanSellGoodLists.Add(goodDataStore.Menu_list[9]);
        CanSellGoodLists.Add(goodDataStore.Menu_list[18]);
    }
	
	#region Cake gameobject mechanism section.
	
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
				
		Debug.Log("CreateCupcakeInstance");
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
	
	#endregion
	
	#region Toast gameobject mechanism section.
	
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
			
			Debug.Log("CreateToastInstance : " + toasts[0]);
        }
		
        if(toasts[1] == null) {
		    GameObject temp_1 = Instantiate(Resources.Load(ObjectsBeh.ToastAndJam_ResourcePath + "breadPure", typeof(GameObject))) as GameObject;
		    toasts[1] = temp_1.GetComponent<ToastBeh>();
		    toasts[1].transform.parent = toastObj_transform_group;
		    toasts[1].transform.localPosition = toast_2_pos;
            toasts[1].destroyObj_Event += new System.EventHandler(DestroyToastEvent);
            toasts[1].putObjectOnTray_Event += new System.EventHandler(PutToastOnTrayEvent);
			
			Debug.Log("CreateToastInstance : " + toasts[1]);
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

    #region Customer section.

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
            packaging_Obj.transform.localPosition = new Vector3(0, 0, -.1f);
        }

        yield return new WaitForSeconds(2);

        StorageManage.Money += currentCustomer.amount;

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
	
	public override void OnInput (string nameInput)
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
