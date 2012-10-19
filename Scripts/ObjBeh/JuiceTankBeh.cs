using UnityEngine;
using System.Collections;

public class JuiceTankBeh : ObjectsBeh {
	
	public const string PineappleJuiceTank = "PineappleJuiceTank";
	public const string AppleJuiceTank = "AppleJuiceTank";
	public const string OrangeJuiceTank = "OrangeJuiceTank";
	public const string PineappleJuice = "PineappleJuice";
	public const string AppleJuice = "AppleJuice";
	public const string OrangeJuice = "OrangeJuice";
	
	
	private BakeryShop sceneManager;
	
    //<!--- pineapple glass local position == vector3(-1.12f, -.48f, -.2f).
    //<!--- apple glass local pos == (-.815f, -.48f, -.2f).
    //<!--- orange glass local pos == (-.53f, -.48f, -.2f).	
    private GameObject juice_glass_instance;
	private GlassBeh juiceGlassBeh;


	// Use this for initialization
    protected override void Start()
    {
        base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
//		if(this.gameObject.name == PineappleJuiceTank) 
//			juice_glass_prefab = Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + PineappleJuice, typeof(GameObject)) as GameObject;
//		else if(this.gameObject.name == AppleJuiceTank)
//			juice_glass_prefab = Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + AppleJuice, typeof(GameObject)) as GameObject;
//		else if(this.gameObject.name == OrangeJuiceTank)
//			juice_glass_prefab = Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + OrangeJuice, typeof(GameObject)) as GameObject;
			
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnMouseDown()
    {
        //base.OnMouseDown();

        if(juice_glass_instance == null) {
			if(this.gameObject.name == PineappleJuiceTank) 
				this.Create_PineappleJuiceGlass();
        }
    }

    private void Create_PineappleJuiceGlass()
    {
        juice_glass_instance = Instantiate(Resources.Load(ObjectsBeh.SouseMachine_ResourcePath + PineappleJuice, typeof(GameObject))) as GameObject;
        juice_glass_instance.transform.parent = this.transform;
        juice_glass_instance.transform.localPosition = new Vector3(0.01f, -0.325f, 0);
		juice_glass_instance.gameObject.name = GoodDataStore.GoodsOrderList.pineapple_juice.ToString();
		
		juiceGlassBeh = juice_glass_instance.GetComponent<GlassBeh>();
		juiceGlassBeh.putObjectOnTray_Event += new System.EventHandler(PutGlassOnFoodTray);
		juiceGlassBeh.destroyObj_Event += HandleJuiceGlassBehdestroyObj_Event;
    }

    void HandleJuiceGlassBehdestroyObj_Event (object sender, System.EventArgs e)
    {
        ObjectsBeh.ResetData();
    	sceneManager.foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
    }
	
	private void PutGlassOnFoodTray(object sender, System.EventArgs e) {
		Debug.Log("PutGlassOnFoodTray");
		
		juice_glass_instance = null;
		juiceGlassBeh = null;
        ObjectsBeh.ResetData();

        if(sceneManager.foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            sceneManager.foodTrayBeh.goodsOnTray_List.Add((GoodsBeh)sender);
	}
}
