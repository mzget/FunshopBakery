using UnityEngine;
using System.Collections;

public class ToastBeh : GoodsBeh {	
		
		
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		base._canActive = true;
		base.animationName_001 = "pure";
		
		if(_canActive)
			base.waitForIngredientEvent += base.Handle_waitForIngredientEvent;
	}
	
	public override void WaitForIngredient(string ingredientName) 
	{
//		base.WaitForIngredient(ingredientName);		
		
		if(base._isWaitFotIngredient == false)
			return;
		
		if(ingredientName == JamBeh.StrawberryJam) {
			base.animatedSprite.Play(JamBeh.StrawberryJam);
			
			this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithStrawberryJam.ToString();
		}
		else if(ingredientName == JamBeh.BlueberryJam) {
			base.animatedSprite.Play(JamBeh.BlueberryJam);
			
			this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithBlueberryJam.ToString();
		}
		else if(ingredientName == JamBeh.ButterJam) {
			base.animatedSprite.Play(JamBeh.ButterJam);
			
			this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithButterJam.ToString();
		}
		else if(ingredientName == JamBeh.CustardJam) {
			base.animatedSprite.Play(JamBeh.CustardJam);
			
			this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithCustardJam.ToString();
		}
		
		base._canDragaable = true;
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
                this.transform.position = originalPosition;
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
}
