using UnityEngine;
using System.Collections;

public class IcecreamTankBeh : ObjectsBeh {
	
    public tk2dAnimatedSprite icecreamValve;
	private GameObject icecream_Instance;
	private IcecreamBeh icecreamBeh;
	private Vector3 icecreamPos_0 = new Vector3(-0.014f, -.25f, -3f);
	private Vector3 icecreamPos_1 = new Vector3(0, -.25f, -3f);
	private Vector3 icecreamPos_2 = new Vector3(-0.04f, -.25f, -3f);
	
	
	// Use this for initialization
	protected override void Start () {
		Debug.Log("IcecreamTankBeh ::" + this.gameObject.name);
		
		base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		base._canDragaable = false;
	}

    protected override void OnTouchDown()
    {
        if(icecream_Instance == null) {
			icecreamValve.Play();
			icecreamValve.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
				if(this.gameObject.name == BakeryShop.icecreamStrawberryTank_name) {
					icecream_Instance = Instantiate(Resources.Load(ObjectsBeh.Icecream_ResourcePath + "StrawberryIcecream", typeof(GameObject))) as GameObject;
					icecream_Instance.transform.parent = this.transform;
					icecream_Instance.transform.localPosition = icecreamPos_0;
					icecream_Instance.gameObject.name = GoodDataStore.FoodMenuList.Strawberry_icecream.ToString();
					
					icecreamBeh = icecream_Instance.GetComponent<IcecreamBeh>();
					icecreamBeh.putObjectOnTray_Event += new System.EventHandler(icecreamBeh_putObjectOnTray_Event);
                    icecreamBeh.destroyObj_Event += new System.EventHandler(icecreamBeh_destroyObj_Event);
				}
				else if(this.gameObject.name == BakeryShop.icecreamVanillaTank_name) {
					icecream_Instance = Instantiate(Resources.Load(ObjectsBeh.Icecream_ResourcePath + "VanillaIcecream", typeof(GameObject))) as GameObject;
					icecream_Instance.transform.parent = this.transform;
					icecream_Instance.transform.localPosition = icecreamPos_1;
					icecream_Instance.gameObject.name = GoodDataStore.FoodMenuList.Vanilla_icecream.ToString();
					
					icecreamBeh = icecream_Instance.GetComponent<IcecreamBeh>();
					icecreamBeh.putObjectOnTray_Event += new System.EventHandler(icecreamBeh_putObjectOnTray_Event);
                    icecreamBeh.destroyObj_Event += new System.EventHandler(icecreamBeh_destroyObj_Event);
				}
				else if(this.gameObject.name == BakeryShop.icecreamChocolateTank_name) {
					icecream_Instance = Instantiate(Resources.Load(ObjectsBeh.Icecream_ResourcePath + "ChocolateIcecream", typeof(GameObject))) as GameObject;
					icecream_Instance.transform.parent = this.transform;
					icecream_Instance.transform.localPosition = icecreamPos_2;
					icecream_Instance.gameObject.name = GoodDataStore.FoodMenuList.Chocolate_icecream.ToString();
					
					icecreamBeh = icecream_Instance.GetComponent<IcecreamBeh>();
					icecreamBeh.putObjectOnTray_Event += new System.EventHandler(icecreamBeh_putObjectOnTray_Event);
                    icecreamBeh.destroyObj_Event += new System.EventHandler(icecreamBeh_destroyObj_Event);
				}
			};
		}

		base.OnTouchDown();
    }

	void icecreamBeh_putObjectOnTray_Event (object sender, System.EventArgs e)
	{		
		GoodsBeh obj = sender as GoodsBeh;
		if (sceneManager.foodTrayBeh.goodsOnTray_List.Contains (obj) == false && sceneManager.foodTrayBeh.goodsOnTray_List.Count < FoodTrayBeh.MaxGoodsCapacity) {
			sceneManager.foodTrayBeh.goodsOnTray_List.Add (obj);			
			sceneManager.foodTrayBeh.ReCalculatatePositionOfGoods();

			//<!-- Setting original position.
			obj.originalPosition = obj.transform.position;
			
			icecreamBeh = null;
			icecream_Instance = null;
		} else {
			Debug.LogWarning("Goods on tray have to max capacity.");

			obj.transform.position = obj.originalPosition;
		}
	}
    private void icecreamBeh_destroyObj_Event(object sender, System.EventArgs e) {
        sceneManager.foodTrayBeh.goodsOnTray_List.Remove(sender as GoodsBeh);
		sceneManager.foodTrayBeh.ReCalculatatePositionOfGoods();
    }
}
