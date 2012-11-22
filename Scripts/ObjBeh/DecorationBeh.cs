using UnityEngine;
using System.Collections;

public class DecorationBeh : ObjectsBeh {

	public const string Sauce = "Sauce";
	public const string Cheese = "Cheese";

	protected override void OnTouchDown ()
	{
		base.animatedSprite.Play();
		base.animatedSprite.animationCompleteDelegate = AnimationComplete;

		if(this.name == DecorationBeh.Sauce)
			sceneManager.hotdog.WaitForIngredient(HotdogBeh.HotdogWithSauce);
		if(this.name == DecorationBeh.Cheese)
			sceneManager.hotdog.WaitForIngredient(HotdogBeh.HotdogWithCheese);

		base.OnTouchDown ();
	}
	
	public void AnimationComplete(tk2dAnimatedSprite sprite, int clipId) {
		animatedSprite.StopAndResetFrame();
		
		base.animatedSprite.animationCompleteDelegate -= AnimationComplete;
	}
}
