using UnityEngine;
using System.Collections;

public class CreamBeh : ObjectsBeh {
	
	public const string ChocolateCream = "ChocolateCream";
	public const string StrawberryCream = "StrawberryCream";
	public const string BlueberryCream = "BlueberryCream";
	
	
	private BakeryShop sceneManager;
	
	
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		this.sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		base._canDragaable = false;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

    #region <!-- OnInput.

	protected override void OnTouchDown ()
	{
		animatedSprite.Play(animationName_001);				
		animatedSprite.animationCompleteDelegate = animationCompleteDelegate;
		
		if(sceneManager.cupcake != null) {
			sceneManager.cupcake.WaitForIngredient(this.gameObject.name);
		}		
		if(sceneManager.miniCake != null) {
			sceneManager.miniCake.WaitForIngredient(this.gameObject.name);
		}		
		if(sceneManager.cake != null) {
			sceneManager.cake.WaitForIngredient(this.gameObject.name);
		}

		base.OnTouchDown ();
	}
    protected override void OnTouchEnded()
    {
        base.OnTouchEnded();
    }

    #endregion
}
