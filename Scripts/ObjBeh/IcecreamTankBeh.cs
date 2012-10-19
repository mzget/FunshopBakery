using UnityEngine;
using System.Collections;

public class IcecreamTankBeh : ObjectsBeh {
	
	private BakeryShop sceneManager;
	
	public tk2dAnimatedSprite icecreamValve;
	private GameObject icecream_Instance;
	private IcecreamBeh icecreamBeh;
	private Vector3 icecreamPos_0 = new Vector3(-0.014f, -.25f, -.2f);
	private Vector3 icecreamPos_1;
	private Vector3 icecreamPos_2;
	
	
	// Use this for initialization
	protected override void Start () {
		Debug.Log("Starting : IcecreamTankBeh");
		
		base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		base._canDragaable = false;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
	
	
	#region <!--- On Mouse Down.
	
	public override void OnMouseDown ()
	{
        //base.OnMouseDown ();
		
		if(icecream_Instance == null) {
			icecreamValve.Play();
			icecreamValve.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
				if(this.gameObject.name == "Strawberry_machine") {
					icecream_Instance = Instantiate(Resources.Load(ObjectsBeh.Icecream_ResourcePath + "StrawberryIcecream", typeof(GameObject))) as GameObject;
					icecream_Instance.transform.parent = this.transform;
					icecream_Instance.transform.localPosition = icecreamPos_0;
					icecream_Instance.gameObject.name = GoodDataStore.GoodsOrderList.strawberry_icecream.ToString();
					
					icecreamBeh = icecream_Instance.GetComponent<IcecreamBeh>();
					icecreamBeh.putObjectOnTray_Event += new System.EventHandler(icecreamBeh_putObjectOnTray_Event);
				}
			};
		}
	}

	void icecreamBeh_putObjectOnTray_Event(object sender, System.EventArgs e)
	{
		icecreamBeh = null;
		icecream_Instance = null;
		
        if(sceneManager.foodTrayBeh.goodsOnTray_List.Contains(sender as GoodsBeh) == false)
            sceneManager.foodTrayBeh.goodsOnTray_List.Add((GoodsBeh)sender);
	}
	
	
	#endregion
}
