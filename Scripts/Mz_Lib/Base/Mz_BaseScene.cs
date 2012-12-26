using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Mz_BaseScene : MonoBehaviour {
    
    public enum SceneNames { none = 0, LoadingScene = 1, MainMenu, WaitForStart, Town, BakeryShop, Sheepbank, Dressing, DisplayReward, };
	
    //<!-- Audio Manage.
    protected static bool ToggleAudioActive = true;
    public GameEffectManager gameEffectManager;
    public AudioEffectManager audioEffect;
	public AudioDescribeManager audioDescribe;
    public GameObject audioBackground_Obj;
    public AudioClip background_clip;
    public List<AudioClip> soundEffect_clips = new List<AudioClip>();
//  public List<AudioClip> appreciate_Clips = new List<AudioClip>();
//	public List<AudioClip> warning_Clips = new List<AudioClip>();
	
	#region <@-- Detect Touch and Input Data Fields.

    public Touch touch;
    public Vector3 mousePos;
    public Vector3 originalPos;
    public Vector3 currentPos;
    private Vector3[] mainCameraPos = new Vector3[] { new Vector3(0, -.13f, -20), new Vector3(2.66f, -.13f, -20) };
	private Vector3 currentCameraPos = new Vector3(0, -.13f, -20);
    public bool _isDragMove = false;
	internal Mz_SmartDeviceInput smartDeviceInput;
	public ExtendsStorageManager extendsStorageManager;
	private HUDFPS hudFPS_Trace;

	#endregion

	#region <@-- Banner.

#if UNITY_IPHONE
	
	public static bool _IsShowADBanner = true;
	protected Mz_ADBannerManager banner;
	
#endif

	#endregion

    public bool _hasQuitCommand = false;


	void Awake ()
	{
		//<@-- Trace OnScreen FPS.
//		this.gameObject.AddComponent<HUDFPS> ();
//		hudFPS_Trace = this.gameObject.GetComponent<HUDFPS>();

		this.gameObject.AddComponent<ExtendsStorageManager> ();
		extendsStorageManager = this.GetComponent<ExtendsStorageManager> ();

#if UNITY_IPHONE

		GameObject bannerObj = GameObject.Find ("Banner_Obj");
		if (Mz_BaseScene._IsShowADBanner && bannerObj == null) {
			bannerObj = new GameObject ("Banner_Obj", typeof(Mz_ADBannerManager));
			DontDestroyOnLoad (bannerObj);

			banner = bannerObj.GetComponent<Mz_ADBannerManager> ();
		}
		else if(Mz_BaseScene._IsShowADBanner && bannerObj) {
			banner = bannerObj.GetComponent<Mz_ADBannerManager> ();
		} 

#endif

		this.Initialization();
	}

	protected virtual void Initialization ()
	{
		Debug.Log("Mz_BaseScene :: Initialization");
	}

	// Use this for initialization
//	void Start () {	}
	
	protected void InitializeAudio()
    {
		Debug.Log("Scene :: InitializeAudio");

        //<!-- Setup All Audio Objects.
        if (audioEffect == null) {
                audioEffect = GameObject.FindGameObjectWithTag("AudioEffect").GetComponent<AudioEffectManager>();
			
                if(audioEffect) { 
					audioEffect.alternativeEffect_source = audioEffect.transform.GetComponentInChildren<AudioSource>();
				
					audioEffect.audio.mute = !ToggleAudioActive;
	            	audioEffect.alternativeEffect_source.audio.mute = !ToggleAudioActive;
				}
            }

        if (audioDescribe == null) {
                audioDescribe = GameObject.FindGameObjectWithTag("AudioDescribe").GetComponent<AudioDescribeManager>();
				audioDescribe.audio.mute = !ToggleAudioActive;
        }
        
        /// <! Manage audio background.
		audioBackground_Obj = GameObject.FindGameObjectWithTag("AudioBackground");
        if (audioBackground_Obj == null)
        {
            audioBackground_Obj = new GameObject("AudioBackground", typeof(AudioSource));
            audioBackground_Obj.tag = "AudioBackground";
            audioBackground_Obj.audio.playOnAwake = true;
			audioBackground_Obj.audio.volume = 0.5f;
            audioBackground_Obj.audio.mute = !ToggleAudioActive;

            DontDestroyOnLoad(audioBackground_Obj);
        }
        else { 
            audioBackground_Obj.audio.mute = !ToggleAudioActive;
        }
    }

    //<!--- GUI_identity.
    public GameObject identityGUI_obj;
    public tk2dTextMesh usernameTextmesh;
    public tk2dTextMesh shopnameTextmesh;
    public tk2dTextMesh availableMoney;
    protected IEnumerator InitializeIdentityGUI()
    {
        if (Mz_StorageManage.Username != string.Empty)
        {
            usernameTextmesh.text = Mz_StorageManage.Username;
            usernameTextmesh.Commit();

            shopnameTextmesh.text = Mz_StorageManage.ShopName;
            shopnameTextmesh.Commit();

            availableMoney.text = Mz_StorageManage.AvailableMoney.ToString();
            availableMoney.Commit();
        }

        yield return null;
    }
    
    /// <summary>
    /// Virtual method. Used to generate game effect at runtime.
    /// Override this and implement your derived class. 
    /// </summary>
	protected virtual void InitializeGameEffectGenerator() {}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
		if (smartDeviceInput == null) {
			this.gameObject.AddComponent<Mz_SmartDeviceInput>();
			smartDeviceInput = this.gameObject.GetComponent<Mz_SmartDeviceInput>();
		}

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				smartDeviceInput.ImplementTouchInput ();
		} 
		else if (Application.isEditor || Application.isWebPlayer) {
				smartDeviceInput.ImplementMouseInput ();
		}

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu))
        {
            _hasQuitCommand = true;
        }
	}

	#region <!-- HasChangeTimeScale event.

	public static event EventHandler HasChangeTimeScale_Event;
	private void OnChangeTimeScale (EventArgs e) {
		if (HasChangeTimeScale_Event != null) 
				HasChangeTimeScale_Event (this, e);
	}
	protected void UpdateTimeScale(int delta) {
		Time.timeScale = delta;
		OnChangeTimeScale(EventArgs.Empty);
	}

	#endregion

    protected void ImplementTouchPostion ()
	{
//		Debug.Log ("ImplementTouchPostion");			
		
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            if(Input.touchCount > 0) {				
            	touch = Input.GetTouch(0);
				
	            if(touch.phase == TouchPhase.Began) {			
					originalPos = touch.position;
					currentPos = touch.position;
	            }

	            if(touch.phase == TouchPhase.Moved) {
					currentPos = touch.position;
                    this.MovingCameraTransform();   					
	            }
				
	            if(touch.phase == TouchPhase.Ended) {
					float distance = Vector2.Distance (currentPos, originalPos);
					float vector = currentPos.x - originalPos.x;
//					float speed = Time.deltaTime * (distance / touch.deltaTime);
					if (vector < 0) {
						if(distance > 200)
							currentCameraPos = mainCameraPos[1];
					}
					else if (vector > 0) {
						if(distance > 200)
							currentCameraPos = mainCameraPos[0];
					}
						
					iTween.MoveTo (Camera.main.gameObject, iTween.Hash("position", currentCameraPos, "time", 0.5f, "easetype", iTween.EaseType.linear));
					
					currentPos = Vector2.zero;
					originalPos = Vector2.zero;
	            }
            }
        }
		else if(Application.isWebPlayer || Application.isEditor) {
			mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				
			if(Input.GetMouseButtonDown(0)) {
				originalPos = mousePos;
                //Debug.Log("originalPos == " + originalPos);
			}
				
			if(Input.GetMouseButton(0)) {
				currentPos = mousePos;
                _isDragMove = true;
				this.MovingCameraTransform();
                //Debug.Log("currentPos == " + currentPos);
			}

            if (Input.GetMouseButtonUp(0)) {
                _isDragMove = true;
                originalPos = Vector3.zero;
                currentPos = Vector3.zero;
            }
		}
    }
	protected virtual void MovingCameraTransform ()
	{

	}

    public virtual void OnInput(string nameInput) {
    	Debug.Log("OnInput :: " + nameInput);
    }

    public virtual void OnPointerOverName(string nameInput) {
    	Debug.Log("OnPointerOverName :: " + nameInput);
    }

    void OnApplicationQuit() {
        extendsStorageManager.SaveDataToPermanentMemory();

#if UNITY_STANDALONE_WIN
        Application.CancelQuit();

        if(!Application.isLoadingLevel) {
            Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
            Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
        }
#endif

#if UNITY_IPHONE || UNITY_ANDROID
        //<-- to do asking for quit game.
#endif
    }

    public virtual void OnDispose() { }
     
    protected virtual void OnGUI()
    {
        GUI.depth = 0;
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, Screen.height / Main.GAMEHEIGHT, 1));

        if (_hasQuitCommand)
        {			
            GUI.BeginGroup(new Rect(Screen.width / 2 - (200 * Mz_OnGUIManager.Extend_heightScale), Main.GAMEHEIGHT / 2 - 100, 400 * Mz_OnGUIManager.Extend_heightScale, 200), "Do you want to quit ?", GUI.skin.window);
            {
                if (GUI.Button(new Rect(60 * Mz_OnGUIManager.Extend_heightScale, 155, 100 * Mz_OnGUIManager.Extend_heightScale, 40), "Yes"))
                    Application.Quit();
                else if (GUI.Button(new Rect(240 * Mz_OnGUIManager.Extend_heightScale, 155, 100 * Mz_OnGUIManager.Extend_heightScale, 40), "No")) {
                    _hasQuitCommand = false; 
				}
            }
            GUI.EndGroup();
        }

        #region <@!-- Draw_LogCallback debuging.

        //if (debugLogCallback_style == null) {
        //    debugLogCallback_style = new GUIStyle(GUI.skin.box);
        //    debugLogCallback_style.fontSize = 12;
        //    debugLogCallback_style.alignment = TextAnchor.MiddleLeft;
        //}

        //GUI.Box(new Rect(0, Main.GAMEHEIGHT - 50, Main.GAMEWIDTH * Mz_OnGUIManager.Extend_heightScale, 50), output, debugLogCallback_style);

        #endregion
    }

	#region <@-- Unity Log Callback.

//    public string output = "";
//    public string stack = "";
//    void OnEnable() {
//        Application.RegisterLogCallback(HandleLog);
//    }
//    void OnDisable() {
//        Application.RegisterLogCallback(null);
//    }
//    public void HandleLog(string logString, string stackTrace, LogType type) {
//        output = logString;
//        stack = stackTrace;
//    }

	#endregion
}
