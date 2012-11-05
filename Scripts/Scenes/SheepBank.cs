using UnityEngine;
using System.Collections;

public class SheepBank : Mz_BaseScene {

    public GameObject background_obj;
    public GameObject upgradeInside_window_Obj;
	public GameObject depositForm_Obj;
	public GameObject withdrawalForm_Obj;
	public GameObject transactionForm_Obj;
	public GameObject shadowPlane_Obj;
    public Mz_CalculatorBeh calculatorBeh;
    public tk2dTextMesh availableMoney_Textmesh;
    public tk2dTextMesh accountBalance_Textmesh;
	int resultValue = 0;

    //<!-- Button object.
    public GameObject back_button;
	public GameObject next_button;
	public GameObject previous_button;
    public GameObject upgradeOutside_button;
    public GameObject upgradeInside_button;
	public GameObject withdrawal_button;
	public GameObject deposit_button;
	
	private Hashtable moveDown_Transaction_Hash;
    private Hashtable moveDown_hashdata = new Hashtable();
    private Hashtable moveUp_hashdata = new Hashtable();
	private UpgradeInsideManager upgradeInsideManager;
    public GameObject[] upgradeButtons = new GameObject[8];
    
    //<!-- Texture resources.
    public Texture2D tk_coin_img;

    public enum DrawGUIState { none = 0, ShowUpgradeInside = 1, };
    public DrawGUIState currentDrawGUIState;

	internal static bool HaveUpgradeOutSide = false;

    void Awake() {
        ShopScene_GUIManager.CalculateViewportScreen();

		SheepBank.HaveUpgradeOutSide = false;
    }

	// Use this for initialization
	void Start () {
        base.InitializeAudio();
		upgradeInsideManager = upgradeInside_window_Obj.GetComponent<UpgradeInsideManager>();

        this.InitializeFields();
        //Mz_ResizeScale.ResizingScale(background_obj.transform);
		shadowPlane_Obj.gameObject.active = false;
	}

