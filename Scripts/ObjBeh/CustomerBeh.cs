using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerBeh : MonoBehaviour {
	
	private BakeryShop sceneManager;	
	public enum CustomerBeh_State { none = 1, ordering = 0, }
	public CustomerBeh_State currentCustomerBeh_State;

    public string[] animationClip_name = new string[] {
        "boy_001", "boy_002", "boy_003", "boy_004",
        "boy_005", "boy_006", "boy_007", "boy_008",
        "boy_009", "boy_010", "boy_011",
    };
    tk2dAnimatedSprite animatedSprite;

    public GameObject customerSprite_Obj;
    public List<CustomerOrderRequire> customerOrderRequire = new List<CustomerOrderRequire>();
    public int amount = 0;
	public int payMoney = 0;

	

	
	// Use this for initialization
	IEnumerator Start () {
        Debug.Log("Instancing Customer");
		
		sceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BakeryShop>();        
		
		this.RandomCustomerFace();

        yield return StartCoroutine(GenerateGoodOrder());

        _enableGUI = true;
		
		yield return null;
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
            maxGoodsType = 2;
//        else if(BakeryShop.gameLevel == 1)
//            maxGoodsType = 3;
//        else if(BakeryShop.gameLevel == 2)
//            maxGoodsType = 4;

        int r = Random.Range(1, maxGoodsType + 1);
        for (int i = 0; i < r; i++) {
			customerOrderRequire.Add(new CustomerOrderRequire() { 
				goods = new Goods(), number = Random.Range(1, 4)
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
	private void CheckGoodsObjInTray() {
		List<CustomerOrderRequire> goods_temp = new List<CustomerOrderRequire>();
		Goods temp_goods = null;
		int temp_counter = 0;
		
		for (int i = 0; i < customerOrderRequire.Count; i++) {				
			foreach(GoodsBeh e_goods in sceneManager.foodTrayBeh.goodsOnTray_List) {
				if(e_goods.name == customerOrderRequire[i].goods.name) { 		
					temp_goods = customerOrderRequire[i].goods;
					temp_counter += 1;
				}
			}

            goods_temp.Add(new CustomerOrderRequire() { goods = temp_goods, number = temp_counter });

            if (customerOrderRequire[i].number == goods_temp[i].number) {
                Debug.Log(goods_temp[i].goods.name + " : " + goods_temp[i].number);    
				
				temp_counter = 0;
				
				if(goods_temp.Count == customerOrderRequire.Count) {
                    OnManageGoodComplete(System.EventArgs.Empty);
				}					
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private bool _enableGUI = false;
    private Rect textbox_DisplayOrder;
	private float init_heightOfTextDisplay = 24;
	private void OnGUI() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/ Main.GAMEHEIGHT, 1));

        textbox_DisplayOrder = new Rect(ShopScene_GUIManager.viewPort_rect.width / 2 - (300 / 2), 0, 300, 200);

        if(_enableGUI) 
        {		
            //GUI.BeginGroup(new Rect(((Screen.width / 2) - (GUI_manager.midcenterGroup_rect.width / 2)), 0, GUI_manager.midcenterGroup_rect.width, Main.FixedGameHeight));
            GUI.BeginGroup(ShopScene_GUIManager.viewPort_rect);
            {
                if (currentCustomerBeh_State == CustomerBeh_State.ordering)
                {
                    GUI.BeginGroup(textbox_DisplayOrder, "Order", GUI.skin.window);
                    {
                        for (int i = 0; i < customerOrderRequire.Count; i++)
                        {
                            GUI.Label(new Rect(10, init_heightOfTextDisplay * (i + 1), textbox_DisplayOrder.width - 20, 24), customerOrderRequire[i].goods.name + " : " + customerOrderRequire[i].number, GUI.skin.textField);
                        }


                        if (GUI.Button(new Rect(textbox_DisplayOrder.width - 100, textbox_DisplayOrder.height - 50, 100, 50), "Done"))
                        {
                            CheckGoodsObjInTray();
                        }
                        else if (GUI.Button(new Rect(textbox_DisplayOrder.width - 220, textbox_DisplayOrder.height - 50, 100, 50), "Go away !"))
                        {
                            StartCoroutine(sceneManager.ExpelCustomer());
                        }
                    }
                    GUI.EndGroup();
                }
            }
			GUI.EndGroup();
        }
	}

    public void Dispose() {
        manageGoodsComplete_event -= sceneManager.currentCustomer_manageGoodsComplete_event;
        Destroy(customerSprite_Obj.gameObject);
        Destroy(this.gameObject);
    }
}
