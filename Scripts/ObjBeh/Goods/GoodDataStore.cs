using UnityEngine;
using System.Collections;

public class GoodDataStore : ScriptableObject {

    public enum GoodsOrderList
    {
        //<!-- beverage.
        orange_juice = 0,           // 1. ¹éÓÊéÁ
        pineapple_juice = 1,        // 2. ¹éÓÊÑÁ»ÐÃŽ
        apple_juice,                // 3. ¹éÓáÍ»à»ÔéÅ
        cocoa_milk,                      // 4. ¹éÓâ¡â¡é
        fresh_milk,                   // 5. ¹éÓ¹ÁàÂç¹
        //<!-- Icecream.
        vanlla_icecream,            // 6. äÍÈ¡ÃÕÁÇÒ¹ÔÅÅÒ
        chocolate_icecream,        // 7. äÍÈ¡ÃÕÁªçÍ¡â¡àÅµ
        strawberry_icecream,       // 8. äÍÈ¡ÃÕÁÊµÃÍàºÍÃÃÕè
        //<!--- Cookie.
        chocolate_chip_cookie,      // 9.  €Ø¡¡ÕéªçÍ¡ªÔŸ
        butter_cookie,              // 10. €Ø¡¡Õéà¹Â
        fruit_cookie,               // 11. €Ø¡¡ÕéŒÅäÁé
        //<!--- Sandwich.
        ham_sandwich,               // 12. á«¹ÇÔªäÊéáÎÁ
        egg_sandwich,               // 13. á«¹ÇÔªäÊéä¢è
        deep_fried_chicken_sandwich,// 14. á«¹ÇÔªäÊéä¡è·ÍŽ
        tuna_sandwich,              // 15. á«¹ÇÔªäÊé·Ù¹èÒ 
        //<!--- Toast with jam.
        toast_with_strawberry_jam,  // 16. ¢¹Á»Ñ§áÂÁÊµÃÍàºÍÃÃÕè
        toast_with_butter_jam,          // 17. ¢¹Á»Ñ§à¹ÂÊŽ
        toast_with_custard_jam,         // 18. ¢¹Á»Ñ§ÊÑ§¢ÂÒ
        toast_with_blueberry_jam,   // 19. ¢¹Á»Ñ§ºÅÙàºÍÃÃÕè
        //<!--- Cake.
        blueberry_cupcake,               // 20. à€é¡ÇÒ¹ÔÅÅÒ
        chocolate_cupcake,             // 21. à€é¡ªçÍ¡â¡àÅµ
        strawberry_cupcake,            // 22. à€é¡ÊµÃÍàºÍÃÃÕè		
        blueberry_minicake,              // 23. à€é¡ÇÒ¹ÔÅÅÒ
        chocolate_minicake,            // 24. à€é¡ªçÍ¡â¡àÅµ
        strawberry_minicake,           // 25. à€é¡ÊµÃÍàºÍÃÃÕè		
        blueberry_cake,               // 26. à€é¡ÇÒ¹ÔÅÅÒ
        chocolate_cake,             // 27. à€é¡ªçÍ¡â¡àÅµ
        strawberry_cake,            // 28. à€é¡ÊµÃÍàºÍÃÃÕè
        //</ cake >

        hotdog_with_sauce,                     // 2x. ÎÍ·ŽÍ¡
        hotdog_with_cheese,        // 2x. ÎÍ·ŽÍ¡ãÊèªÕÊ
    };
	
   	public Goods[] Menu_list = new Goods[] {                               
	    new Goods(GoodsOrderList.pineapple_juice.ToString(), 3), //_can sell
        new Goods(GoodsOrderList.apple_juice.ToString(), 3),        // 1
        new Goods(GoodsOrderList.cocoa_milk.ToString(), 3),         // 2
        new Goods(GoodsOrderList.orange_juice.ToString(), 3),       // 3
        new Goods(GoodsOrderList.fresh_milk.ToString(), 3),         // 4

	    new Goods(GoodsOrderList.toast_with_strawberry_jam.ToString(), 10), //_can sell
        new Goods(GoodsOrderList.toast_with_blueberry_jam.ToString(), 10),      // 6
        new Goods(GoodsOrderList.toast_with_butter_jam.ToString(), 10),         // 7
        new Goods(GoodsOrderList.toast_with_custard_jam.ToString(), 10),        // 8
        
	    new Goods(GoodsOrderList.chocolate_cupcake.ToString(), 15), //_can sell
        new Goods(GoodsOrderList.blueberry_cupcake.ToString(), 15),     // 10
        new Goods(GoodsOrderList.strawberry_cupcake.ToString(), 15),    // 11
		
        new Goods(GoodsOrderList.chocolate_minicake.ToString(), 20),    // 12
        new Goods(GoodsOrderList.blueberry_minicake.ToString(), 20),    // 13
        new Goods(GoodsOrderList.strawberry_minicake.ToString(), 20),   // 14
		
        new Goods(GoodsOrderList.chocolate_cake.ToString(), 25),	//15
        new Goods(GoodsOrderList.blueberry_cake.ToString(), 25),	//16	
        new Goods(GoodsOrderList.strawberry_cake.ToString(), 25),	//17
        
	    new Goods(GoodsOrderList.strawberry_icecream.ToString(), 8), 				//_can sell.
        new Goods(GoodsOrderList.vanlla_icecream.ToString(), 8),					//19
        new Goods(GoodsOrderList.chocolate_icecream.ToString(), 8),					//20

        new Goods(GoodsOrderList.tuna_sandwich.ToString(), 12),						//21
        new Goods(GoodsOrderList.deep_fried_chicken_sandwich.ToString(), 20),		//22
        new Goods(GoodsOrderList.ham_sandwich.ToString(), 15),						//23
        new Goods(GoodsOrderList.egg_sandwich.ToString(), 12),						//24

        new Goods(GoodsOrderList.chocolate_chip_cookie.ToString(), 5),				//25
        new Goods(GoodsOrderList.fruit_cookie.ToString(), 5),						//26
        new Goods(GoodsOrderList.butter_cookie.ToString(), 5),						//27

        new Goods(GoodsOrderList.hotdog_with_sauce.ToString(), 10),					//28
        new Goods(GoodsOrderList.hotdog_with_cheese.ToString(), 15),				//29
    };
	
    public GoodDataStore() {
        Debug.Log("Starting GoodDataStore");
    }
}
