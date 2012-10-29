using UnityEngine;
using System.Collections;

public class ShopScene_GUIManager : MonoBehaviour {
	
	private Mz_BaseScene baseScene;

    public static Rect viewPort_rect;
	public static Rect midcenterGroup_rect = new Rect(0, 0, Main.GAMEWIDTH, Main.GAMEHEIGHT);	
	//<!--- Equation finding scale == x = screen.height/ main.fixed.
	public static float extend_heightScale;
    public static void CalculateViewportScreen() {    
		// Calculation height of screen.
		if(Screen.height == Main.FixedGameHeight) {
			extend_heightScale = 1;
		}
		else {
			extend_heightScale =  Screen.height / Main.FixedGameHeight;
			
			midcenterGroup_rect.height = Main.FixedGameHeight * extend_heightScale;
			midcenterGroup_rect.width = Main.FixedGameWidth * extend_heightScale;
		}
		
        viewPort_rect = new Rect(((Screen.width / 2) - (midcenterGroup_rect.width / 2)), 0, midcenterGroup_rect.width, Main.FixedGameHeight);
    }


    void Awake() {
        CalculateViewportScreen();
    } 

	// Use this for initialization
	void Start () {
		baseScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Mz_BaseScene>();
		bakeryShop_scene = baseScene as BakeryShop;

        this.Initialize_OnGUIDataField();
	}

    private void Initialize_OnGUIDataField()
    {
        //<!-- initialize data field.
        textbox_DisplayOrder_rect = new Rect(midcenterGroup_rect.width / 2 - 200, 0, 400, 150);

        trademark_Rect = new Rect(viewPort_rect.width - (350 * ShopScene_GUIManager.extend_heightScale), 0, 350 * ShopScene_GUIManager.extend_heightScale, 160);
        shopName_Rect = new Rect(140, 35, 180 * ShopScene_GUIManager.extend_heightScale, 35);

        if (Screen.height != Main.GAMEHEIGHT) {
            shopName_Rect.x = shopName_Rect.x * ShopScene_GUIManager.extend_heightScale;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    BakeryShop bakeryShop_scene;
    Rect shopName_Rect;
    Rect trademark_Rect;
	void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/ Main.GAMEHEIGHT, 1));

		GUI.BeginGroup(viewPort_rect);
		{
            //if(Application.loadedLevelName == Mz_BaseScene.SceneNames.BakeryShop.ToString())
            //    this.DrawPlayButton();

            GUI.BeginGroup(trademark_Rect, "Trademark", GUIStyle.none);
            {
                GUI.Box(shopName_Rect, StorageManage.ShopName);
            }
            GUI.EndGroup();

            if(bakeryShop_scene.currentGamePlayState == BakeryShop.GamePlayState.calculationPrice) {
                this.DrawCalculationPrice();
            }
			else if(bakeryShop_scene.currentGamePlayState == BakeryShop.GamePlayState.giveTheChange) {
				this.DrawEquationOfGiveTheChange();
			}
		}
		GUI.EndGroup();
	}

    Rect textbox_DisplayOrder_rect;
    Rect showPriceEquation_rect = new Rect(10, 25, 380, 30);
    private void DrawCalculationPrice()
    {
        GUI.BeginGroup(textbox_DisplayOrder_rect, "Calculation Price.", GUI.skin.window);
        {
            string[] goodsTypes = new string[3];
            int[] goodsPrice = new int[3];
            int[] amountGoods = new int[3];
            for (int i = 0; i < bakeryShop_scene.currentCustomer.customerOrderRequire.Count; i++) {
                goodsTypes[i] = bakeryShop_scene.currentCustomer.customerOrderRequire[i].goods.name;
                goodsPrice[i] = bakeryShop_scene.currentCustomer.customerOrderRequire[i].goods.price;
                amountGoods[i] = bakeryShop_scene.currentCustomer.customerOrderRequire[i].number;
                
                GUI.Box(new Rect(showPriceEquation_rect.x, ((showPriceEquation_rect.height + 10) * i) + 25, showPriceEquation_rect.width, showPriceEquation_rect.height),
                    goodsTypes[i] + " : Price : " + goodsPrice[i] + " * " + amountGoods[i]);
            }
        }
        GUI.EndGroup();
    }
	
	private void DrawEquationOfGiveTheChange() {
		GUI.BeginGroup(textbox_DisplayOrder_rect, "Give the change.", GUI.skin.window);
        {
			GUI.Box(new Rect(showPriceEquation_rect.x, 25, showPriceEquation_rect.width, showPriceEquation_rect.height), "Total : " + bakeryShop_scene.currentCustomer.amount);
			GUI.Box(new Rect(showPriceEquation_rect.x, (25 + 5) * 2, showPriceEquation_rect.width, showPriceEquation_rect.height), "Receive : " + bakeryShop_scene.currentCustomer.payMoney);
			GUI.Box(new Rect(showPriceEquation_rect.x, (25 + 5) * 3, showPriceEquation_rect.width, showPriceEquation_rect.height), "Give the change = ?");
        }
        GUI.EndGroup();
	}
	
//	public void DrawPlayButton() {				
//        if (bakeryShop_scene.currentGamePlayState != BakeryShop.GamePlayState.hasNewCustomer)
//            show_play_button = true;
//        else
//            show_play_button = false;
//
//        GUI.enabled = show_play_button;
//        if (GUI.Button(new Rect(0, 0, 128, 64), "play")) {
//            bakeryShop_scene.currentGamePlayState = BakeryShop.GamePlayState.hasNewCustomer;
//            bakeryShop_scene.CreateCustomer();
//        }
//        GUI.enabled = true;
//	}
}
