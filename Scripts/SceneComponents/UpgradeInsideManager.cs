using UnityEngine;
using System.Collections;

public class UpgradeInsideManager : MonoBehaviour {
	
	
	public GameObject[,] upgradeInsideObj2D = new GameObject[2,4];
    private tk2dSprite[,] upgradeInsideSprite2D = new tk2dSprite[2, 4];
    private tk2dTextMesh[,] upgradeInside_PriceTextmesh = new tk2dTextMesh[2, 4];
    private string[,] firstPage_spriteNames = new string[2, 4] {
       {"blueberry_jam", "blueberry_cream", "miniCake", "Cake"},
       {"vanilla_icecream", "tuna_sandwich", "chocolate_chip_cookie", "hotdog"},
    };
    private string[,] secondPage_spriteNames = new string[2, 4] {
        {"appleJuiceTank", "chocolateMilkTank", "butter_jam", "strawberry_cream"},
        {"chocolate_icecream", "deep_fried_chicken_sandwich", "fruit_cookie", "orangeJuiceTank"},
    };
    private string[,] thirdPage_spriteNames = new string[2, 4] {
		{"freshMilkTank", "custard_jam", "ham_sandwich", "egg_sandwich"},
		{"butter_cookie", "hotdog_cheese", "", ""},
	};
    private int[,] firstPage_prices = new int[,] {
        {200, 300, 400, 500},
        {600, 700, 800, 900},
    };
    private int[,] secondPage_prices = new int[,] {
        {1000,1100,1200,1300},
        {1400,1500,1600,1700},
    };
    private int[,] thirdPage_prices = new int[,] {
        {1800,1900,2000,2100},
        {2200,2300,2400,2500},
    };
	
	private int pageIndex = 0;	
	
	
	// Use this for initialization
	void Start () {
		pageIndex = 0;
	}
	
	private bool _isInitialize = false;
	void InitailizeDataFields() {
	    if(upgradeInsideSprite2D[0,0] == null) 
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    upgradeInsideSprite2D[i, j] = upgradeInsideObj2D[i, j].GetComponent<tk2dSprite>();
                    upgradeInside_PriceTextmesh[i, j] = upgradeInsideObj2D[i, j].transform.GetComponentInChildren<tk2dTextMesh>();
                }
            }
        }		
		
		_isInitialize  = true;
		CalculateObjectsToDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		if(_isInitialize == false)
			InitailizeDataFields();
	}
	
	public void GotoNextPage() {
	    foreach(GameObject obj in upgradeInsideObj2D) {
            obj.animation.Play();
        }
		
		if(pageIndex < 2)			pageIndex += 1;
		else pageIndex = 0;
		

        CalculateObjectsToDisplay();
	}
	
	public void BackToPreviousPage() {		
	    foreach(GameObject obj in upgradeInsideObj2D) {
            obj.animation.Play();
        }

        if(pageIndex > 0)             pageIndex -= 1;
		else			pageIndex = 2;
		
		CalculateObjectsToDisplay();
	}

    private void CalculateObjectsToDisplay()
    {
        if(pageIndex == 0) {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = firstPage_spriteNames[i, j];
                    upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    upgradeInside_PriceTextmesh[i, j].text = firstPage_prices[i, j].ToString();
                    upgradeInside_PriceTextmesh[i, j].Commit();
                }
            }
        }
        else if(pageIndex == 1) {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = secondPage_spriteNames[i, j];
                    upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    upgradeInside_PriceTextmesh[i, j].text = secondPage_prices[i, j].ToString();
                    upgradeInside_PriceTextmesh[i, j].Commit();
                }
            }
        }
        else if(pageIndex == 2) {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = thirdPage_spriteNames[i, j];
                    upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    upgradeInside_PriceTextmesh[i, j].text = thirdPage_prices[i, j].ToString();
                    upgradeInside_PriceTextmesh[i, j].Commit();
                }
            }
        }
    }

    public void BuyingUpgradeMechanism(string upgradeName) {
        if(pageIndex == 0) {
            if(upgradeName == "upgrade_00") {
                if(StorageManage.Money >= firstPage_prices[0,0]) {
                    Debug.Log("buying : blueberry_jam");

                    BakeryShop.CanSellGoodLists.Add(new GoodDataStore().Menu_list[6]);
                }
            }
            else if(upgradeName == "upgrade_01") {
                if(StorageManage.Money >= firstPage_prices[0,1]) {
                    Debug.Log("buying : blueberry_cream");

                    BakeryShop.CanSellGoodLists.Add(new GoodDataStore().Menu_list[10]);
                }
            }
        }
    }
}
