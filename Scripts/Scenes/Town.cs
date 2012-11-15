using UnityEngine;
using System.Collections;

public class Town : Mz_BaseScene {
	//<!-- Dont destroy obj.
    private GameObject bankBeh_obj;


	public GameObject town_bg_group;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];
    public GameObject flyingBird_group;
	public GameObject shop_body_sprite;
    public GameObject sheepBank_body_Obj;
    public tk2dAnimatedSprite bakeryShopDoorOpen_animated;
    public tk2dAnimatedSprite sheepBank_door_animated;
	public GameObject GUIMidcenter_anchor;
	public UpgradeOutsideManager upgradeOutsideManager;
	
	//<!--- Game button.
//	public GameObject dressing_button_Obj;
//	public GameObject trophy_button_Obj;
//	public GameObject info_button_Obj;
//	public GameObject back_button_obj;
    //<!-- Texture2d resources.
    public Texture2D tk_coin_img;
	public Texture2D dress_img;
	public Texture2D trophy_img;
	public Texture2D info_img;
	public Texture2D backIcon_img;

	public enum OnGUIState { none = 0, DrawEditShopname, };
	public OnGUIState currentGUIState;
	Rect TopLeft_Anchor_GroupRect;          //<!-- TopLeft rectangle of screen.
	Rect drawPlayerName_rect;
	Rect drawShopName_rect;
	Rect drawPlayerMoney_rect;
	
	Rect topRight_Anchor_GroupRect ;
	Rect drawDress_ButtonRect; 
	Rect drawTrophy_ButtonRect;
	Rect drawInfo_ButtonRect;
	Rect drawBack_ButtonRect;

	string username = "";
	Rect editShop_Textfield_rect = new Rect( 50, 60, 200, 50);
	Rect editShop_OKButton_rect = new Rect(10, 150, 100, 40);
	Rect editShop_CancelButton_rect = new Rect(160, 150, 100, 40);

	
	// Use this for initialization
	void Start () 
	{
		base.InitializeAudio ();
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();

 		Mz_ResizeScale.ResizingScale(town_bg_group.transform);

		ShopScene_GUIManager.CalculateViewportScreen();

        //StartCoroutine(InitializeBankBeh());

		this.Initialize_OnGUIDataFields ();
        this.upgradeOutsideManager.InitializeDecorationObjects();

		if (SheepBank.HaveUpgradeOutSide) {
            StartCoroutine(this.ActiveDecorationBar());
			SheepBank.HaveUpgradeOutSide = false;
		}
		else
			StartCoroutine(this.UnActiveDecorationBar());

        iTween.MoveTo(flyingBird_group, iTween.Hash("x", 5f, "time", 20f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.loop));
		iTween.MoveTo(cloudAndFog_Objs[0].gameObject, iTween.Hash("y", -.1f, "time", 2f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[1].gameObject, iTween.Hash("y", -.1f, "time", 3f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[2].gameObject, iTween.Hash("y", -.1f, "time", 4f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", .3f, "time", 5f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
	}

	private IEnumerator InitializeBankBeh ()
	{		
		bankBeh_obj = GameObject.Find("BankBeh");

		if (bankBeh_obj == null) {
			bankBeh_obj = new GameObject ("BankBeh", typeof(BankBeh));
		}
		/// Add event. Get interest from bank.
		BankBeh.GiveInterest_event += base.Handle_GiveInterest_event;

		yield return 0;
	}

	void Initialize_OnGUIDataFields () {
		TopLeft_Anchor_GroupRect  = new Rect(0, 0, 200 * ShopScene_GUIManager.extend_heightScale, 150);
		drawPlayerName_rect  = new Rect(0, 20, 200 * ShopScene_GUIManager.extend_heightScale, 40);
		drawShopName_rect  = new Rect(0, 65, 200 * ShopScene_GUIManager.extend_heightScale, 40);
		drawPlayerMoney_rect = new Rect(0, 110, 200 * ShopScene_GUIManager.extend_heightScale, 40);
		
		topRight_Anchor_GroupRect = new Rect(Screen.width - 400, 0, 400 * ShopScene_GUIManager.extend_heightScale, 200);
		drawDress_ButtonRect = new Rect(10, 20, 80 * ShopScene_GUIManager.extend_heightScale, 80);
		drawTrophy_ButtonRect = new Rect(100, 20, 80 * ShopScene_GUIManager.extend_heightScale, 80);
		drawInfo_ButtonRect = new Rect(190, 20, 80 * ShopScene_GUIManager.extend_heightScale, 80);
		drawBack_ButtonRect = new Rect(280, 20, 80 * ShopScene_GUIManager.extend_heightScale, 80);

		if (Screen.height != Main.GAMEHEIGHT) {
			topRight_Anchor_GroupRect.x = Screen.width - (400 * ShopScene_GUIManager.extend_heightScale);
			drawDress_ButtonRect.x = drawDress_ButtonRect.x * ShopScene_GUIManager.extend_heightScale;
			drawTrophy_ButtonRect.x = drawTrophy_ButtonRect.x * ShopScene_GUIManager.extend_heightScale;
			drawInfo_ButtonRect.x = drawInfo_ButtonRect.x * ShopScene_GUIManager.extend_heightScale;
			drawBack_ButtonRect.x = drawBack_ButtonRect.x * ShopScene_GUIManager.extend_heightScale;
		}
	}

	#region <!-- Decoration upgrade bar.

	IEnumerator ActiveDecorationBar ()
	{
		yield return StartCoroutine(this.SettingGUIMidcenter(true));
		iTween.MoveTo(GUIMidcenter_anchor.gameObject, iTween.Hash("position", new Vector3(0, 0, 1), "islocal", true, "time", 1f, "easetype", iTween.EaseType.spring));
		upgradeOutsideManager.ActiveRoof();
	}
	IEnumerator UnActiveDecorationBar ()
	{
		yield return new WaitForEndOfFrame();
		iTween.MoveTo(GUIMidcenter_anchor.gameObject,
			iTween.Hash("position", new Vector3(0, -1, 1), "islocal", true, "time", 1f, "easetype", iTween.EaseType.spring,
			"oncomplete", "TweenDownComplete", "oncompletetarget", this.gameObject));		 
	}
	void TweenDownComplete() {
		StartCoroutine(this.SettingGUIMidcenter(false));
	}
	IEnumerator SettingGUIMidcenter (bool active)
	{
		yield return new WaitForEndOfFrame();
		GUIMidcenter_anchor.SetActiveRecursively(active);
	}

	#endregion
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
		base.ImplementTouchPostion();
		
		if(Camera.main.transform.position.x > 2.66f) 
			Camera.main.transform.position = new Vector3(2.66f, Camera.main.transform.position.y, Camera.main.transform.position.z); 	//Vector3.left * Time.deltaTime;
		else if(Camera.main.transform.position.x < 0) 
			Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);	 //Vector3.right * Time.deltaTime;
	}

	protected override void MovingCameraTransform ()
	{	
		base.MovingCameraTransform();

		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			float speed = Time.fixedDeltaTime * 0.2f;
			// Get movement of the finger since last frame   
			Vector2 touchDeltaPosition = touch.deltaPosition;
			// Move object across XY plane       
			//transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
			Camera.main.transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
		}
		else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor) {
			if(_isDragMove) {
				float vector = currentPos.x - originalPos.x;
				if(vector < 0)
					Camera.main.transform.position += Vector3.right * Time.deltaTime * 2;
				else if(vector > 0) 
					Camera.main.transform.position += Vector3.left * Time.deltaTime * 2;
			}
		}
	}

	#region <!-- Gui region.

    void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/ Main.GAMEHEIGHT, 1));
		
		/// TopLeft_Anchor_GroupRect.
        GUI.BeginGroup(TopLeft_Anchor_GroupRect, "TopLeft anchor", GUI.skin.window);
        {
            GUI.Box(drawPlayerName_rect, Mz_StorageManage.Username);
			if(GUI.Button(drawShopName_rect, Mz_StorageManage.ShopName)) {
				currentGUIState = OnGUIState.DrawEditShopname;
				base.UpdateTimeScale(0);
			}
            GUI.Box(drawPlayerMoney_rect, new GUIContent(Mz_StorageManage.AvailableMoney.ToString(), tk_coin_img));
        }
        GUI.EndGroup();      
		
		/// topRight_Anchor_GroupRect
		GUI.BeginGroup(topRight_Anchor_GroupRect, "TopRight anchor.", GUI.skin.window);
		{
			if(GUI.Button(drawDress_ButtonRect, new GUIContent(dress_img))) {
				if (Application.isLoadingLevel == false) {
	                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Dressing.ToString();
	                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            	}
			}
			else if(GUI.Button(drawTrophy_ButtonRect, new GUIContent(trophy_img))) {
				
			}
			else if(GUI.Button(drawInfo_ButtonRect, new GUIContent(info_img))) {

			}
			else if(GUI.Button(drawBack_ButtonRect, new GUIContent(backIcon_img))) {
				if(Application.isLoadingLevel == false) {
                    Mz_StorageManage.Save();
					//<!-- Clear static NumberOfCanSellItem.
					BakeryShop.NumberOfCansellItem.Clear();
					
	                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
	                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
	            }
			}

			if(GUI.Button(new Rect(10, 130, 100,40), "decoration")) {
				StartCoroutine(this.ActiveDecorationBar());
			}
		}
		GUI.EndGroup();
		
		/// OnGUIState.DrawEditShopname.
		if(currentGUIState == OnGUIState.DrawEditShopname)
			this.DrawEditShopnameWindow();
    }

	void DrawEditShopnameWindow ()
	{
		GUI.BeginGroup(new Rect (Screen.width / 2 - 150, Main.GAMEHEIGHT / 2 - 100, 300, 200), "Edit shopname !", GUI.skin.window);
		{
			username = GUI.TextField(editShop_Textfield_rect, username, 13);

			if(GUI.Button(editShop_OKButton_rect, "OK")) {
				if(username != "" && username.Length >= 5) {
					Mz_StorageManage.ShopName = username;

					if(username == "Greed is bad") {
						Mz_StorageManage.AvailableMoney += 1000000;
					}

					Mz_StorageManage.Save();

					currentGUIState = OnGUIState.none;
					base.UpdateTimeScale(1);
				}
			}
			else if(GUI.Button(editShop_CancelButton_rect, "Cancel")) {
				currentGUIState = OnGUIState.none;
				base.UpdateTimeScale(1);
			}
		}
		GUI.EndGroup();
	}

	#endregion

	public override void OnInput (string nameInput)
	{
		if (nameInput == shop_body_sprite.gameObject.name) {
				this.PlayBakeryShopOpenAnimation ();
				return;
		}
//        else if (nameInput == back_button_obj.name) {
//            if(Application.isLoadingLevel == false) {
//				//<!-- Clear static NumberOfCanSellItem.
//				BakeryShop.NumberOfCansellItem.Clear();
//				
//				
//                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
//                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
//            }
//        }
//        else if (nameInput == dressing_button_Obj.name) {
//            if (Application.isLoadingLevel == false) {
//                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Dressing.ToString();
//                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
//            }
//        }
		if (nameInput == sheepBank_body_Obj.name) {
				this.PlaySheepBankOpenAnimation ();
				return;
		}

		if (GUIMidcenter_anchor.active) {
			switch (nameInput) {
			case "Close_button": StartCoroutine(this.UnActiveDecorationBar());
				break;
			case UpgradeOutsideManager.Roof_button : upgradeOutsideManager.ActiveRoof();
				break;
			case UpgradeOutsideManager.Awning_button : upgradeOutsideManager.ActiveAwning();
				break;
			case UpgradeOutsideManager.Table_button: upgradeOutsideManager.ActiveTable();
				break;
			case UpgradeOutsideManager.Accessories_button: upgradeOutsideManager.ActiveAccessories();
				break;
			case "None_button" : upgradeOutsideManager.HaveNoneCommand();
				break;
			case "Previous_button" : upgradeOutsideManager.HavePreviousPageCommand();
				break;
			case "Next_button" : upgradeOutsideManager.HaveNextPageCommand();
				break;
            case "Block_00": upgradeOutsideManager.BuyDecoration("Block_00");
                break;
            case "Block_01": upgradeOutsideManager.BuyDecoration("Block_01");
                break;
            case "Block_02": upgradeOutsideManager.BuyDecoration("Block_02");
                break;
            case "Block_03": upgradeOutsideManager.BuyDecoration("Block_03");
                break;
            case "Block_04": upgradeOutsideManager.BuyDecoration("Block_04");
                break;
            case "Block_05": upgradeOutsideManager.BuyDecoration("Block_05");
                break;
            case "Block_06": upgradeOutsideManager.BuyDecoration("Block_06");
                break;
			default:
			break;
			}
		}
	}

    void PlayBakeryShopOpenAnimation()
    {
        bakeryShopDoorOpen_animated.Play();
        bakeryShopDoorOpen_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
        {
            if (Application.isLoadingLevel == false)
            {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.BakeryShop.ToString();
                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        };
    }

    void PlaySheepBankOpenAnimation() {
        sheepBank_door_animated.Play();
        sheepBank_door_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
        {
            if(Application.isLoadingLevel == false) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Sheepbank.ToString();
                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        };
    }
}
