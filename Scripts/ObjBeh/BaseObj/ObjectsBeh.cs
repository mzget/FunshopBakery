using UnityEngine;
using System.Collections;

public class ObjectsBeh : MonoBehaviour {

    public const string Packages_ResourcePath = "Packages/";
    //<!-- merchandise.
    public const string SouseMachine_ResourcePath = "BakeryShopObjects/SouseMachine/";
	public const string ToastAndJam_ResourcePath = "BakeryShopObjects/ToastandJam/";
	public const string Icecream_ResourcePath = "BakeryShopObjects/Icecreams/";
	public const string Cakes_ResourcePath = "BakeryShopObjects/Cakes/";
	public const string Sandwich_ResourcePath = "BakeryShopObjects/Sandwichs/";


	protected Mz_BaseScene baseScene;
    
    protected tk2dAnimatedSprite animatedSprite;
	
	public string animationName_001;
	public string animationName_002;
    public bool _canDragaable = false;
	protected bool _isDraggable = false;
    protected bool _isDropObject = false;
	public bool _canActive = false;	
    protected Vector3 originalPosition;
	protected bool _isActive = false;

    /// <summary>
    /// destroyObj_Event.
    /// </summary>	
	public event System.EventHandler destroyObj_Event;
    protected void OnDestroyObject_event(System.EventArgs e) {
        if (destroyObj_Event != null)
            destroyObj_Event(this, e);
    }
    /// <summary>
    /// Put goods objects intance on food tray.
    /// </summary>
    public event System.EventHandler putObjectOnTray_Event;
    protected void OnPutOnTray_event(System.EventArgs eventArgs) {
        if (putObjectOnTray_Event != null)
            putObjectOnTray_Event(this, eventArgs);
    }
	
	
    public static void ResetData() {
        //_IsActive = false;
    }	
	
	// Use this for initialization
	protected virtual void Start () {
        baseScene = GameObject.FindGameObjectWithTag("GameController").GetComponent<Mz_BaseScene>();

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

        this.transform.position = new Vector3(screenPoint.x, screenPoint.y, -4f);
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(_isDraggable) {
			this.ImplementDraggableObject();
		}
	}
	
	#region <!--- On Mouse Events.
	
	public virtual void OnMouseEnter() {
		
	}
	
	public virtual void OnMouseDrag() {        
//        Debug.Log(this.gameObject.name + " :: OnMouseDrag");
		   
        if(this._canDragaable) {
			this._isDraggable = true;
        }
	}
	
	public virtual void OnMouseDown() 
	{
//        Debug.Log(this.gameObject.name + " :: OnMouseDown");
        
//		if(_canActive && _IsActive == false) {
//			_IsActive = true;
//			
//			if(waitForIngredientEvent != null) {
//				waitForIngredientEvent(this, System.EventArgs.Empty);
//			}
//		}	
//		else 
//			return;
		
		//<!--- On object active.
        if(animation) {
			this.animation.Play();
		}
		else if(animatedSprite && animationName_001 != string.Empty) {
            animatedSprite.Play(animationName_001);
		
			animatedSprite.animationCompleteDelegate = animationCompleteDelegate;
		}
		
        baseScene.audioEffect.PlayOnecSound(baseScene.audioEffect.buttonDown_Clip);
	}	
	public void animationCompleteDelegate(tk2dAnimatedSprite sprite, int clipId) {
		if(animationName_002 != "") {
    		animatedSprite.Play(animationName_002);
			animatedSprite.animationCompleteDelegate -= animationCompleteDelegate;
		}
	}
	
	public virtual void OnMouseUp() {
//        Debug.Log(this.gameObject.name + " :: OnMouseUp");
	}
	
	public virtual void OnMouseExit() {

	}
	
	#endregion.
}
