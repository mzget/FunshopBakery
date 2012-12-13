using UnityEngine;
using System.Collections;

public class GoodDataStore : ScriptableObject {

    public enum GoodsOrderList
    {
        //<!-- beverage.
        Pineapple_juice = 0,
        Apple_juice = 1,
        Cocoa_milk = 2,
        Orange_juice = 3,
        Freshmilk = 4,
        //<!--- Toast with jam.
        ToastWithStrawberryJam = 5,
        ToastWithBlueberryJam = 6,
        ToastWithButterJam = 7,
        ToastWithCustardJam = 8,
		//<!--- Cake.
		Chocolate_cupcake = 9,
		Blueberry_cupcake = 10, 
		Strawberry_cupcake = 11,	
		Chocolate_minicake = 12, 
		Blueberry_minicake = 13,
		Strawberry_minicake = 14,	
		Chocolate_cake = 15,
		Blueberry_cake = 16,
		Strawberry_cake = 17,     //</ cake >
		//<!-- Icecream.
		Strawberry_icecream = 18,
		Vanilla_icecream = 19,
		Chocolate_icecream = 20,
        //<!--- Sandwich.
        Tuna_sandwich = 21,
        DeepFriedChicken_sandwich = 22,
        Ham_sandwich = 23,
        Egg_sandwich = 24,
        //<!--- Cookie.
        Chocolate_cookie = 25,
        Fruit_cookie = 26,
        Butter_cookie = 27,

        Hotdog = 28,
        HotdogWithCheese = 29,
    };
	
   	public Goods[] Menu_list = new Goods[] {                               
	    new Goods(GoodsOrderList.Pineapple_juice.ToString(), 3), //_can sell
        new Goods(GoodsOrderList.Apple_juice.ToString(), 3),        // 1
        new Goods(GoodsOrderList.Cocoa_milk.ToString(), 3),         // 2
        new Goods(GoodsOrderList.Orange_juice.ToString(), 3),       // 3
        new Goods(GoodsOrderList.Freshmilk.ToString(), 3),         // 4

	    new Goods(GoodsOrderList.ToastWithStrawberryJam.ToString(), 10), //_can sell
        new Goods(GoodsOrderList.ToastWithBlueberryJam.ToString(), 10),      // 6
        new Goods(GoodsOrderList.ToastWithButterJam.ToString(), 10),         // 7
        new Goods(GoodsOrderList.ToastWithCustardJam.ToString(), 10),        // 8
        
	    new Goods(GoodsOrderList.Chocolate_cupcake.ToString(), 15), // 9 << can sell >>
        new Goods(GoodsOrderList.Blueberry_cupcake.ToString(), 15),     // 10
        new Goods(GoodsOrderList.Strawberry_cupcake.ToString(), 15),    // 11
		
        new Goods(GoodsOrderList.Chocolate_minicake.ToString(), 20),    // 12
        new Goods(GoodsOrderList.Blueberry_minicake.ToString(), 20),    // 13
        new Goods(GoodsOrderList.Strawberry_minicake.ToString(), 20),   // 14
		
        new Goods(GoodsOrderList.Chocolate_cake.ToString(), 25),	//15
        new Goods(GoodsOrderList.Blueberry_cake.ToString(), 25),	//16	
        new Goods(GoodsOrderList.Strawberry_cake.ToString(), 25),	//17
        
	    new Goods(GoodsOrderList.Strawberry_icecream.ToString(), 8), 			//_can sell.
        new Goods(GoodsOrderList.Vanilla_icecream.ToString(), 8),				//19
        new Goods(GoodsOrderList.Chocolate_icecream.ToString(), 8),				//20

        new Goods(GoodsOrderList.Tuna_sandwich.ToString(), 12),					//21
        new Goods(GoodsOrderList.DeepFriedChicken_sandwich.ToString(), 20),		//22
        new Goods(GoodsOrderList.Ham_sandwich.ToString(), 15),					//23
        new Goods(GoodsOrderList.Egg_sandwich.ToString(), 12),					//24

        new Goods(GoodsOrderList.Chocolate_cookie.ToString(), 5),				//25
        new Goods(GoodsOrderList.Fruit_cookie.ToString(), 5),					//26
        new Goods(GoodsOrderList.Butter_cookie.ToString(), 5),					//27

        new Goods(GoodsOrderList.Hotdog.ToString(), 10),						//28
        new Goods(GoodsOrderList.HotdogWithCheese.ToString(), 15),				//29
    };
	
    public GoodDataStore() {
        Debug.Log("Starting GoodDataStore");
    }
}
