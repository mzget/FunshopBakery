using UnityEngine;
using System.Collections;


public class Goods {

    public BakeryShop sceneManager;
    private Goods instance;
	public string name;
	public int price;
	
	public Goods ()
	{
		Debug.Log ("Starting Goods");

        sceneManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<BakeryShop>();        

		if (sceneManager.currentCustomer.list_goodsBag.Count > 0) {
			int r = Random.Range(0, sceneManager.currentCustomer.list_goodsBag.Count);

			instance = sceneManager.currentCustomer.list_goodsBag[r];
			this.name = instance.name;
			this.price = instance.price;	

			sceneManager.currentCustomer.list_goodsBag.Remove(instance);	

			Debug.Log("list_goodsBag.Count : " + sceneManager.currentCustomer.list_goodsBag.Count);
		}
        else {
			Debug.LogError("CustomerInstance.arr_goodsBag.Length == 0");
        }
	}
	
	public Goods(string Init_name, int p_price) {
		this.instance = this;
		this.instance.name = Init_name;
		this.instance.price = p_price;
	}
}