    private void InitializeFields()
    {
        moveDown_hashdata.Add("position", new Vector3(0, -10f, 0));
        moveDown_hashdata.Add("islocal", true);
        moveDown_hashdata.Add("time", 1f);
        moveDown_hashdata.Add("easetype", iTween.EaseType.spring);
		moveDown_hashdata.Add("oncomplete", "OnMoveDownComplete_event");
		moveDown_hashdata.Add("oncompletetarget", this.gameObject);

        moveUp_hashdata.Add("position", new Vector3(0, 200, 0));
        moveUp_hashdata.Add("islocal", true);
        moveUp_hashdata.Add("time", .5f);
        moveUp_hashdata.Add("easetype", iTween.EaseType.easeOutSine);
        moveUp_hashdata.Add("oncomplete", "OnMoveUpComplete_event");
        moveUp_hashdata.Add("oncompletetarget", this.gameObject);
		
		moveDown_Transaction_Hash = moveDown_hashdata;
		moveDown_Transaction_Hash["position"] = new Vector3(0, 0, -20f);
		moveDown_Transaction_Hash["oncomplete"] = "OnTransactionMoveDownComplete";
		moveDown_Transaction_Hash["oncompletetarget"] = this.gameObject;
		
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 4; j++) {
				string temp = i.ToString() + j.ToString();
				upgradeInsideManager.upgradeInsideObj2D[i,j] = upgradeInside_window_Obj.transform.Find(temp).gameObject;
				upgradeInsideManager.upgradeButton_Objs[i,j] = upgradeInside_window_Obj.transform.Find("upgrade_" + i+j).gameObject;
			}
		}
		upgradeInside_window_Obj.SetActiveRecursively(false);
		depositForm_Obj.gameObject.SetActiveRecursively(false);
		withdrawalForm_Obj.gameObject.SetActiveRecursively(false);
		transactionForm_Obj.gameObject.SetActiveRecursively(false);

		this.AvailableMoneyManager(Mz_StorageManage.AvailableMoney);
        this.AccountBalanceManager(Mz_StorageManage.InBankMoney);
    }

    void AvailableMoneyManager(int r_value) {
        availableMoney_Textmesh.text = r_value.ToString("N");
        availableMoney_Textmesh.Commit();

		Mz_StorageManage.AvailableMoney = r_value;
    }
    void AccountBalanceManager(int r_value) {
		accountBalance_Textmesh.text = r_value.ToString("N");
        accountBalance_Textmesh.Commit();

		Mz_StorageManage.InBankMoney = r_value;
    }
	
	private void OnMoveDownComplete_event() {
		if(upgradeInside_window_Obj.active) {        
            currentDrawGUIState = DrawGUIState.ShowUpgradeInside;
        }
	}
    private void OnMoveUpComplete_event() {
        upgradeInside_window_Obj.SetActiveRecursively(false);
		depositForm_Obj.gameObject.SetActiveRecursively(false);
		withdrawalForm_Obj.gameObject.SetActiveRecursively(false);
		transactionForm_Obj.gameObject.SetActiveRecursively(false);
		shadowPlane_Obj.gameObject.active = false;
        currentDrawGUIState = DrawGUIState.none;
    }
	void OnTransactionMoveDownComplete() {
		shadowPlane_Obj.gameObject.active = true;
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();        
    } 
    
    public bool _showSkinLayout;
    private void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));

		GUI.BeginGroup(ShopScene_GUIManager.viewPort_rect);
		{
            if(_showSkinLayout) {
                GUI.Box(new Rect(0, 0, ShopScene_GUIManager.viewPort_rect.width, ShopScene_GUIManager.viewPort_rect.height), "Skin layout", GUI.skin.box);
            }

            if(currentDrawGUIState == DrawGUIState.ShowUpgradeInside) {
                Rect drawCoin_rect = new Rect(25, 10, 100, 100);
                Rect drawPlayerName_rect = new Rect(150, 5, 250, 50);
                Rect drawPlayerMoney_rect = new Rect(150, 60, 250, 50);
                GUI.BeginGroup(new Rect(ShopScene_GUIManager.viewPort_rect.width / 2 - 100, 0, 400, 120), GUIContent.none, GUI.skin.box);
                {
                    GUI.DrawTexture(drawCoin_rect, tk_coin_img);
                    GUI.Box(drawPlayerName_rect, Mz_StorageManage.ShopName);
                    GUI.Box(drawPlayerMoney_rect, Mz_StorageManage.AvailableMoney.ToString());
                }
                GUI.EndGroup();
            }
        }
        GUI.EndGroup();
    }
        
    public override void OnInput (string nameInput)	
	{        
        if (nameInput == upgradeInside_button.name) {
			upgradeInside_window_Obj.SetActiveRecursively (true);
			iTween.MoveTo (upgradeInside_window_Obj.gameObject, moveDown_hashdata);
			return;
		} 
		else if (nameInput == upgradeOutside_button.name) {
			SheepBank.HaveUpgradeOutSide = true;
			if (Application.isLoadingLevel == false) {
				Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString ();
				Application.LoadLevelAsync (Mz_BaseScene.SceneNames.LoadingScene.ToString ());
			}

			return;
		} 
		else if(nameInput == deposit_button.name) {
			depositForm_Obj.gameObject.SetActiveRecursively(true);
			transactionForm_Obj.gameObject.SetActiveRecursively(true);
			iTween.MoveTo(depositForm_Obj.gameObject, moveDown_Transaction_Hash);
			iTween.MoveTo(transactionForm_Obj.gameObject, moveDown_Transaction_Hash);
//			blurEffect_camera.enabled = true;
			return;
		}
		else if(nameInput == withdrawal_button.name) {
			withdrawalForm_Obj.gameObject.SetActiveRecursively(true);
			transactionForm_Obj.gameObject.SetActiveRecursively(true);
			iTween.MoveTo(withdrawalForm_Obj.gameObject, moveDown_Transaction_Hash);
			iTween.MoveTo(transactionForm_Obj.gameObject, moveDown_Transaction_Hash);
//			blurEffect_camera.enabled = true;
			return;
		}
		else if (nameInput == back_button.name) {
			if (upgradeInside_window_Obj.active) {
				iTween.MoveTo (upgradeInside_window_Obj.gameObject, moveUp_hashdata);
				return;
			}
			if(depositForm_Obj.gameObject.active) {
				iTween.MoveTo(depositForm_Obj.gameObject, moveUp_hashdata);
				iTween.MoveTo(transactionForm_Obj.gameObject, moveUp_hashdata);
				return;
			}
			if(withdrawalForm_Obj.gameObject.active) {
				iTween.MoveTo(withdrawalForm_Obj.gameObject, moveUp_hashdata);
				iTween.MoveTo(transactionForm_Obj.gameObject, moveUp_hashdata);
				return;
			}

			if (upgradeInside_window_Obj.active == false) {
				if (Application.isLoadingLevel == false) {
					Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString ();
					Application.LoadLevelAsync (Mz_BaseScene.SceneNames.LoadingScene.ToString ());
				}

				return;
			}
		}
		
		if (upgradeInside_window_Obj.active) {
						if (nameInput == next_button.name) {
								upgradeInsideManager.GotoNextPage ();
						} else if (nameInput == previous_button.name) {
								upgradeInsideManager.BackToPreviousPage ();
						} else {
								for (int i = 0; i < upgradeButtons.Length; i++) {
										if (nameInput == upgradeButtons [i].name) {
												upgradeInsideManager.BuyingUpgradeMechanism (upgradeButtons [i].name);
												break;
										}  
								}
						}
				}

        if (depositForm_Obj.gameObject.active)
        {
            switch (nameInput)
            {
                case "OK_button": CompleteDopositSession();
                    break;
                default:
                    break;
            }
        }

        if(withdrawalForm_Obj.gameObject.active) {
            switch (nameInput)
            {
                case "OK_button": CompleteWithdrawalSesion();
                    break;
                default:
                    break;
            }
        }

        if(calculatorBeh.gameObject.active) {
            calculatorBeh.GetInput(nameInput);
        }
    }

	void CompleteDopositSession ()
	{
		resultValue = calculatorBeh.GetDisplayResultTextToInt();
        if(resultValue <= Mz_StorageManage.AvailableMoney) {
		    int sumOfAccountBalance =  Mz_StorageManage.InBankMoney + resultValue;
		    int availableBalance = Mz_StorageManage.AvailableMoney - resultValue;
		    AccountBalanceManager(sumOfAccountBalance);
		    AvailableMoneyManager(availableBalance);
		    calculatorBeh.ClearCalcMechanism();
        }
        else {
            calculatorBeh.ClearCalcMechanism();

			Debug.LogWarning("result value more than available money");
        }
	}
    
    private void CompleteWithdrawalSesion()
    {
        resultValue = calculatorBeh.GetDisplayResultTextToInt();

        if (resultValue <= Mz_StorageManage.InBankMoney)
        {
            int newAvailableBalance = Mz_StorageManage.AvailableMoney + resultValue;
            int newAccountBalance = Mz_StorageManage.InBankMoney - resultValue;
            AvailableMoneyManager(newAvailableBalance);
            AccountBalanceManager(newAccountBalance);
            calculatorBeh.ClearCalcMechanism();
        }
        else {
            calculatorBeh.ClearCalcMechanism();

			Debug.LogWarning("result value more than account balance");
        }
    }
}
