using UnityEngine;
using System;
using System.Collections;

public class ToastBeh : GoodsBeh {	
		
	public static bool _IsActive = false;
		
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
	
	protected override void OnTouchDown ()
	{
        if (_canActive && ToastBeh._IsActive == false)
        {
            ToastBeh._IsActive = true;
            base.CheckingDelegationOfWaitFotIngredientEvent(this, EventArgs.Empty);

			sceneManager.SetAnimatedJamInstance(true);
        }
		
		base.OnTouchDown();
	}
	
	public override void WaitForIngredient(string ingredientName) 
	{		
		if(base._isWaitFotIngredient == false)
			return;
		
		if(ingredientName == JamBeh.StrawberryJam) {
			base.animatedSprite.Play(JamBeh.StrawberryJam);
			base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite anim, int Id)
			{				
				this.gameObject.name = GoodDataStore.FoodMenuList.ToastWithStrawberryJam.ToString();
				
				base._canDragaable = true;
				ToastBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
			};
		}
		else if(ingredientName == JamBeh.BlueberryJam) {
			base.animatedSprite.Play(JamBeh.BlueberryJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id)
			{
                this.gameObject.name = GoodDataStore.FoodMenuList.ToastWithBlueberryJam.ToString();
				
				base._canDragaable = true;
				ToastBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
            };
		}
		else if(ingredientName == JamBeh.ButterJam) {
			base.animatedSprite.Play(JamBeh.ButterJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id)
			{
                this.gameObject.name = GoodDataStore.FoodMenuList.ToastWithButterJam.ToString();
                				
				base._canDragaable = true;
				ToastBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;
            };
		}
		else if(ingredientName == JamBeh.CustardJam) {
			base.animatedSprite.Play(JamBeh.CustardJam);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id)
			{ 
				this.gameObject.name = GoodDataStore.FoodMenuList.ToastWithCustardJam.ToString();
                				
				base._canDragaable = true;
				ToastBeh._IsActive = false;
				base._canActive = false;
                base._isWaitFotIngredient = false;              
            };
		}
	}
}
