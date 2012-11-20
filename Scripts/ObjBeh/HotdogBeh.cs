using UnityEngine;
using System.Collections;

public class HotdogBeh : GoodsBeh {
	
	public const string HotdogWithSauce = "HotdogWithSauce";
	public const string HotdogWithCheese = "HotdogWithCheese";
	
	
	// Use this for initialization
    protected override void Start()
    {
        base.Start();
		
		base._canActive = true;
        base.offsetPos = Vector3.up * -0.1f;
		
		if(_canActive) {
			base.waitForIngredientEvent += base.Handle_waitForIngredientEvent;
		}
    }
	
	

    public override void WaitForIngredient(string ingredientName)
    {
        base.WaitForIngredient(ingredientName);

		if(_isWaitFotIngredient == false)
			return;
		
		iTween.Stop(this.gameObject);
		this.transform.position = originalPosition;
		
		if(ingredientName == HotdogBeh.HotdogWithSauce) {
			base.animatedSprite.Play(HotdogBeh.HotdogWithSauce);
			base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) 
			{	
				this.gameObject.name = GoodDataStore.GoodsOrderList.Hotdog.ToString();
				
				_canDragaable = true;
			};
			
			return;
		}
		
		if(ingredientName == HotdogBeh.HotdogWithCheese) {
			base.animatedSprite.Play(HotdogBeh.HotdogWithCheese);
			base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
			{		
				this.gameObject.name = GoodDataStore.GoodsOrderList.HotdogWithCheese.ToString();
				
				_canDragaable = true;
			};
			
			return;
		}
    }
}
