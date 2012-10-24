using UnityEngine;
using System.Collections;

public class Town : Mz_BaseScene {
	
	public GameObject town_bg_group;
	public GameObject shop_body_sprite;
    public GameObject sheepBank_body_Obj;
    public tk2dAnimatedSprite bakeryShopDoorOpen_animated;
    public tk2dAnimatedSprite sheepBank_door_animated;
	
	//<!--- Game button.
	public GameObject dressing_button_Obj;
	public GameObject trophy_button_Obj;
	public GameObject info_button_Obj;
	public GameObject back_button_obj;
    //<!-- Texture2d resources.
    public Texture2D tk_coin_img;


    Rect TK_money_rect = new Rect(0, 0, 400, 150);          //<!-- TopLeft rectangle of screen.
    Rect drawCoin_rect = new Rect(20, 20, 100, 100);
    Rect drawPlayerName_rect = new Rect(150, 20, 250, 50);
    Rect drawPlayerMoney_rect = new Rect(150, 90, 250, 50);
    void Awake() {
        GUI_manager.CalculateViewportScreen();
    }
	
	// Use this for initialization
	void Start () {
		base.InitializeAudio();
		Mz_ResizeScale.ResizingScale(town_bg_group.transform);
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
		if(Camera.main.transform.position.x > 2.66f) 
			Camera.main.transform.position = new Vector3(2.66f, Camera.main.transform.position.y, Camera.main.transform.position.z); 	//Vector3.left * Time.deltaTime;
		else if(Camera.main.transform.position.x < 0) 
			Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);	 //Vector3.right * Time.deltaTime;
	}

	protected override void CheckTouchPostionAndMove ()
	{
		base.CheckTouchPostionAndMove();

        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            float speed = Time.deltaTime;
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
	
    void OnGUI() {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height/ Main.GAMEHEIGHT, 1));

        GUI.BeginGroup(TK_money_rect, "", GUI.skin.box);
        {
            GUI.DrawTexture(drawCoin_rect, tk_coin_img);
            GUI.Box(drawPlayerName_rect, StorageManage.Username);
            GUI.Box(drawPlayerMoney_rect, StorageManage.Money.ToString());
        }
        GUI.EndGroup();        
    }


	public override void OnInput (string nameInput)
	{
		base.OnInput (nameInput);

        if (nameInput == shop_body_sprite.gameObject.name) {
            this.PlayBakeryShopOpenAnimation();
        }
        else if (nameInput == back_button_obj.name) {
            if(Application.isLoadingLevel == false) {
				//<!-- Clear static NumberOfCanSellItem.
				BakeryShop.NumberOfCansellItem.Clear();
				
				
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        }
        else if (nameInput == dressing_button_Obj.name) {
            if (Application.isLoadingLevel == false) {
                Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Dressing.ToString();
                Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
            }
        }
        else if(nameInput == sheepBank_body_Obj.name) {
            this.PlaySheepBankOpenAnimation();
        }
	}

    private void PlayBakeryShopOpenAnimation()
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
