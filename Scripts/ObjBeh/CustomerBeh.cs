using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerBeh : MonoBehaviour {
	
	private BakeryShop sceneManager;	
    private tk2dAnimatedSprite animatedSprite;
//	public tk2dAnimatedSprite AnimatedSprite { get {return animatedSprite;}}
	private int currentPlayAnimatedID = 0;

    public string[] animationClip_name = new string[] {
        "boy_001", "boy_002", "boy_003", "boy_004",
        "boy_005", "boy_006", "boy_007", "boy_008",
        "boy_009", "boy_010", "boy_011",
    };
	public string[] arr_mutterAnimationClip_name = new string[] {
		"boy001_mutter", "boy002_mutter", "boy003_mutter", "boy004_mutter", 
		"boy005_mutter", "boy006_mutter", "boy007_mutter", "boy008_mutter", 
		"boy009_mutter", "boy010_mutter", "boy011_mutter", 
	};
	
	public List<Goods> list_goodsBag;		// Use for shuffle bag goods obj.
    public GameObject customerSprite_Obj;
	public GameObject customerOrderingIcon_Obj;
    public List<CustomerOrderRequire> customerOrderRequire = new List<CustomerOrderRequire>();
    public int amount = 0;
	public int payMoney = 0;

	
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("Instancing Customer");

		sceneManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<BakeryShop> ();        

		StartCoroutine (RandomCustomerFace ());

		list_goodsBag = new List<Goods> (sceneManager.CanSellGoodLists);
		this.GenerateGoodOrder ();
	}
	
	private IEnumerator RandomCustomerFace() {
		if(customerSprite_Obj) {
			animatedSprite = customerSprite_Obj.GetComponent<tk2dAnimatedSprite>();
			
        	int r = Random.Range(0, animationClip_name.Length);
			currentPlayAnimatedID = animatedSprite.GetClipIdByName(animationClip_name[r]);
        	animatedSprite.Play(currentPlayAnimatedID);
		}

        yield return 0;
	}
	
	public void PlayRampage_animation ()
	{
		if (animatedSprite != null) {
			animatedSprite.Play(arr_mutterAnimationClip_name[currentPlayAnimatedID]);			
		}
	}

    private void GenerateGoodOrder()
    {		
        int maxGoodsType = 0;
//        if(BakeryShop.gameLevel == 0)
            maxGoodsType = 3;
//        else if(BakeryShop.gameLevel == 1)
//            maxGoodsType = 3;
//        else if(BakeryShop.gameLevel == 2)
//            maxGoodsType = 4;

        int r = Random.Range(1, maxGoodsType + 1);
        for (int i = 0; i < r; i++) {
			customerOrderRequire.Add(new CustomerOrderRequire() 
            { 
				goods = new Goods(),
				number = 1,	// Random.Range(1, 4),
			});
        }

        print("GenerateGoodOrder complete! " + "Type : " + customerOrderRequire.Count);

        this.CalculationPrice();
        sceneManager.GenerateOrderGUI();
	}

    private void CalculationPrice()
    {
        int[] prices = new int[3];
        int[] number = new int[3];
        for (int i = 0; i < customerOrderRequire.Count; i++)
        {
            prices[i] = customerOrderRequire[i].goods.price;
            number[i] = customerOrderRequire[i].number;
        }

        for(int j = 0; j < customerOrderRequire.Count; j++) {
            amount += prices[j] * number[j];
        }

        Debug.Log("CalculationPrice => amount : " + amount);
    }
    
    /// <summary>
    /// Manage goods complete Event handle.
    /// </summary>
    public event System.EventHandler manageGoodsComplete_event;
	private void OnManageGoodComplete(System.EventArgs e) {
		if(manageGoodsComplete_event != null) {
			manageGoodsComplete_event(this, e);
		}
	}

	internal void CheckGoodsObjInTray() {
		if(sceneManager.foodTrayBeh.goodsOnTray_List.Count == 0)
			return;
		if(sceneManager.foodTrayBeh.goodsOnTray_List.Count != customerOrderRequire.Count)
			return;
		
		List<CustomerOrderRequire> list_goodsTemp = new List<CustomerOrderRequire>();
		Goods temp_goods = null;
		int temp_counter = 0;
		
		for (int i = 0; i < customerOrderRequire.Count; i++) 
        {				
			foreach(GoodsBeh item in sceneManager.foodTrayBeh.goodsOnTray_List) 
			{
				if(item.name == customerOrderRequire[i].goods.name) { 		
					temp_goods = customerOrderRequire[i].goods;
					temp_counter += 1;
				}
			}

            list_goodsTemp.Add(new CustomerOrderRequire() { 
				goods = temp_goods, 
				number = temp_counter,
			});

            if (customerOrderRequire[i].number == list_goodsTemp[i].number) {
                Debug.Log(list_goodsTemp[i].goods.name + " : " + list_goodsTemp[i].number);    
								
				if(list_goodsTemp.Count == customerOrderRequire.Count) {
                    OnManageGoodComplete(System.EventArgs.Empty);
				}					
            }
			
			temp_goods = null;
			temp_counter = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Dispose() {
        manageGoodsComplete_event -= sceneManager.currentCustomer_manageGoodsComplete_event;
		list_goodsBag.Clear();
        Destroy(customerSprite_Obj);
		Destroy(customerOrderingIcon_Obj);
        Destroy(this);
    }
}
