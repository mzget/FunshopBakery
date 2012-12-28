using UnityEngine;
using System.Collections;

[System.Serializable]	
public class BankOfficer {
	public const string WOMAN_IDLE = "woman_idle"; 	
	public const string WOMAN_TALK = "woman_talk";
	public const string MAN_TALK = "man_talk";
	public const string MAN_IDLE = "man_idle";


	public tk2dAnimatedSprite woman_animated;
	public tk2dAnimatedSprite man_animated;
};

public class SheepBank : Mz_BaseScene {

    public GameObject background_obj;
    public GameObject upgradeInside_window_Obj;
	public GameObject depositForm_Obj;
	public GameObject withdrawalForm_Obj;
	public GameObject transactionForm_Obj;
    public GameObject donationForm_group;
	public GameObject passbook_group;
	public GameObject shadowPlane_Obj;
	public tk2dTextMesh availableMoney_Textmesh;
	public tk2dTextMesh accountBalance_Textmesh;
	public tk2dTextMesh passbookAccountBalance_textmesh;
    public Mz_CalculatorBeh calculatorBeh;
	public GameObject availabelMoneyBillboard_Obj;
	public tk2dTextMesh availableMoneyBillboard_Textmesh;
	int resultValue = 0;

    //<!-- Constance Button Name.
	const string WITHDRAWAL_BUTTON_NAME = "Withdrawal_button";
	const string DEPOSIT_BUTTON_NAME = "Deposit_button";
	const string UpgradeInside_BUTTON_NAME = "UpgradeInside_button";
	const string UpgradeOutside_BUTTON_NAME = "UpgradeOutside_button";
    const string Donate_Button_Name = "Donate_button";
	const string PreviousButtonName = "Previous_button";
	const string NextButtonName = "Next_button";
    const string OKButtonName = "OK_button";
	const string BACK_BUTTON_NAME = "Back_button";
	const string PASSBOOKBUTTONNAME = "Passbook_button";
	const string YES_BUTTON_NAME = "Yes_button";
	const string NO_BUTTON_NAME = "No_button";
	
	const string ActiveDonationForm_function = "ActiveDonationForm";
    const string ActiveDepositForm_function = "ActiveDepositForm";
    const string ActiveWithdrawalForm_function = "ActiveWithdrawalForm";
	
	private Hashtable moveDown_Transaction_Hash;
    private Hashtable moveDownUpgradeInside = new Hashtable();
    private Hashtable moveUp_hashdata = new Hashtable();
	
	private DonationManager donationManager;
    private UpgradeInsideManager upgradeInsideManager;
    public BankOfficer offecer = new BankOfficer();
    public GameObject[] upgradeButtons = new GameObject[8];

    public enum GameSceneStatus { none = 0, ShowUpgradeInside = 1, ShowDonationForm, ShowDepositForm, ShowWithdrawalForm, ShowPassbook, };
    public GameSceneStatus currentGameStatus;
	
	internal static bool HaveUpgradeOutSide = false;
	
	// Use this for initialization
	void Start () {
        Mz_ResizeScale.ResizingScale(background_obj.transform);
		
		SheepBank.HaveUpgradeOutSide = false;

		StartCoroutine(this.InitializeAudio());
		this.InitializeGameEffectGenerator();
		StartCoroutine(this.InitializeBankOfficer());
        StartCoroutine(base.InitializeIdentityGUI());
		
		upgradeInsideManager = upgradeInside_window_Obj.GetComponent<UpgradeInsideManager>();
        donationManager = donationForm_group.GetComponent<DonationManager>();

        this.InitializeFields();
		shadowPlane_Obj.gameObject.active = false;
	}
    protected new IEnumerator InitializeAudio()
    {
        base.InitializeAudio();

        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();
		
		yield return 0;
    }
	protected override void InitializeGameEffectGenerator ()
	{
//		base.InitializeGameEffectGenerator ();

		this.gameObject.AddComponent<GameEffectManager>();
		base.gameEffectManager = this.gameObject.GetComponent<GameEffectManager>();
	}

	private IEnumerator InitializeBankOfficer ()
	{		
		offecer.woman_animated.Play(BankOfficer.WOMAN_TALK);
		offecer.woman_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
			offecer.woman_animated.Play(BankOfficer.WOMAN_IDLE);
		};

		offecer.man_animated.Play(BankOfficer.MAN_TALK);
		offecer.man_animated.animationCompleteDelegate = delegate {			
			offecer.man_animated.Play(BankOfficer.MAN_IDLE);
		};

