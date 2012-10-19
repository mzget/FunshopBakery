using UnityEngine;
using System.Collections;

public class IcecreamBeh : GoodsBeh {
	
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		Debug.Log("Starting : IcecreamBeh");

		base._canDragaable = true;
		base.originalPosition = this.transform.position;
	}
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();
		
		Ray cursorRay;
		RaycastHit hit;
		cursorRay = new Ray(this.transform.position, Vector3.forward);
		
		if(Physics.Raycast(cursorRay, out hit)) {
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
					base._isWaitFotIngredient = false;
					base.waitForIngredientEvent -= base.Handle_waitForIngredientEvent;
					base.originalPosition = this.transform.position;
					
                    OnPutOnTray_event(System.EventArgs.Empty);
                }
            }
        	else {
	            if(this._isDropObject) {
	                this.transform.position = base.originalPosition;
	                this._isDropObject = false;
	                base._isDraggable = false;
	            }
        	}
		}
		else {
			if(this._isDropObject) {
                this.transform.position = base.originalPosition;
                this._isDropObject = false;
                base._isDraggable = false;
            }
		}
		
		Debug.DrawRay(cursorRay.origin, Vector3.forward, Color.red);
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
	
	public override void OnMouseUp ()
	{
//		base.OnMouseUp (); // Not Implement base.
		if(base._isDraggable) 
			base._isDropObject = true;
	}
}
