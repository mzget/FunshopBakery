using UnityEngine;
using System.Collections;


public class Goods {

    public BakeryShop sceneManager;

    private Goods instance;
	public string name;
	public int price;
	
	public Goods() {
		Debug.Log("Starting Goods");

        sceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BakeryShop>();        

        if(sceneManager.CanSellGoodLists.Count != 0) {
            int r = Random.Range(0, sceneManager.CanSellGoodLists.Count);
            instance = sceneManager.CanSellGoodLists[r];
			this.name = instance.name;
			this.price = instance.price;
        }
        else {
            Debug.LogWarning("CanSellGoodLists.Count == 0");
        }
	}
	
	public Goods(string Init_name, int p_price) {
		this.instance = this;
		this.instance.name = Init_name;
		this.instance.price = p_price;
	}
}