		yield return 0;
	}
	private IEnumerator PlayWomanOfficerAnimation (string onCompleteFuctionName)
	{
		offecer.woman_animated.Play(BankOfficer.WOMAN_TALK);
		offecer.woman_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
			offecer.woman_animated.Play(BankOfficer.WOMAN_IDLE);
		};

		do{
			yield return 0;
		}
		while(offecer.woman_animated.CurrentClip.name == BankOfficer.WOMAN_TALK);

        if (onCompleteFuctionName != "")
            this.SendMessage(onCompleteFuctionName, null, SendMessageOptions.RequireReceiver);
        else
            Debug.LogWarning("PlayWomanOfficerAnimation : " + onCompleteFuctionName);
	}
    private IEnumerator PlayManOfficerAnimation(string oncompleteFunctionName)
    {
		offecer.man_animated.Play(BankOfficer.MAN_TALK);
		offecer.man_animated.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
			offecer.man_animated.Play(BankOfficer.MAN_IDLE);
		};

		do{
			yield return 0;
		}
		while(offecer.man_animated.CurrentClip.name == BankOfficer.MAN_TALK);

        if (oncompleteFunctionName != "")
            this.SendMessage(oncompleteFunctionName, null, SendMessageOptions.RequireReceiver);
        else
            Debug.LogWarning("PlayManOfficerAnimation : " + oncompleteFunctionName);
    }

    private void InitializeFields()
    {
        moveDownUpgradeInside.Add("position", new Vector3(0, -10f, -20f));
        moveDownUpgradeInside.Add("islocal", true);
        moveDownUpgradeInside.Add("time", .5f);
        moveDownUpgradeInside.Add("easetype", iTween.EaseType.spring);
		moveDownUpgradeInside.Add("oncomplete", "OnMoveDownComplete_event");
		moveDownUpgradeInside.Add("oncompletetarget", this.gameObject);

        moveUp_hashdata.Add("position", new Vector3(0, 200, -20f));
        moveUp_hashdata.Add("islocal", true);
        moveUp_hashdata.Add("time", .5f);
        moveUp_hashdata.Add("easetype", iTween.EaseType.easeOutSine);
        moveUp_hashdata.Add("oncomplete", "OnMoveUpComplete_event");
        moveUp_hashdata.Add("oncompletetarget", this.gameObject);
		
		moveDown_Transaction_Hash = new Hashtable(moveDownUpgradeInside);
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
        donationForm_group.SetActiveRecursively(false);
		passbook_group.SetActiveRecursively(false);

		this.AvailableMoneyManager(Mz_StorageManage.AvailableMoney);
        this.AccountBalanceManager(Mz_StorageManage.AccountBalance);
    }

    void AvailableMoneyManager(int r_value) {
        availableMoney_Textmesh.text = r_value.ToString("N");
        availableMoney_Textmesh.Commit();

		Mz_StorageManage.AvailableMoney = r_value;
		this.ReDrawGUIIdentity();
    }
    void AccountBalanceManager(int r_value) {
		accountBalance_Textmesh.text = r_value.ToString("N");
        accountBalance_Textmesh.Commit();

		Mz_StorageManage.AccountBalance = r_value;
    }

	void ReDrawGUIIdentity ()
	{
		base.availableMoney.text = Mz_StorageManage.AvailableMoney.ToString();
		base.availableMoney.Commit();
	}
	
	private void OnMoveDownComplete_event() {
        shadowPlane_Obj.active = true;
		if(upgradeInside_window_Obj.active) {        
            currentGameStatus = GameSceneStatus.ShowUpgradeInside;

			availabelMoneyBillboard_Obj.gameObject.SetActiveRecursively(true);
			this.ManageAvailabelMoneyBillBoard();

			upgradeInsideManager.ReInitializeData();
        }
	}

	internal void ManageAvailabelMoneyBillBoard ()
	{
		availableMoneyBillboard_Textmesh.text = Mz_StorageManage.AccountBalance.ToString();
		availableMoneyBillboard_Textmesh.Commit();
	}

    private void OnMoveUpComplete_event() {
        upgradeInside_window_Obj.SetActiveRecursively(false);
		depositForm_Obj.gameObject.SetActiveRecursively(false);
		withdrawalForm_Obj.gameObject.SetActiveRecursively(false);
		transactionForm_Obj.gameObject.SetActiveRecursively(false);
        donationForm_group.SetActiveRecursively(false);
		passbook_group.SetActiveRecursively(false);
		shadowPlane_Obj.gameObject.active = false;
		availabelMoneyBillboard_Obj.SetActiveRecursively(false);

        currentGameStatus = GameSceneStatus.none;        
    }
	void OnTransactionMoveDownComplete() {        
        this.AccountBalanceManager(Mz_StorageManage.AccountBalance);
	}
        
    public override void OnInput (string nameInput)	
	{
        if (nameInput == UpgradeInside_BUTTON_NAME)
        {
            StartCoroutine(this.PlayManOfficerAnimation("ActiveUpgradeInsideForm"));
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == UpgradeOutside_BUTTON_NAME)
        {
            StartCoroutine(PlayManOfficerAnimation("ActiveUpgradeOutside"));
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == DEPOSIT_BUTTON_NAME)
        {
            StartCoroutine(this.PlayWomanOfficerAnimation(ActiveDepositForm_function));
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == WITHDRAWAL_BUTTON_NAME)
        {
            StartCoroutine(this.PlayWomanOfficerAnimation(ActiveWithdrawalForm_function));
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == Donate_Button_Name)
        {
            StartCoroutine(this.PlayWomanOfficerAnimation(ActiveDonationForm_function));
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == PASSBOOKBUTTONNAME)
        {
            this.ActivePassbookForm();
            shadowPlane_Obj.gameObject.active = true;
            return;
        }
        else if (nameInput == BACK_BUTTON_NAME)
        {
            if (upgradeInside_window_Obj.active)
            {
                iTween.MoveTo(upgradeInside_window_Obj.gameObject, moveUp_hashdata);
                return;
            }
            if (depositForm_Obj.gameObject.active)
            {
                iTween.MoveTo(depositForm_Obj.gameObject, moveUp_hashdata);
                iTween.MoveTo(transactionForm_Obj.gameObject, moveUp_hashdata);
                return;
            }
            if (withdrawalForm_Obj.gameObject.active)
            {
                iTween.MoveTo(withdrawalForm_Obj.gameObject, moveUp_hashdata);
                iTween.MoveTo(transactionForm_Obj.gameObject, moveUp_hashdata);
                return;
            }
            else if (donationForm_group.active)
            {
                iTween.MoveTo(donationForm_group, moveUp_hashdata);
                return;
            }
            else if (currentGameStatus == GameSceneStatus.ShowPassbook)
            {
                iTween.MoveTo(passbook_group, moveUp_hashdata);
                return;
            }

            if (upgradeInside_window_Obj.active == false)
            {
                if (Application.isLoadingLevel == false)
                {
                    Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString();
                    Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
                }

                return;
            }
        }

        switch (currentGameStatus)
        {
            case GameSceneStatus.ShowUpgradeInside:
                if (nameInput == NextButtonName) { upgradeInsideManager.GotoNextPage(); }
                else if (nameInput == PreviousButtonName) { upgradeInsideManager.BackToPreviousPage(); }
                else if (nameInput == YES_BUTTON_NAME) { upgradeInsideManager.UserComfirm(); }
                else if (nameInput == NO_BUTTON_NAME) { upgradeInsideManager.UnActiveComfirmationWindow(); }
                else
                {
                    for (int i = 0; i < upgradeButtons.Length; i++)
                    {
                        if (nameInput == upgradeButtons[i].name)
                        {
                            upgradeInsideManager.BuyingUpgradeMechanism(upgradeButtons[i].name);
                            break;
                        }
                    }
                }
                break;
            case GameSceneStatus.ShowDonationForm:
                if (nameInput == PreviousButtonName)
                {
                    donationManager.PreviousDonationPage();
                }
                else if (nameInput == NextButtonName)
                {
                    donationManager.NextDonationPage();
                }
                break;
            case GameSceneStatus.ShowDepositForm:
                if (nameInput == OKButtonName) {
                    CompleteDopositSession();
				}
                break;
            case GameSceneStatus.ShowWithdrawalForm:
                if (nameInput == OKButtonName)
                    CompleteWithdrawalSesion();
                break;
            default:
                break;
        }

        if(calculatorBeh.gameObject.active) {
            calculatorBeh.GetInput(nameInput);
        }

        if (currentGameStatus == GameSceneStatus.ShowDonationForm) {
            donationManager.GetInput(nameInput);
        }
    }

    private void ActiveUpgradeInsideForm()
    {
		upgradeInside_window_Obj.SetActiveRecursively (true);
		iTween.MoveTo (upgradeInside_window_Obj.gameObject, moveDownUpgradeInside);

        audioEffect.PlayOnecWithOutStop(audioEffect.calc_clip);
    }  
    private void ActiveUpgradeOutside()
    {
		SheepBank.HaveUpgradeOutSide = true;
		if (Application.isLoadingLevel == false) {
			Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString ();
			Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString ());
		}
    }

    private void ActiveDepositForm() {
        depositForm_Obj.gameObject.SetActiveRecursively(true);
        transactionForm_Obj.gameObject.SetActiveRecursively(true);
        iTween.MoveTo(depositForm_Obj.gameObject, moveDown_Transaction_Hash);
        iTween.MoveTo(transactionForm_Obj.gameObject, moveDown_Transaction_Hash);
        
        audioEffect.PlayOnecWithOutStop(audioEffect.calc_clip);

        currentGameStatus = GameSceneStatus.ShowDepositForm;
    }
	private void CompleteDopositSession ()
	{
		resultValue = calculatorBeh.GetDisplayResultTextToInt();
        if(resultValue > 0 && resultValue <= Mz_StorageManage.AvailableMoney) {
		    int sumOfAccountBalance =  Mz_StorageManage.AccountBalance + resultValue;
		    int availableBalance = Mz_StorageManage.AvailableMoney - resultValue;
		    AccountBalanceManager(sumOfAccountBalance);
		    AvailableMoneyManager(availableBalance);
		    calculatorBeh.ClearCalcMechanism();
			
			audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
			gameEffectManager.Create2DSpriteAnimationEffect(GameEffectManager.BLOOMSTAR_EFFECT_PATH, GameObject.Find(OKButtonName).transform);
        }
        else {
            calculatorBeh.ClearCalcMechanism();			
			audioEffect.PlayOnecWithOutStop(audioEffect.mutter_clip);

			Debug.LogWarning("result value more than available money");
        }
	}

    private void ActiveWithdrawalForm() { 
		withdrawalForm_Obj.gameObject.SetActiveRecursively(true);
		transactionForm_Obj.gameObject.SetActiveRecursively(true);
		iTween.MoveTo(withdrawalForm_Obj.gameObject, moveDown_Transaction_Hash);
		iTween.MoveTo(transactionForm_Obj.gameObject, moveDown_Transaction_Hash);
    
        audioEffect.PlayOnecWithOutStop(audioEffect.calc_clip);

        currentGameStatus = GameSceneStatus.ShowWithdrawalForm;
    }
    private void CompleteWithdrawalSesion()
    {
        resultValue = calculatorBeh.GetDisplayResultTextToInt();

        if (resultValue > 0 && resultValue <= Mz_StorageManage.AccountBalance)
        {
            int newAvailableBalance = Mz_StorageManage.AvailableMoney + resultValue;
            int newAccountBalance = Mz_StorageManage.AccountBalance - resultValue;
            AvailableMoneyManager(newAvailableBalance);
            AccountBalanceManager(newAccountBalance);
            calculatorBeh.ClearCalcMechanism();			
			
			audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
			gameEffectManager.Create2DSpriteAnimationEffect(GameEffectManager.BLOOMSTAR_EFFECT_PATH, GameObject.Find(OKButtonName).transform);
        }
        else {
            calculatorBeh.ClearCalcMechanism();
			audioEffect.PlayOnecWithOutStop(audioEffect.mutter_clip);
			Debug.LogWarning("result value more than account balance");
        }
    }

    private void ActiveDonationForm() {				
        donationForm_group.SetActiveRecursively(true);
		donationManager.ReInitializeData();
        iTween.MoveTo(donationForm_group, moveDown_Transaction_Hash);

        audioEffect.PlayOnecWithOutStop(audioEffect.calc_clip);
		
		currentGameStatus = GameSceneStatus.ShowDonationForm;
    }

	private void ActivePassbookForm() {
		passbook_group.SetActiveRecursively(true);
		iTween.MoveTo(passbook_group, moveDown_Transaction_Hash);

		audioEffect.PlayOnecWithOutStop(audioEffect.calc_clip);

		currentGameStatus = GameSceneStatus.ShowPassbook;

		passbookAccountBalance_textmesh.text = Mz_StorageManage.AccountBalance.ToString();
		passbookAccountBalance_textmesh.Commit();
	}
}

