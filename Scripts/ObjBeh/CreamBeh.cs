using UnityEngine;
using System.Collections;

public class CreamBeh : ObjectsBeh {
	
	public const string ChocolateCream = "ChocolateCream";
	public const string StrawberryCream = "StrawberryCream";
	public const string BlueberryCream = "BlueberryCream";	

	/// <summary>
	/// Static game data.
	/// </summary>
	internal static string[] arr_CreamBehs = new string[3] { "", "", "", }; 
	

	void Awake() {
		iTween.Init(this.gameObject);
	}

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		this.sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		base._canDragaable = false;
	}

	protected override void OnTouchDown ()
	{
		sceneManager.SetAnimatedCreamInstance(false);

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
}
