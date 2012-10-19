using UnityEngine;
using System.Collections;

public class GoodsBeh : ObjectsBeh {
    
	protected BakeryShop sceneManager;
	
	
	/// <summary>
	/// WaitForIngredientEvent.
	/// </summary>
	protected bool _isWaitFotIngredient = false;	
	protected event System.EventHandler waitForIngredientEvent;
	protected virtual void Handle_waitForIngredientEvent (object sender, System.EventArgs e)
	{
		_isWaitFotIngredient = true;
	}
	public virtual void WaitForIngredient(string ingredientName) {			
		Debug.Log("WaitForIngredient :: " + ingredientName);
	}	
	
	// Use this for initialization
	protected override void  Start()
    {
        base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		base.originalPosition = this.transform.position;
    }
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();
		
		Ray cursorRay;
		RaycastHit hit;		
		cursorRay = new Ray(this.transform.position, Vector3.forward);
		
		if(Physics.Raycast(cursorRay, out hit)) 
        {
			if(hit.collider.name == sceneManager.bin_behavior_obj.name) {			
				if(this._isDropObject == true) {
					sceneManager.bin_behavior_obj.PlayOpenAnimation();
					Destroy(this.gameObject);
                    OnDestroyObject_event(System.EventArgs.Empty);
				}
			}
			else if(hit.collider.name == sceneManager.foodsTray_obj.name) {
                if(this._isDropObject) {
					this._isDropObject = false;
	                base._isDraggable = false;
					base._canActive = false;
					this._isWaitFotIngredient = false;
					this.waitForIngredientEvent -= this.Handle_waitForIngredientEvent;
					originalPosition = this.transform.position;
					
                    OnPutOnTray_event(System.EventArgs.Empty);
					
					if(sceneManager.toasts[0] == this) {
						sceneManager.toasts[0] = null;
					}
					else if(sceneManager.toasts[1] == this) {
						sceneManager.toasts[1] = null;
					}
                }
            }
        	else {
	            if(this._isDropObject) {
	                this.transform.position = originalPosition;
	                this._isDropObject = false;
	                base._isDraggable = false;
	            }
        	}
		}
		else {
            if(this._isDropObject) {
//            if(_isDraggable == false) {
                this.transform.position = originalPosition;
                this._isDropObject = false;
                base._isDraggable = false;
            }
		}
		
		Debug.DrawRay(cursorRay.origin, Vector3.forward, Color.red);
	}

    private void StopActiveAnimation() {
		//<!--- On object active.
        if(animation) {
			this.animation.Stop();
		}
		else if(animatedSprite) {
            animatedSprite.Stop();
		}
    }
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
		if(sceneManager.touch.phase == TouchPhase.Ended || sceneManager.touch.phase == TouchPhase.Canceled) {			
			if(base._isDraggable)
				_isDropObject = true;
		}
        
        //if(Input.touchCount > 0) {
        //    Touch touch = Input.GetTouch(0);
        //    if(touch.tapCount >= 2) {
        //        if (touch.phase == TouchPhase.Began) {
        //            if(_canActive && _isActive) {
        //                _isActive = false;

        //                this.StopActiveAnimation();
        //                _isWaitFotIngredient = false;
        //                touch = new Touch();
        //            }
        //        }
        //    }
        //}
	}

    public override void OnMouseDown()
    {		
		if(_canActive && _isActive == false) {
			_isActive = true;
			
			if(waitForIngredientEvent != null) {
				waitForIngredientEvent(this, System.EventArgs.Empty);
			}
		}	
		else 
			return;
		
        base.OnMouseDown();
    }
	
	public override void OnMouseUp ()
	{
		if(base._isDraggable)
			_isDropObject = true;
	}
}