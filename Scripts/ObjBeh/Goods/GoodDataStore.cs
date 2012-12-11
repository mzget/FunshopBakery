using UnityEngine;
using System.Collections;

public class GoodDataStore : ScriptableObject {

    public enum GoodsOrderList
    {
        //<!-- beverage.
        Orange_juice = 0,           // 1. ¹éÓÊéÁ
        Pineapple_juice = 1,        // 2. ¹éÓÊÑÁ»ÐÃŽ
        Apple_juice,                // 3. ¹éÓáÍ»à»ÔéÅ
        Cocoa_milk,                      // 4. ¹éÓâ¡â¡é
        Freshmilk,                   // 5. ¹éÓ¹ÁàÂç¹
        //<!-- Icecream.
        Vanilla_icecream,            // 6. äÍÈ¡ÃÕÁÇÒ¹ÔÅÅÒ
        Chocolate_icecream,        // 7. äÍÈ¡ÃÕÁªçÍ¡â¡àÅµ
        Strawberry_icecream,       // 8. äÍÈ¡ÃÕÁÊµÃÍàºÍÃÃÕè
        //<!--- Cookie.
        Chocolate_cookie,      // 9.  €Ø¡¡ÕéªçÍ¡ªÔŸ
        Butter_cookie,              // 10. €Ø¡¡Õéà¹Â
        Fruit_cookie,               // 11. €Ø¡¡ÕéŒÅäÁé
        //<!--- Sandwich.
        Ham_sandwich,               // 12. á«¹ÇÔªäÊéáÎÁ
        Egg_sandwich,               // 13. á«¹ÇÔªäÊéä¢è
        DeepFriedChicken_sandwich,// 14. á«¹ÇÔªäÊéä¡è·ÍŽ
        Tuna_sandwich,              // 15. á«¹ÇÔªäÊé·Ù¹èÒ 
        //<!--- Toast with jam.
        ToastWithStrawberryJam,  // 16. ¢¹Á»Ñ§áÂÁÊµÃÍàºÍÃÃÕè
        ToastWithButterJam,          // 17. ¢¹Á»Ñ§à¹ÂÊŽ
        ToastWithCustardJam,         // 18. ¢¹Á»Ñ§ÊÑ§¢ÂÒ
        ToastWithBlueberryJam,   // 19. ¢¹Á»Ñ§ºÅÙàºÍÃÃÕè
        //<!--- Cake.
        Blueberry_cupcake,               // 20. à€é¡ÇÒ¹ÔÅÅÒ
        Chocolate_cupcake,             // 21. à€é¡ªçÍ¡â¡àÅµ
        Strawberry_cupcake,            // 22. à€é¡ÊµÃÍàºÍÃÃÕè		
        Blueberry_minicake,              // 23. à€é¡ÇÒ¹ÔÅÅÒ
        Chocolate_minicake,            // 24. à€é¡ªçÍ¡â¡àÅµ
        Strawberry_minicake,           // 25. à€é¡ÊµÃÍàºÍÃÃÕè		
        Blueberry_cake,               // 26. à€é¡ÇÒ¹ÔÅÅÒ
        Chocolate_cake,             // 27. à€é¡ªçÍ¡â¡àÅµ
        Strawberry_cake,            // 28. à€é¡ÊµÃÍàºÍÃÃÕè
        //</ cake >

        Hotdog,                     // 2x. ÎÍ·ŽÍ¡
        HotdogWithCheese,        // 2x. ÎÍ·ŽÍ¡ãÊèªÕÊ
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
        
	    new Goods(GoodsOrderList.Strawberry_icecream.ToString(), 8), 				//_can sell.
        new Goods(GoodsOrderList.Vanilla_icecream.ToString(), 8),					//19
        new Goods(GoodsOrderList.Chocolate_icecream.ToString(), 8),					//20

        new Goods(GoodsOrderList.Tuna_sandwich.ToString(), 12),						//21
        new Goods(GoodsOrderList.DeepFriedChicken_sandwich.ToString(), 20),		//22
        new Goods(GoodsOrderList.Ham_sandwich.ToString(), 15),						//23
        new Goods(GoodsOrderList.Egg_sandwich.ToString(), 12),						//24

        new Goods(GoodsOrderList.Chocolate_cookie.ToString(), 5),				//25
        new Goods(GoodsOrderList.Fruit_cookie.ToString(), 5),						//26
        new Goods(GoodsOrderList.Butter_cookie.ToString(), 5),						//27

        new Goods(GoodsOrderList.Hotdog.ToString(), 10),					//28
        new Goods(GoodsOrderList.HotdogWithCheese.ToString(), 15),				//29
    };
	
    public GoodDataStore() {
        Debug.Log("Starting GoodDataStore");
    }
}
