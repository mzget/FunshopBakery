using UnityEngine;
using System.Collections;

public class ObjectsBeh : Base_ObjectBeh {

    public const string Packages_ResourcePath = "Packages/";
    //<!-- merchandise.
    public const string SouseMachine_ResourcePath = "BakeryShopObjects/SouseMachine/";
	public const string ToastAndJam_ResourcePath = "BakeryShopObjects/ToastandJam/";
	public const string Icecream_ResourcePath = "BakeryShopObjects/Icecreams/";
	public const string Cakes_ResourcePath = "BakeryShopObjects/Cakes/";
	public const string Sandwich_ResourcePath = "BakeryShopObjects/Sandwichs/";
    public const string Cookie_ResourcePath = "BakeryShopObjects/Cookies/";    
    public const string Hotdog_ResourcePath = "BakeryShopObjects/Hotdogs/";


	protected Mz_BaseScene baseScene;    
	protected BakeryShop sceneManager;
    protected tk2dAnimatedSprite animatedSprite;
	
	public string animationName_001;
	public string animationName_002;
    public bool _canDragaable = false;
	protected bool _isDraggable = false;
    protected bool _isDropObject = false;
	public bool _canActive = false;	
    internal Vector3 originalPosition;

    /// <summary>
    /// destroyObj_Event.
    /// </summary>	
	public event System.EventHandler destroyObj_Event;
    protected void OnDestroyObject_event(System.EventArgs e) {
        if (destroyObj_Event != null)
        {
            destroyObj_Event(this, e);
            Debug.Log(destroyObj_Event + ": destroyObj_Event : " + this.name);
        }
    }

	
	// Use this for initialization
	protected virtual void Start () {
		this.originalPosition = this.transform.position;
		
        baseScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Mz_BaseScene>();
        sceneManager = baseScene as BakeryShop;
		
        try {
            animatedSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();
        }
        catch { }
	}
	
	protected virtual void ImplementDraggableObject() {
        Vector3 screenPoint;
		
        if (Input.touchCount >= 1) {
            screenPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else {
            screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        this.transform.position = new Vector3(screenPoint.x, screenPoint.y, -3f);
	}

	public void animationCompleteDelegate(tk2dAnimatedSprite sprite, int clipId) {
		if(animationName_002 != "") {
			animatedSprite.Play(animationName_002);
			animatedSprite.animationCompleteDelegate -= animationCompleteDelegate;
		}
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();

		if(_isDraggable) {
			this.ImplementDraggableObject();
		}
		
		if(sceneManager.touch.phase == TouchPhase.Ended || sceneManager.touch.phase == TouchPhase.Canceled) {			
			if(this._isDraggable)
				_isDropObject = true;
		}
	}

	protected override void OnTouchDown ()
	{
		if(_canActive) {
			//<!--- On object active.
			if(animatedSprite && animationName_001 != string.Empty) {
				animatedSprite.Play(animationName_001);				
				animatedSprite.animationCompleteDelegate = animationCompleteDelegate;
			}
	        else{ 
	            iTween.PunchPosition(this.gameObject, iTween.Hash("y", 0.2f, "time", 1f, "looptype", iTween.LoopType.loop));
	        }
		}
		
		base.OnTouchDown ();
	}
    protected override void OnTouchDrag()
    {
        base.OnTouchDrag();
        
        if(this._canDragaable) {
			this._isDraggable = true;
        }
    }
}
