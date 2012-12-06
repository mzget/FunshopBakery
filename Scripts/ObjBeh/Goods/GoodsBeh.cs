using UnityEngine;
using System.Collections;

public class GoodsBeh : ObjectsBeh {    
    public const string ClassName = "GoodsBeh";
	protected static bool _IsActive = false;
    
    public Vector3 offsetPos;	

	// WaitForIngredientEvent.
	protected bool _isWaitFotIngredient = false;	
	protected event System.EventHandler waitForIngredientEvent;
	protected virtual void Handle_waitForIngredientEvent (object sender, System.EventArgs e)
	{
		_isWaitFotIngredient = true;
	}
	public virtual void WaitForIngredient(string ingredientName) {			
		Debug.Log("WaitForIngredient :: " + ingredientName);
	}
	
	/// Put goods objects intance on food tray.
	public event System.EventHandler putObjectOnTray_Event;
	protected void OnPutOnTray_event (System.EventArgs eventArgs) {
		if (putObjectOnTray_Event != null) {
			putObjectOnTray_Event (this, eventArgs);
			Debug.Log (putObjectOnTray_Event + " : " + this.name);

            sceneManager.currentCustomer.CheckGoodsObjInTray(GoodsBeh.ClassName);
		}
	}
	protected virtual void Handle_putObjectOnTray_Event (object sender, System.EventArgs e) {

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
                    this.OnDispose();
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
	
	/// <!-- OnInput Events.
	protected override void OnTouchDown()
    {		
		if(_canActive && _IsActive == false) {
			_IsActive = true;
			
			if(waitForIngredientEvent != null) {
				waitForIngredientEvent(this, System.EventArgs.Empty);
			}
			
			base.OnTouchDown();
		}	
		else 
			return;
    }	
	protected override void OnTouchEnded ()
	{
        base.OnTouchEnded();

		if(base._isDraggable)
			_isDropObject = true;
	}

    public override void OnDispose()
    {
        base.OnDispose();

        Destroy(this.gameObject);
        this.waitForIngredientEvent -= this.Handle_waitForIngredientEvent;
        this.putObjectOnTray_Event -= this.Handle_putObjectOnTray_Event;
    }

	public static void StaticDispose ()
	{
		GoodsBeh._IsActive = false;
	}
}