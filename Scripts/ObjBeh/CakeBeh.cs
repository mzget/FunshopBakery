using UnityEngine;
using System.Collections;

public class CakeBeh : GoodsBeh {
	
	public const string Cupcake = "Cupcake";
	public const string MiniCake = "MiniCake";
	public const string Cake = "Cake";
	
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		base._canActive = true;
		
		if(_canActive) {
			base.waitForIngredientEvent += base.Handle_waitForIngredientEvent;
		}
	}
	
	public override void WaitForIngredient (string ingredientName)
	{
		base.WaitForIngredient (ingredientName);
		
		if(_isWaitFotIngredient == false)
			return;

        iTween.Stop(this.gameObject);
		this.transform.position = base.originalPosition;
		
		
		if(ingredientName == CreamBeh.ChocolateCream) {
			base.animatedSprite.Play(CreamBeh.ChocolateCream);
			
			if(this.gameObject.name == Cupcake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Chocolate_cupcake.ToString();
			else if(this.gameObject.name == MiniCake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Chocolate_minicake.ToString();
			else if(this.gameObject.name == Cake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Chocolate_cake.ToString();
		}
		else if(ingredientName == CreamBeh.BlueberryCream) {
			base.animatedSprite.Play(CreamBeh.BlueberryCream);
			
			if(this.gameObject.name == Cupcake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Blueberry_cupcake.ToString();
			else if(this.gameObject.name == MiniCake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Blueberry_minicake.ToString();
			else if(this.gameObject.name == Cake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Blueberry_cake.ToString();
		}
		else if(ingredientName == CreamBeh.StrawberryCream) {
			base.animatedSprite.Play(CreamBeh.StrawberryCream);
			
			if(this.gameObject.name == Cupcake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Strawberry_cupcake.ToString();
			else if(this.gameObject.name == MiniCake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Strawberry_minicake.ToString();
			else if(this.gameObject.name == Cake)
				this.gameObject.name = GoodDataStore.GoodsOrderList.Strawberry_cake.ToString();
		}
		
		base._canDragaable = true;
	}
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
}
