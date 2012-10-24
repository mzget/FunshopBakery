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
	void Update () {
	
	}
	
	public override void OnMouseDown ()
	{
		base.OnMouseDown ();

		if(sceneManager.cupcake != null) {
			sceneManager.cupcake.WaitForIngredient(this.gameObject.name);
		}
		
		if(sceneManager.miniCake != null) {
			sceneManager.miniCake.WaitForIngredient(this.gameObject.name);
		}
		
		if(sceneManager.cake != null) {
			sceneManager.cake.WaitForIngredient(this.gameObject.name);
		}
	}
}
