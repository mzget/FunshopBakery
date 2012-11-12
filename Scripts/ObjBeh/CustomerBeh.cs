using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerBeh : MonoBehaviour {
	
	private BakeryShop sceneManager;	

    public string[] animationClip_name = new string[] {
        "boy_001", "boy_002", "boy_003", "boy_004",
        "boy_005", "boy_006", "boy_007", "boy_008",
        "boy_009", "boy_010", "boy_011",
    };
    tk2dAnimatedSprite animatedSprite;

    public GameObject customerSprite_Obj;
	public GameObject customerOrderingIcon_Obj;
    public List<CustomerOrderRequire> customerOrderRequire = new List<CustomerOrderRequire>();
    public int amount = 0;
	public int payMoney = 0;    

    private Rect textbox_DisplayOrder;
	

	
	// Use this for initialization
	IEnumerator Start () {
        Debug.Log("Instancing Customer");
		
		sceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BakeryShop>();        
		
		this.RandomCustomerFace();

        yield return StartCoroutine(GenerateGoodOrder());

        sceneManager.GenerateOrderGUI();
	}
	
	void RandomCustomerFace() {
		if(customerSprite_Obj) {
			animatedSprite = customerSprite_Obj.GetComponent<tk2dAnimatedSprite>();
			
        	int r = Random.Range(0, animationClip_name.Length);
			int id = animatedSprite.GetClipIdByName(animationClip_name[r]);
        	animatedSprite.Play(id);
		}
	}

    IEnumerator GenerateGoodOrder()
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
			customerOrderRequire.Add(new CustomerOrderRequire() { 
				goods = new Goods(),
				number = 1,	// Random.Range(1, 4),
			});
        }

        print("GenerateGoodOrder complete! " + "Type : " + customerOrderRequire.Count);

		yield return 0;

        this.CalculationPrice();
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
    
    public event System.EventHandler manageGoodsComplete_event;
	private void OnManageGoodComplete(System.EventArgs e) {
		if(manageGoodsComplete_event != null) {
			manageGoodsComplete_event(this, e);
		}
	}

	internal void CheckGoodsObjInTray() {
		List<CustomerOrderRequire> list_goodsTemp = new List<CustomerOrderRequire>();
		Goods temp_goods = null;
		int temp_counter = 0;
		
		for (int i = 0; i < customerOrderRequire.Count; i++) {				
			foreach(GoodsBeh e_goods in sceneManager.foodTrayBeh.goodsOnTray_List) {
				if(e_goods.name == customerOrderRequire[i].goods.name) { 		
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
				
				temp_counter = 0;
				
				if(list_goodsTemp.Count == customerOrderRequire.Count) {
                    OnManageGoodComplete(System.EventArgs.Empty);
				}					
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Dispose() {
        manageGoodsComplete_event -= sceneManager.currentCustomer_manageGoodsComplete_event;
        Destroy(customerSprite_Obj);
		Destroy(customerOrderingIcon_Obj);
        Destroy(this.gameObject);
    }
}
