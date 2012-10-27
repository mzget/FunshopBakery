using UnityEngine;
using System.Collections;

public class SheepBank : Mz_BaseScene {

    public GameObject background_obj;
    public GameObject upgradeInside_window_Obj;

    //<!-- Button object.
    public GameObject back_button;
	public GameObject next_button;
	public GameObject previous_button;
    public GameObject upgradeOutside_button;
    public GameObject upgradeInside_button;

    private Hashtable moveDown_hashdata = new Hashtable();
    private Hashtable moveUp_hashdata = new Hashtable();
	
	private UpgradeInsideManager upgradeInsideManager;
    public GameObject[] upgradeButtons = new GameObject[8];
    
    //<!-- Texture resources.
    public Texture2D tk_coin_img;

    public enum DrawGUIState { none = 0, ShowUpgradeInside = 1, };
    public DrawGUIState currentDrawGUIState;


    void Awake() {
        GUImanager.CalculateViewportScreen();
    }

	// Use this for initialization
	void Start () {
        base.InitializeAudio();
		upgradeInsideManager = upgradeInside_window_Obj.GetComponent<UpgradeInsideManager>();

        this.InitializeFields();
        //Mz_ResizeScale.ResizingScale(background_obj.transform);
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
        moveUp_hashdata.Add("time", 1f);
        moveUp_hashdata.Add("easetype", iTween.EaseType.easeOutSine);
        moveUp_hashdata.Add("oncomplete", "OnMoveUpComplete_event");
        moveUp_hashdata.Add("oncompletetarget", this.gameObject);
		
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 4; j++) {
				string temp = i.ToString() + j.ToString();
				upgradeInsideManager.upgradeInsideObj2D[i,j] = upgradeInside_window_Obj.transform.Find(temp).gameObject;
			}
		}
		upgradeInside_window_Obj.SetActiveRecursively(false);
    }
	
	private void OnMoveDownComplete_event() {
		if(upgradeInside_window_Obj.active) {        
            currentDrawGUIState = DrawGUIState.ShowUpgradeInside;
        }
	}
    private void OnMoveUpComplete_event() {
        upgradeInside_window_Obj.SetActiveRecursively(false);
        currentDrawGUIState = DrawGUIState.none;
    }
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();        
    } 
    
    public bool _showSkinLayout;
    private void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));

		GUI.BeginGroup(GUImanager.viewPort_rect);
		{
            if(_showSkinLayout) {
                GUI.Box(new Rect(0, 0, GUImanager.viewPort_rect.width, GUImanager.viewPort_rect.height), "Skin layout", GUI.skin.box);
            }

            if(currentDrawGUIState == DrawGUIState.ShowUpgradeInside) {
                //int row_1 = 290;
                //int row_2 = 540;
                //int boxWidth = 100;
                //int boxHeight = 40;
                //Rect matrixObj_00 = new Rect(110, row_1, boxWidth, boxHeight);
                //Rect matrixObj_01 = new Rect(245, row_1, boxWidth, boxHeight);
                //Rect matrixObj_02 = new Rect(380, row_1, boxWidth, boxHeight);
                //Rect matrixObj_03 = new Rect(515, row_1, boxWidth, boxHeight);
                //Rect matrixObj_10 = new Rect(110, row_2, boxWidth, boxHeight);
                //Rect matrixObj_11 = new Rect(245, row_2, boxWidth, boxHeight);
                //Rect matrixObj_12 = new Rect(380, row_2, boxWidth, boxHeight);
                //Rect matrixObj_13 = new Rect(515, row_2, boxWidth, boxHeight);

                //GUI.Box(matrixObj_00, "matrix_00");
                //GUI.Box(matrixObj_01, "matrix_01");
                //GUI.Box(matrixObj_02, "matrix_02");
                //GUI.Box(matrixObj_03, "matrix_03");
                //GUI.Box(matrixObj_10, "matrix_10");
                //GUI.Box(matrixObj_11, "matrix_11");
                //GUI.Box(matrixObj_12, "matrix_12");
                //GUI.Box(matrixObj_13, "matrix_13");
                Rect drawCoin_rect = new Rect(25, 10, 100, 100);
                Rect drawPlayerName_rect = new Rect(150, 5, 250, 50);
                Rect drawPlayerMoney_rect = new Rect(150, 60, 250, 50);
                GUI.BeginGroup(new Rect(GUImanager.viewPort_rect.width / 2 - 100, 0, 400, 120), GUIContent.none, GUI.skin.box);
                {
                    GUI.DrawTexture(drawCoin_rect, tk_coin_img);
                    GUI.Box(drawPlayerName_rect, StorageManage.ShopName);
                    GUI.Box(drawPlayerMoney_rect, StorageManage.Money.ToString());
                }
                GUI.EndGroup();
            }
        }
        GUI.EndGroup();
    }
        
    public override void OnInput(string nameInput)
    {
        base.OnInput(nameInput);

        if(nameInput == upgradeInside_button.name) {
            upgradeInside_window_Obj.SetActiveRecursively(true);
            iTween.MoveTo(upgradeInside_window_Obj.gameObject, moveDown_hashdata);
        }
        else if(nameInput == upgradeOutside_button.name) {
        
        }
        else if(nameInput == back_button.name) {
            if(upgradeInside_window_Obj.active) {
                iTween.MoveTo(upgradeInside_window_Obj.gameObject, moveUp_hashdata);
            }

            if(upgradeInside_window_Obj.active == false) {
                if(Application.isLoadingLevel == false) {
                    Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString();
                    Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
                }
            }
        }
		
		
		if(upgradeInside_window_Obj.active) {
			if(nameInput == next_button.name) {
				upgradeInsideManager.GotoNextPage();
			}
			else if(nameInput == previous_button.name) {
				upgradeInsideManager.BackToPreviousPage();
			}
            else {
                for (int i = 0; i < upgradeButtons.Length; i++) {
                    if(nameInput == upgradeButtons[i].name) {
                        upgradeInsideManager.BuyingUpgradeMechanism(upgradeButtons[i].name);
                    }  
                }
            }
		}
    }
}
