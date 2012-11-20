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
}
