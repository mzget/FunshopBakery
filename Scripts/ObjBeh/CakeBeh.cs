using UnityEngine;
using System;
using System.Collections;

public class CakeBeh : GoodsBeh {

    public static bool _IsActive = false;

	public const string Cupcake = "Cupcake";
	public const string MiniCake = "MiniCake";
	public const string Cake = "Cake";
	
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		base._canActive = true;
		
		if(_canActive)
            base.waitForIngredientEvent += base.Handle_waitForIngredientEvent;
	}

    protected override void OnTouchDown()
    {		
        if (_canActive && CakeBeh._IsActive == false)
        {
            CakeBeh._IsActive = true;
            base.CheckingDelegationOfWaitFotIngredientEvent(this, EventArgs.Empty);

            base.OnTouchDown();
        }
		else {
			base.OnTouchDown();
		}
    }

    public override void WaitForIngredient(string ingredientName)
    {
        base.WaitForIngredient(ingredientName);
        if (_isWaitFotIngredient == false)
            return;

        iTween.Stop(this.gameObject);
        this.transform.position = base.originalPosition;

        if (ingredientName == CreamBeh.ChocolateCream)
        {
            base.animatedSprite.Play(CreamBeh.ChocolateCream);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite animSprite, int id)
            {
                if (this.gameObject.name == Cupcake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Chocolate_cupcake.ToString();
                else if (this.gameObject.name == MiniCake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Chocolate_minicake.ToString();
                else if (this.gameObject.name == Cake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Chocolate_cake.ToString();

                base._canDragaable = true;
                CakeBeh._IsActive = false;
                base._canActive = false;
                base._isWaitFotIngredient = false;
            };
        }
        else if (ingredientName == CreamBeh.BlueberryCream)
        {
            base.animatedSprite.Play(CreamBeh.BlueberryCream);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
            {
                if (this.gameObject.name == Cupcake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Blueberry_cupcake.ToString();
                else if (this.gameObject.name == MiniCake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Blueberry_minicake.ToString();
                else if (this.gameObject.name == Cake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Blueberry_cake.ToString();

                base._canDragaable = true;
                CakeBeh._IsActive = false;
                base._canActive = false;
                base._isWaitFotIngredient = false;
            };
        }
        else if (ingredientName == CreamBeh.StrawberryCream)
        {
            base.animatedSprite.Play(CreamBeh.StrawberryCream);
            base.animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId)
            {
                if (this.gameObject.name == Cupcake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Strawberry_cupcake.ToString();
                else if (this.gameObject.name == MiniCake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Strawberry_minicake.ToString();
                else if (this.gameObject.name == Cake)
                    this.gameObject.name = GoodDataStore.FoodMenuList.Strawberry_cake.ToString();

                base._canDragaable = true;
                CakeBeh._IsActive = false;
                base._canActive = false;
                base._isWaitFotIngredient = false;
            };
        }
    }

	public override void OnDispose ()
	{
		base.OnDispose ();

		Destroy(this);
	}
}
