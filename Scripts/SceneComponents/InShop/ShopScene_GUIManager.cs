using UnityEngine;
using System.Collections;

public class ShopScene_GUIManager : Mz_OnGUIManager {
	

    void Awake() {
        CalculateViewportScreen();
    } 

	// Use this for initialization
	void Start () {
        this.Initialize_OnGUIDataField();
	}

    private void Initialize_OnGUIDataField()
    {
        //<!-- initialize data field.

        trademark_Rect = new Rect(viewPort_rect.width - (350 * ShopScene_GUIManager.Extend_heightScale), 0, 350 * ShopScene_GUIManager.Extend_heightScale, 160);
        shopName_Rect = new Rect(140, 35, 180 * ShopScene_GUIManager.Extend_heightScale, 35);

        if (Screen.height != Main.GAMEHEIGHT) {
            shopName_Rect.x = shopName_Rect.x * ShopScene_GUIManager.Extend_heightScale;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    Rect shopName_Rect;
    Rect trademark_Rect;
	void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/ Main.GAMEHEIGHT, 1));

		GUI.BeginGroup(viewPort_rect);
		{
            //if(Application.loadedLevelName == Mz_BaseScene.SceneNames.BakeryShop.ToString())
            //    this.DrawPlayButton();

            GUI.BeginGroup(trademark_Rect);
            {
                GUI.Box(shopName_Rect, Mz_StorageManage.ShopName);
            }
            GUI.EndGroup();
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
