using UnityEngine;
using System.Collections;


public class Goods : ScriptableObject {

    //public BakeryShop sceneManager;

    private Goods instance;
	public int price;
	
	public Goods() {
		Debug.Log("Starting Goods");

        //sceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BakeryShop>();        

        if(BakeryShop.gameLevel == 0) {
            int r = Random.Range(0, BakeryShop.CanSellGoodLists.Count);
            instance = BakeryShop.CanSellGoodLists[r];
			this.name = instance.name;
			this.price = instance.price;
        }
	}
	
	public Goods(string Init_name, int p_price) {
		this.instance = this;
		this.instance.name = Init_name;
		this.instance.price = p_price;
	}
}
