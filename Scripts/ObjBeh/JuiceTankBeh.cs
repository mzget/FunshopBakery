using UnityEngine;
using System.Collections;

public class JuiceTankBeh : ObjectsBeh {
	
	public const string PineappleJuiceTank = "PineappleJuiceTank";
	public const string AppleJuiceTank = "AppleJuiceTank";
	public const string OrangeJuiceTank = "OrangeJuiceTank";
    public const string CocoaMilkTank = "CocoaMilkTank";
    public const string FreshMilkTank = "FreshMilkTank";

	public const string PineappleJuice = "PineappleJuice";
	public const string AppleJuice = "AppleJuice";
	public const string OrangeJuice = "OrangeJuice";
    public const string CocoaMilk = "CocoaMilk";
    public const string FreshMilk = "FreshMilk";
	
	
	private BakeryShop sceneManager;
	
    //<!--- pineapple glass local position == vector3(-1.12f, -.48f, -.2f).
    //<!--- apple glass local pos == (-.815f, -.48f, -.2f).
    //<!--- orange glass local pos == (-.53f, -.48f, -.2f).	
    //<!-- Cocoamilk glass local position => (-0.133, -0.177, -.1);
    //<!-- Freshmilk glass local position => (-0.124, -0.145, -.1);
    private GameObject juice_glass_instance;
	private GlassBeh juiceGlassBeh;


	// Use this for initialization
    protected override void Start()
    {
        base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();			
    }
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

    #region <!-- OnInput.

	protected override void OnTouchDown()
    {
        if(juice_glass_instance == null)
		{
			if(this.gameObject.name == PineappleJuiceTank) 
				this.Create_PineappleJuiceGlass();
			else if(this.gameObject.name == AppleJuiceTank) 
				this.Create_AppleJuiceTank();
			else if(this.gameObject.name == OrangeJuiceTank) 
				this.Create_OrangeJuiceGlass();
			else if(this.gameObject.name == CocoaMilkTank) 
				this.Create_CocoaMilkGlass();
			else if(this.gameObject.name == FreshMilkTank)
				this.Create_FreshMilkGlass();
        }

        base.OnTouchDown();
    }

    #endregion

    private void Create_PineappleJuiceGlass()
    {
        juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + PineappleJuice, typeof(GameObject))) as GameObject;
        juice_glass_instance.transform.parent = this.transform;
        juice_glass_instance.transform.localPosition = new Vector3(0.01f, -0.325f, 0);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.pineapple_juice.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += Handle_JuiceGlassBeh_destroyObj_Event;
    }
	
	void Create_AppleJuiceTank() {		
        juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + AppleJuice, typeof(GameObject))) as GameObject;
        juice_glass_instance.transform.parent = this.transform;
        juice_glass_instance.transform.localPosition = new Vector3(0.01f, -0.325f, 0);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.apple_juice.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += Handle_JuiceGlassBeh_destroyObj_Event;
	}
	
	void Create_OrangeJuiceGlass() {
        juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + OrangeJuice, typeof(GameObject))) as GameObject;
        juice_glass_instance.transform.parent = this.transform;
        juice_glass_instance.transform.localPosition = new Vector3(0.01f, -0.325f, 0);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.orange_juice.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += Handle_JuiceGlassBeh_destroyObj_Event;
	}

	void Create_CocoaMilkGlass ()
	{
		juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + CocoaMilk, typeof(GameObject))) as GameObject;
		juice_glass_instance.transform.parent = this.transform;
		juice_glass_instance.transform.localPosition = new Vector3(-0.133f, -0.177f, -.1f);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.cocoa_milk.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += Handle_JuiceGlassBeh_destroyObj_Event;
	}

	void Create_FreshMilkGlass ()
	{
		juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + FreshMilk, typeof(GameObject))) as GameObject;
		juice_glass_instance.transform.parent = this.transform;
		juice_glass_instance.transform.localPosition = new Vector3(-0.124f, -0.145f, -.1f);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.fresh_milk.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += Handle_JuiceGlassBeh_destroyObj_Event;
	}

    void Handle_JuiceGlassBeh_destroyObj_Event (object sender, System.EventArgs e)
    {
    	sceneManager.foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
    }
	
	private void PutGlassOnFoodTray(object sender, System.EventArgs e) {
		Debug.Log("PutGlassOnFoodTray");
		
		juice_glass_instance = null;
		juiceGlassBeh = null;

        if(sceneManager.foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            sceneManager.foodTrayBeh.goodsOnTray_List.Add((GoodsBeh)sender);
	}
}
