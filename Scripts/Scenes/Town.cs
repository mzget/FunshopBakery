using UnityEngine;
using System.Collections;

public class Town : Mz_BaseScene {

	public GameObject town_bg_group;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];
    public GameObject flyingBird_group;
	public GameObject startingCar;
	public GameObject shop_body_sprite;
    public GameObject sheepBank_body_Obj;
    public tk2dAnimatedSprite bakeryShopDoorOpen_animated;
    public tk2dAnimatedSprite sheepBank_door_animated;
	public GameObject GUIMidcenter_anchor;
	public UpgradeOutsideManager upgradeOutsideManager;
    public CharacterAnimationManager characterAnimatedManage;
	
	//<!--- Game button.
//	public GameObject dressing_button_Obj;
//	public GameObject trophy_button_Obj;
//	public GameObject info_button_Obj;
//	public GameObject back_button_obj;
    //<!-- Texture2d resources.
    public Texture2D tk_coin_img;

	public enum OnGUIState { none = 0, DrawEditShopname, };
	public OnGUIState currentGUIState;
	Rect TopLeft_Anchor_GroupRect;          //<!-- TopLeft rectangle of screen.
	Rect drawPlayerName_rect;
	Rect drawShopName_rect;
	Rect drawPlayerMoney_rect;

	string username = "";
	Rect editShop_Textfield_rect = new Rect( 50, 60, 200, 50);
	Rect editShop_OKButton_rect = new Rect(10, 150, 100, 40);
	Rect editShop_CancelButton_rect = new Rect(160, 150, 100, 40);

	
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(this.InitializeAudio());
 		Mz_ResizeScale.ResizingScale(town_bg_group.transform);

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
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", -0.85f, "time", 8f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 

		iTween.MoveTo(startingCar, iTween.Hash("x", 5f, "Time", 20f, "easetype", iTween.EaseType.linear, "oncompletetarget", this.gameObject, "oncomplete", "OnStartingCarComplete"));
	}

	void OnStartingCarComplete ()
	{
		Destroy(startingCar);
	}

	protected new IEnumerator InitializeAudio ()
	{
    	base.InitializeAudio();
		
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();

        yield return null;
	}

	void Initialize_OnGUIDataFields () {
		TopLeft_Anchor_GroupRect  = new Rect(0, 0, 200 * ShopScene_GUIManager.Extend_heightScale, 150);
		drawPlayerName_rect  = new Rect(0, 20, 200 * ShopScene_GUIManager.Extend_heightScale, 40);
		drawShopName_rect  = new Rect(0, 65, 200 * ShopScene_GUIManager.Extend_heightScale, 40);
		drawPlayerMoney_rect = new Rect(0, 110, 200 * ShopScene_GUIManager.Extend_heightScale, 40);
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
		else if(Application.isWebPlayer || Application.isEditor) {
			if(_isDragMove) {
				float vector = currentPos.x - originalPos.x;
				if(vector < 0)
					Camera.main.transform.position += Vector3.right * Time.deltaTime * 2;
				else if(vector > 0) 
					Camera.main.transform.position += Vector3.left * Time.deltaTime * 2;
			}
		}
	}

    /// <summary>
    /// <!-- Gui region.
    /// </summary>
    private new void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));
		
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
		
		/// OnGUIState.DrawEditShopname.
		if(currentGUIState == OnGUIState.DrawEditShopname)
			this.DrawEditShopnameWindow();
    }

	void DrawEditShopnameWindow ()
	{
		GUI.BeginGroup(new Rect (Screen.width / 2 - 150, Main.GAMEHEIGHT / 2 - 100, 300, 200), "Edit shopname !", GUI.skin.window);
		{
			username = GUI.TextField(editShop_Textfield_rect, username, 24);

			if(GUI.Button(editShop_OKButton_rect, "OK")) {
				if(username != "" && username.Length >= 3) {
					Mz_StorageManage.ShopName = username;

					if(username == "Fulfill your greed") {
						Mz_StorageManage.AvailableMoney += 1000000;
                        characterAnimatedManage.PlayRampageAnimation();
					}
					else if(username == "Greed is bad") {
                        BakeryShop.NumberOfCansellItem.Clear();
                        for (int i = 0; i < 30; i++)
                        {
                            BakeryShop.NumberOfCansellItem.Add(i);
                        }
                        base.extendsStorageManager.SaveCanSellGoodListData();
                        characterAnimatedManage.PlayRampageAnimation();
					}

                    base.extendsStorageManager.SaveDataToPermanentMemory();

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

	public override void OnInput (string nameInput)
	{
		switch (nameInput) {
		case "Shop_body" : this.PlayBakeryShopOpenAnimation ();
			break;
		case "SheepBank_body" : this.PlaySheepBankOpenAnimation ();
			break;
		case "Back_button" :  
			if(Application.isLoadingLevel == false) {
                base.extendsStorageManager.SaveDataToPermanentMemory();
                this.OnDispose();
				
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
                Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
			break;
		case "Dress_button" : 
			if (Application.isLoadingLevel == false) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Dressing.ToString();
                Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
			}
			break;
        case "Decoration_button":
            this.characterAnimatedManage.RandomPlayGoodAnimation();
            StartCoroutine(ActiveDecorationBar());
			break;
		case "Trophy_button" : 
			if (!Application.isLoadingLevel) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.DisplayReward.ToString();
                Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
			}
			break;
		default:
			break;
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
            if (Application.isLoadingLevel == false) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.BakeryShop.ToString();
                Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        };
    }

    void PlaySheepBankOpenAnimation() {
        sheepBank_door_animated.Play();
        sheepBank_door_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
        {
            if(Application.isLoadingLevel == false) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Sheepbank.ToString();
                Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        };
    }

    public override void OnDispose()
    {
        base.OnDispose();
        //<!-- Clear static NumberOfCanSellItem.
        BakeryShop.NumberOfCansellItem.Clear();
    }
}
