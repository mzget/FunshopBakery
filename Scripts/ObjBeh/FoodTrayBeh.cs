using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodTrayBeh : ScriptableObject {
	
	public List<GoodsBeh> goodsOnTray_List = new List<GoodsBeh>();	
	
	// Use this for initialization
	public void OnEnable() {
		Debug.Log("Starting : FoodTrayBeh");
	}
}
