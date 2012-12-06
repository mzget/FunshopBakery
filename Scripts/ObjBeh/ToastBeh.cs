using UnityEngine;
using System.Collections;

public class ToastBeh : GoodsBeh {	
		
		
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		base._canActive = true;
		base.animationName_001 = "pure";
        base.offsetPos = Vector3.up * -0.1f;
		
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
			base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite anim, int Id) {				
				this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithStrawberryJam.ToString();
				
				base._canDragaable = true;
				GoodsBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
			};
		}
		else if(ingredientName == JamBeh.BlueberryJam) {
			base.animatedSprite.Play(JamBeh.BlueberryJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id) {
                this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithBlueberryJam.ToString();
				
				base._canDragaable = true;
				GoodsBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
            };
		}
		else if(ingredientName == JamBeh.ButterJam) {
			base.animatedSprite.Play(JamBeh.ButterJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id) {
                this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithButterJam.ToString();
                				
				base._canDragaable = true;
				GoodsBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
            };
		}
		else if(ingredientName == JamBeh.CustardJam) {
			base.animatedSprite.Play(JamBeh.CustardJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id) { 
				this.gameObject.name = GoodDataStore.GoodsOrderList.ToastWithCustardJam.ToString();
                				
				base._canDragaable = true;
				GoodsBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;              
            };
		}
	}
}
