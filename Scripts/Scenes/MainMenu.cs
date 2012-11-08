using UnityEngine;
using System.Collections;

public class MainMenu : Mz_BaseScene {

    public GameObject cloud_Obj;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];
    public GameObject baseBuilding_Obj;

    public Transform mainmenu_Group;
    public Transform newgame_Group;
    public Transform initializeNewGame_Group;
	InitializeNewShop initializeNewShop;
    public Transform loadgame_Group;
    public GameObject back_button;
    //<!--- Main menu group.
    public GameObject createNewShop_button;
    public GameObject loadShop_button;
    //<!--- Newgame && Loadgame.

    private Hashtable moveDownTransform_Data = new Hashtable();
    private Hashtable moveUpTransform_Data = new Hashtable();

	//<!-- Quality setting.
//	string[] qualities_list;
    
    public GUISkin mainmenu_Skin;
    public enum SceneState { none = 0, showOption, showNewGame, showNewShop, showLoadGame, };
    private SceneState sceneState;

    private string username = string.Empty;
    private string shopName = "";

    private bool _isNullUsernameNotification = false;
    private bool _isDuplicateUsername = false;
    private bool _isFullSaveGameSlot;
    private string player_1;
    private string player_2;
    private string player_3;

	public bool _showSkinLayout;
	Rect newgame_Textfield_rect;
	Rect newShopName_rect;
	
	float group_width = 400;
	Rect showSaveGameSlot_GroupRect;
	Rect slot_1Rect;
	Rect slot_2Rect;
	Rect slot_3Rect;

	

	// Use this for initialization
	void Start () {
		moveDownTransform_Data.Add("position", new Vector3(0, 0, -2));
		moveDownTransform_Data.Add("time", 1f);
		moveDownTransform_Data.Add("easetype", iTween.EaseType.spring);
		
		moveUpTransform_Data.Add("position", new Vector3(0, 2, -2));
		moveUpTransform_Data.Add("time", 1f);
		moveUpTransform_Data.Add("easetype", iTween.EaseType.linear);
		
		base.InitializeAudio();

        Mz_ResizeScale.ResizingScale(cloud_Obj.transform);
        Mz_ResizeScale.ResizingScale(baseBuilding_Obj.transform);
        ShopScene_GUIManager.CalculateViewportScreen();

        iTween.MoveTo(mainmenu_Group.gameObject, moveDownTransform_Data);

        newgame_Group.gameObject.SetActiveRecursively(false);
		initializeNewShop = initializeNewGame_Group.GetComponent<InitializeNewShop>();
        initializeNewGame_Group.gameObject.SetActiveRecursively(false);
        loadgame_Group.gameObject.SetActiveRecursively(false);
        back_button.gameObject.active = false;
		
		//<!-- get name quality.
//		qualities_list = QualitySettings.names;
		StartCoroutine(this.AutomaticSetup_QualitySetting());
		
		this.RecalculateOnGUI_DataFields();
		
		iTween.MoveTo(cloudAndFog_Objs[0].gameObject, iTween.Hash("y", 0f, "time", 3f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[1].gameObject, iTween.Hash("y", 0.2f, "time", 3.5f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[2].gameObject, iTween.Hash("y", 0.5f, "time", 4f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", .3f, "time", 5f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
	}
	
	private IEnumerator AutomaticSetup_QualitySetting() {
#if UNITY_IPHONE
		if(iPhone.generation == iPhoneGeneration.iPad1Gen || 
			iPhone.generation == iPhoneGeneration.iPhone3G || 
			iPhone.generation == iPhoneGeneration.iPhone3GS) {
			QualitySettings.SetQualityLevel(0);	
		}
		else {
			QualitySettings.SetQualityLevel(3);
		}
#elif UNITY_ANDROID
        QualitySettings.SetQualityLevel(3);
#endif

		yield return null;
	}

	void RecalculateOnGUI_DataFields ()
	{
		newgame_Textfield_rect = new Rect((ShopScene_GUIManager.viewPort_rect.width / 2) - 150, ShopScene_GUIManager.viewPort_rect.height / 2 + 58, 300, 82);
		newShopName_rect = new Rect((ShopScene_GUIManager.viewPort_rect.width / 2) - 138, ShopScene_GUIManager.viewPort_rect.height / 2 - 110, 400, 80);
		
		group_width = 400 * ShopScene_GUIManager.extend_heightScale;
		showSaveGameSlot_GroupRect = new Rect((ShopScene_GUIManager.viewPort_rect.width/2) - (group_width/2), (Main.GAMEHEIGHT / 2) - 70, group_width, 300);
		slot_1Rect = new Rect(35, 12, group_width - 60, 80);
		slot_2Rect  = new Rect(35, 112, group_width - 60, 80);
		slot_3Rect = new Rect(35, 212, group_width - 60, 80);
		
		if(Screen.height != Main.GAMEHEIGHT) {
			newgame_Textfield_rect.width = newgame_Textfield_rect.width * ShopScene_GUIManager.extend_heightScale;
			newgame_Textfield_rect.x = (ShopScene_GUIManager.viewPort_rect.width / 2) - newgame_Textfield_rect.width / 2;
			
			newShopName_rect.width = newShopName_rect.width * ShopScene_GUIManager.extend_heightScale;
			newShopName_rect.x = (ShopScene_GUIManager.viewPort_rect.width / 2) - 138 * ShopScene_GUIManager.extend_heightScale;
			
			slot_1Rect.width = group_width - (60 * ShopScene_GUIManager.extend_heightScale);
			slot_1Rect.x = slot_1Rect.x * ShopScene_GUIManager.extend_heightScale;
			
			slot_2Rect.width = group_width - (60 * ShopScene_GUIManager.extend_heightScale);
			slot_2Rect.x = slot_2Rect.x * ShopScene_GUIManager.extend_heightScale;
			
			slot_3Rect.width = group_width - (60 * ShopScene_GUIManager.extend_heightScale);
			slot_3Rect.x = slot_3Rect.x * ShopScene_GUIManager.extend_heightScale;
		}
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();

        if (username == "MzReset")
        {
            PlayerPrefs.DeleteAll();
            username = string.Empty;
            Mz_StorageManage.SaveSlot = 0;
        }
	}

	#region <!-- OnGUI Section.

	private void OnGUI() {
        player_1 = PlayerPrefs.GetString(1 + Mz_StorageManage.KEY_USERNAME);
        player_2 = PlayerPrefs.GetString(2 + Mz_StorageManage.KEY_USERNAME);
        player_3 = PlayerPrefs.GetString(3 + Mz_StorageManage.KEY_USERNAME);

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));
		
		GUI.BeginGroup(ShopScene_GUIManager.viewPort_rect);
        {
            if(_showSkinLayout) {
                GUI.Box(new Rect(0, 0, ShopScene_GUIManager.viewPort_rect.width, ShopScene_GUIManager.viewPort_rect.height), "Skin layout", GUI.skin.box);
            }

            if(sceneState == SceneState.showNewGame) {
                this.DrawNewGameTextField();                
            }
            else if(sceneState == SceneState.showNewShop) {
                this.DrawNewShopGUI();
            }
            else if(sceneState == SceneState.showLoadGame) {                
                // Call ShowSaveGameSlot Method.
                this.ShowSaveGameSlot(_isFullSaveGameSlot);
            }
            else if(sceneState == SceneState.none) {
                _isDuplicateUsername = false;
                _isNullUsernameNotification = false;
                _isFullSaveGameSlot = false;
                username = "";
                shopName = "";
            }

		    string notificationText = "";
		    string dublicateNoticeText = "";

            //notificationText = "Please Fill Your Username. \n ��س������ͼ�����";
            //dublicateNoticeText = "This name already exists. \n ���͹������������";

			notificationText = "Please Fill Your Username.";
			dublicateNoticeText = "This name already exists.";

            mainmenu_Skin.textField.normal.textColor = Color.white;
            Rect notification_Rect = new Rect(ShopScene_GUIManager.viewPort_rect.width / 2 - 200, 0, 400, 64);

            if (_isNullUsernameNotification)           
                GUI.Box(notification_Rect, notificationText, mainmenu_Skin.textField);
            if (_isDuplicateUsername)
                GUI.Box(notification_Rect, dublicateNoticeText, mainmenu_Skin.textField);
        
        }
        GUI.EndGroup();
    }
    
    private void DrawNewShopGUI()
    {
        shopName = GUI.TextField(newShopName_rect, shopName, 13, GUI.skin.textArea);
    }

    private void DrawNewGameTextField()
    {
        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return)
        {
            audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);
                    
            this.CheckUserNameFormInput();
        }
        
        //<!-- "Please Insert Username !".
        GUI.SetNextControlName("Username");
        username = GUI.TextField(newgame_Textfield_rect, username, 13, GUI.skin.textArea);

        if (GUI.GetNameOfFocusedControl() == string.Empty || GUI.GetNameOfFocusedControl() == "")
        {
            GUI.FocusControl("Username");
        }
	}

	#endregion

    private void CheckUserNameFormInput()
    {
        username.Trim('\n');

        if (username == "" || username == string.Empty || username == null) {
            Debug.LogWarning("Username == null");
	        _isNullUsernameNotification = true;
            _isDuplicateUsername = false;
	    }
        else if (username == player_1 || username == player_2 || username == player_3) {
            Debug.LogWarning("Duplicate Username");
	        _isDuplicateUsername = true;
            _isNullUsernameNotification = false;
            username = string.Empty;
	    }
        else
        {
            _isDuplicateUsername = false;
            _isNullUsernameNotification = false;

            this.EnterUsername();
	    }
    }
    //<!-- Enter Username from User. 
    void EnterUsername()
    {
        Debug.Log("EnterUsername");

        //<!-- Autosave Mechanicism. When have empty game slot.  
        if (player_1 == string.Empty) {
            Mz_StorageManage.SaveSlot = 1;
            //this.SaveNewPlayer();
            StartCoroutine(ShowInitializeNewShop());
        }
        else if (player_2 == string.Empty) {
			Mz_StorageManage.SaveSlot = 2;
            //this.SaveNewPlayer();
            StartCoroutine(ShowInitializeNewShop());
        }
        else if (player_3 == string.Empty) {
			Mz_StorageManage.SaveSlot = 3;
            //this.SaveNewPlayer();
            StartCoroutine(ShowInitializeNewShop());
        }
        else {
			Mz_StorageManage.SaveSlot = 0;
            _isFullSaveGameSlot = true;
            StartCoroutine(ShowLoadShop());

            Debug.Log("<!-- Full Slot Call Showsavegameslot method.");
        }
    }

    /// <summary>
    /// Save default game data for new player.
    /// </summary>
    private void SaveNewPlayer()
    {
		PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_USERNAME, this.username);
		PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_NAME, this.shopName);
        PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_MONEY, 500);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot +  Mz_StorageManage.KEY_SHOP_LOGO, initializeNewShop.currentLogoID);
		PlayerPrefs.SetString(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_SHOP_LOGO_COLOR , initializeNewShop.currentLogoColor);

        int[] IdOfCanSellItem = new int[] { 0, 5, 9, 18 };
        PlayerPrefsX.SetIntArray(Mz_StorageManage.SaveSlot + "cansellgoodslist", IdOfCanSellItem);

		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ROOF_ID, 255);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_AWNING_ID, 255);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TABLE_ID, 0);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_ACCESSORY_ID, 0);
		
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TK_CLOTHE_ID, 255);
		PlayerPrefs.SetInt(Mz_StorageManage.SaveSlot + Mz_StorageManage.KEY_TK_HAT_ID, 255);

        Debug.Log("Store new player data complete.");		

        Mz_StorageManage.LoadSaveDataToGameStorage();

        this.LoadSceneTarget();
    }
    private void LoadSceneTarget() {
        if(Application.isLoadingLevel == false) {
			Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString();
			Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());					
		}
    }

    //<!-- Show save game slot. If slot is full.
    void ShowSaveGameSlot(bool _toSaveGame)
    {
        if (_toSaveGame) 
		{   
            //<!-- Full save game slot. Show notice message.
            string message = string.Empty;			
            //message = "���͡��ͧ����ͧ��� ����ź��������� ��зѺ���¢���������";
            message = "Select Data Slot To Replace New Data";
			
            mainmenu_Skin.textField.normal.textColor = Color.white;
            GUI.Box(new Rect(ShopScene_GUIManager.viewPort_rect.width / 2 - 200, 0, 400, 64), message, mainmenu_Skin.textField);
		}

        GUI.BeginGroup(showSaveGameSlot_GroupRect);
        {
            if (_toSaveGame)			
            {
                /// Display To Save Username.
//                GUI.Box(textbox_header_rect, username, mainmenu_Skin.textField);
                /// Choose SaveGame Slot for replace new data.
                if (GUI.Button(slot_1Rect, new GUIContent(PlayerPrefs.GetString(1 + Mz_StorageManage.KEY_USERNAME), "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    Mz_StorageManage.SaveSlot = 1;
//                    SaveNewPlayer();
					StartCoroutine(ShowInitializeNewShop());
                }
                else if (GUI.Button(slot_2Rect, new GUIContent(PlayerPrefs.GetString(2 + Mz_StorageManage.KEY_USERNAME), "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    Mz_StorageManage.SaveSlot = 2;
//				    SaveNewPlayer();
					StartCoroutine(ShowInitializeNewShop());
                }
                else if (GUI.Button(slot_3Rect, new GUIContent(PlayerPrefs.GetString(3 + Mz_StorageManage.KEY_USERNAME), "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    Mz_StorageManage.SaveSlot = 3;
                    //  SaveNewPlayer();
					StartCoroutine(ShowInitializeNewShop());
                }
            }
            else {
//                string headerText = "";
//                headerText = "���͡��ͧ����ͧ���������������¤�Ѻ";
//				headerText = "Select Data Slot";
//                GUI.Box(textbox_header_rect, headerText, mainmenu_Skin.textField);
                /// Choose SaveGame Slot for Load Save Data.
                string slot_1 = string.Empty;
                string slot_2 = string.Empty;
                string slot_3 = string.Empty;

                if (player_1 == string.Empty) slot_1 = "Empty";
                else slot_1 = player_1;
                if (player_2 == string.Empty) slot_2 = "Empty";
                else slot_2 = player_2;
                if (player_3 == string.Empty) slot_3 = "Empty";
                else slot_3 = player_3;

                #region <!-- GUI data slot button.

                if (GUI.Button(slot_1Rect, new GUIContent(slot_1, "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if(player_1 != string.Empty) {
                        Mz_StorageManage.SaveSlot = 1;
                        Mz_StorageManage.LoadSaveDataToGameStorage();
                        this.LoadSceneTarget();
                    }
                }
                else if (GUI.Button(slot_2Rect, new GUIContent(slot_2, "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if(player_2 != string.Empty) {
                        Mz_StorageManage.SaveSlot =2;
                        Mz_StorageManage.LoadSaveDataToGameStorage();
                        this.LoadSceneTarget();
                    }
                }
                else if (GUI.Button(slot_3Rect, new GUIContent(slot_3, "button")))
                {
                    audioEffect.PlayOnecWithOutStop(audioEffect.buttonDown_Clip);

                    if(player_3 != string.Empty) {
                        Mz_StorageManage.SaveSlot = 3;
                        Mz_StorageManage.LoadSaveDataToGameStorage();
                        this.LoadSceneTarget();
                    }
                }

                #endregion
            } 
        }
        GUI.EndGroup();
    }

    public override void OnInput(string nameInput)
    {
        base.OnInput(nameInput);

        if(mainmenu_Group.gameObject.active) {
            if (nameInput == createNewShop_button.name) {
                //<!-- SceneState.showNewShop -->
                StartCoroutine(ShowCreateNewShop());
            }
            else if (nameInput == loadShop_button.name)  {
                //<!-- SceneState.showLoadGame -->
                StartCoroutine(ShowLoadShop());
            }
        }
        else if(newgame_Group.gameObject.active) {
            //<!-- GUIState.showNewGame -->
            if(nameInput == back_button.name) {
                StartCoroutine(ShowMainMenu());
            }
            else if(nameInput == "OK_button") {
                this.CheckUserNameFormInput();
                //StartCoroutine(ShowInitializeNewShop());
            }
        }
        else if(loadgame_Group.gameObject.active) {
            if(nameInput == back_button.name) {
                StartCoroutine(ShowMainMenu());
            }
        }
        else if(initializeNewGame_Group.gameObject.active) {
            if(nameInput == back_button.name) {
                StartCoroutine(ShowMainMenu());
            }
            else if(nameInput == "OK_button") {
				if(shopName != "") {
                	this.SaveNewPlayer();
				}
            }
			else if(nameInput == "Previous_button") {
				initializeNewShop.HavePreviousCommand();
			}
			else if(nameInput == "Next_button") {
				initializeNewShop.HaveNextCommand();
			}
			else if(nameInput == "Blue") {
				initializeNewShop.HaveChangeLogoColor("Blue");
			}
			else if(nameInput == "Green") {
				initializeNewShop.HaveChangeLogoColor("Green");
			}
			else if(nameInput == "Pink") {
				initializeNewShop.HaveChangeLogoColor("Pink");
			}
			else if(nameInput == "Red") {
				initializeNewShop.HaveChangeLogoColor("Red");
			}
			else if(nameInput == "Yellow") {
				initializeNewShop.HaveChangeLogoColor("Yellow");
			}
        }
    }

    private IEnumerator ShowInitializeNewShop()
    {
        sceneState = SceneState.showNewShop;

        initializeNewGame_Group.gameObject.SetActiveRecursively(true);

        if(mainmenu_Group.gameObject.active)
            iTween.MoveTo(mainmenu_Group.gameObject, moveUpTransform_Data);
        if (newgame_Group.gameObject.active)
            iTween.MoveTo(newgame_Group.gameObject, moveUpTransform_Data);
        if (loadgame_Group.gameObject.active)
            iTween.MoveTo(loadgame_Group.gameObject, moveUpTransform_Data);

        yield return new WaitForSeconds(1);

        iTween.MoveTo(initializeNewGame_Group.gameObject, moveDownTransform_Data);
        mainmenu_Group.gameObject.SetActiveRecursively(false);
        newgame_Group.gameObject.SetActiveRecursively(false);
        loadgame_Group.gameObject.SetActiveRecursively(false);
    }

    private IEnumerator ShowMainMenu()
    {
        sceneState = SceneState.none;

        mainmenu_Group.gameObject.SetActiveRecursively(true);

        if(newgame_Group.gameObject.active)
            iTween.MoveTo(newgame_Group.gameObject, moveUpTransform_Data);
        if (initializeNewGame_Group.gameObject.active)
            iTween.MoveTo(initializeNewGame_Group.gameObject, moveUpTransform_Data);
        if (loadgame_Group.gameObject.active)
            iTween.MoveTo(loadgame_Group.gameObject, moveUpTransform_Data);

        yield return new WaitForSeconds(1);

        iTween.MoveTo(mainmenu_Group.gameObject, moveDownTransform_Data);
        newgame_Group.gameObject.SetActiveRecursively(false);
        initializeNewGame_Group.gameObject.SetActiveRecursively(false);
        loadgame_Group.gameObject.SetActiveRecursively(false);
        back_button.gameObject.SetActiveRecursively(false);
    }

    private IEnumerator ShowCreateNewShop() {
        iTween.MoveTo(mainmenu_Group.gameObject, moveUpTransform_Data);
        newgame_Group.gameObject.SetActiveRecursively(true);
        back_button.gameObject.SetActiveRecursively(true);

        yield return new WaitForSeconds(1);

        iTween.MoveTo(newgame_Group.gameObject, moveDownTransform_Data);
        mainmenu_Group.gameObject.SetActiveRecursively(false);

        sceneState = SceneState.showNewGame;
    }

    private IEnumerator ShowLoadShop() {        
        if(newgame_Group.gameObject.active)
            iTween.MoveTo(newgame_Group.gameObject, moveUpTransform_Data);
        if (initializeNewGame_Group.gameObject.active)
            iTween.MoveTo(initializeNewGame_Group.gameObject, moveUpTransform_Data);
        if(mainmenu_Group.gameObject.active)
            iTween.MoveTo(mainmenu_Group.gameObject, moveUpTransform_Data);

        loadgame_Group.gameObject.SetActiveRecursively(true);
        back_button.gameObject.SetActiveRecursively(true);

        yield return new WaitForSeconds(1);

        iTween.MoveTo(loadgame_Group.gameObject, moveDownTransform_Data);
        mainmenu_Group.gameObject.SetActiveRecursively(false);
        newgame_Group.gameObject.SetActiveRecursively(false);

        sceneState = SceneState.showLoadGame;
    }
}
