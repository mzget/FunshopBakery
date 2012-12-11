using UnityEngine;
using System.Collections;

public class UpgradeInsideManager : MonoBehaviour {
	
	public GameObject[,] upgradeButton_Objs = new GameObject[2,4];
	private tk2dSprite[,] upgradeButton_Sprites = new tk2dSprite[2,4];
	
	public GameObject[,] upgradeInsideObj2D = new GameObject[2,4];
	private tk2dSprite[,] upgradeInsideSprite2D = new tk2dSprite[2, 4];
    private tk2dTextMesh[,] upgradeInside_PriceTextmesh = new tk2dTextMesh[2, 4];
	private const int ActiveUpgradeButtonID = 25;
	private const int UnActiveUpgradeButtonID = 29;
	
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
        {2200,2300,0,0},
    };
	
	private int pageIndex = 0;
    private const int MAX_PAGE_NUMBER = 3;
    public tk2dTextMesh displayCurrentPageID_Textmesh;

	/// <summary>
	/// Calss references.
	/// </summary>
	private SheepBank sceneController;
	
	
	// Use this for initialization
	void Start () {
		pageIndex = 0;

		var controller = GameObject.FindGameObjectWithTag("GameController");
		sceneController = controller.GetComponent<SheepBank>();
	}
	
	internal void ReInitializeData() {
		pageIndex = 0;
		CalculateObjectsToDisplay();
	}
	
	// Update is called once per frame
	void Update () {
		if(_isInitialize == false)
			InitailizeDataFields();
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
					
					upgradeButton_Sprites[i,j] = upgradeButton_Objs[i,j].GetComponent<tk2dSprite>();
				}
			}
		}	
		
		_isInitialize  = true;
		CalculateObjectsToDisplay();
	}
	
	public void GotoNextPage() {
	    foreach(GameObject obj in upgradeInsideObj2D) {
            obj.animation.Play();
        }
		
		if(pageIndex < MAX_PAGE_NUMBER - 1)	pageIndex += 1;
		else pageIndex = 0;
		
        CalculateObjectsToDisplay();
	}
	
	public void BackToPreviousPage() {		
	    foreach(GameObject obj in upgradeInsideObj2D) {
            obj.animation.Play();
        }

        if(pageIndex > 0)             
			pageIndex -= 1;
		else
			pageIndex = MAX_PAGE_NUMBER - 1;
		
		CalculateObjectsToDisplay();
	}

    private void CalculateObjectsToDisplay()
    {
        if(pageIndex == 0)
        {
            #region <@-- Page index == 0

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = firstPage_spriteNames[i, j];
                    upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    upgradeInside_PriceTextmesh[i, j].text = firstPage_prices[i, j].ToString();
                    upgradeInside_PriceTextmesh[i, j].Commit();

					upgradeButton_Objs[i,j].active = true;
//					upgradeButton_Objs[i,j].collider.enabled = true;
					upgradeButton_Sprites[i,j].spriteId = ActiveUpgradeButtonID;
                }
            }			
			
			if(BakeryShop.NumberOfCansellItem.Contains(6) || Mz_StorageManage.AccountBalance < firstPage_prices[0,0]) {
				upgradeButton_Sprites[0,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,0].collider.enabled = false;
			}
//			if(BakeryShop.NumberOfCansellItem.Contains(10) || Mz_StorageManage.AccountBalance < firstPage_prices[0,1]) {
//				upgradeButton_Sprites[0,1].spriteId = UnActiveUpgradeButtonID;
////				upgradeButton_Objs[0,1].collider.enabled = false;
//			}
			if(CreamBeh.arr_CreamBehs[1] != string.Empty || Mz_StorageManage.AccountBalance < firstPage_prices[0,1]) {
				upgradeButton_Sprites[0,1].spriteId = UnActiveUpgradeButtonID;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(12) || BakeryShop.NumberOfCansellItem.Contains(13) || Mz_StorageManage.AccountBalance < firstPage_prices[0,2]) {
				upgradeButton_Sprites[0,2].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,2].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(15) || BakeryShop.NumberOfCansellItem.Contains(16) || Mz_StorageManage.AccountBalance < firstPage_prices[0,3]) {
				upgradeButton_Sprites[0,3].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,3].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(19) || Mz_StorageManage.AccountBalance < firstPage_prices[1,0]) {
				upgradeButton_Sprites[1,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,0].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(21) || Mz_StorageManage.AccountBalance < firstPage_prices[1,1]) {
				upgradeButton_Sprites[1,1].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,1].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(25) || Mz_StorageManage.AccountBalance < firstPage_prices[1,2]) {
				upgradeButton_Sprites[1,2].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,2].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(28) || Mz_StorageManage.AccountBalance < firstPage_prices[1,3])	{
				upgradeButton_Sprites[1,3].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,3].collider.enabled = false;
            }

            #endregion
        }
        else if(pageIndex == 1)
        {
            #region <@-- Page index = 1;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = secondPage_spriteNames[i, j];
                    upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    upgradeInside_PriceTextmesh[i, j].text = secondPage_prices[i, j].ToString();
                    upgradeInside_PriceTextmesh[i, j].Commit();
					
					upgradeButton_Objs[i,j].active = true;
					upgradeButton_Objs[i,j].collider.enabled = true;
					upgradeButton_Sprites[i,j].spriteId = ActiveUpgradeButtonID;
                }
            }
			
			if(BakeryShop.NumberOfCansellItem.Contains(1) || Mz_StorageManage.AccountBalance < secondPage_prices[0,0]) {
				upgradeButton_Sprites[0,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,0].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(2) || Mz_StorageManage.AccountBalance < secondPage_prices[0,1]) {
				upgradeButton_Sprites[0,1].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,1].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(7) || Mz_StorageManage.AccountBalance < secondPage_prices[0,2]) {
				upgradeButton_Sprites[0,2].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,2].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(11) || BakeryShop.NumberOfCansellItem.Contains(14) || BakeryShop.NumberOfCansellItem.Contains(17) ||
				Mz_StorageManage.AccountBalance < secondPage_prices[0,3]) {
				upgradeButton_Sprites[0,3].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,3].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(20) || Mz_StorageManage.AccountBalance < secondPage_prices[1,0]) {
				upgradeButton_Sprites[1,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,0].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(22) || Mz_StorageManage.AccountBalance < secondPage_prices[1,1])	{
				upgradeButton_Sprites[1,1].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,1].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(26) || Mz_StorageManage.AccountBalance < secondPage_prices[1,2]) {
				upgradeButton_Sprites[1,2].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,2].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(3) || Mz_StorageManage.AccountBalance < secondPage_prices[1,3]) {
				upgradeButton_Sprites[1,3].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,3].collider.enabled = false;
            }

            #endregion
        }
        else if(pageIndex == 2)
        {
            #region <@-- Page index == 2

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string nameSpecify = thirdPage_spriteNames[i, j];
					if(nameSpecify != "") {
                    	upgradeInsideSprite2D[i, j].spriteId = upgradeInsideSprite2D[i, j].GetSpriteIdByName(nameSpecify);
                    	upgradeInside_PriceTextmesh[i, j].text = thirdPage_prices[i, j].ToString();
                    	upgradeInside_PriceTextmesh[i, j].Commit();
						
						upgradeButton_Objs[i,j].active = true;
						upgradeButton_Objs[i,j].collider.enabled = true;
						upgradeButton_Sprites[i,j].spriteId = ActiveUpgradeButtonID;
					}
					else {
						tk2dSprite sprite = upgradeInsideSprite2D[i,j];
						tk2dTextMesh textmesh = upgradeInside_PriceTextmesh[i, j];
						sprite.spriteId = 27;
						textmesh.text = "none";
						textmesh.transform.localPosition = new Vector3(1, textmesh.transform.localPosition.y, textmesh.transform.localPosition.z);
						textmesh.Commit();
						
						upgradeButton_Objs[i,j].active = false;
					}
                }
            }
			
			if(BakeryShop.NumberOfCansellItem.Contains(4) || Mz_StorageManage.AccountBalance < thirdPage_prices[0,0]) {
				upgradeButton_Sprites[0,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,0].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(8) || Mz_StorageManage.AccountBalance < thirdPage_prices[0,1]) {
				upgradeButton_Sprites[0,1].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,1].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(23) || Mz_StorageManage.AccountBalance < thirdPage_prices[0,2]) {
				upgradeButton_Sprites[0,2].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,2].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(24) || Mz_StorageManage.AccountBalance < thirdPage_prices[0,3]) {
				upgradeButton_Sprites[0,3].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[0,3].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(27) || Mz_StorageManage.AccountBalance < thirdPage_prices[1,0]) {
				upgradeButton_Sprites[1,0].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,0].collider.enabled = false;
			}
			if(BakeryShop.NumberOfCansellItem.Contains(29) || Mz_StorageManage.AccountBalance < thirdPage_prices[1,1]) {
				upgradeButton_Sprites[1,1].spriteId = UnActiveUpgradeButtonID;
//				upgradeButton_Objs[1,1].collider.enabled = false;
            }

            #endregion
        }

        int temp_pageID = pageIndex + 1;
        displayCurrentPageID_Textmesh.text = temp_pageID + "/" + MAX_PAGE_NUMBER;
        displayCurrentPageID_Textmesh.Commit();
    }

    public void BuyingUpgradeMechanism(string upgradeName) {
        if(pageIndex == 0) {
			#region <!-- page 0, low 0.
			
            if(upgradeName == "upgrade_00")
            {
                #region <@-- "buying : blueberry_jam".

                if (Mz_StorageManage.AccountBalance >= firstPage_prices[0,0]) 
				{
					if(BakeryShop.NumberOfCansellItem.Contains(6) == false) {
						BakeryShop.NumberOfCansellItem.Add(6);	
						

						this.BuyingUpgradeComplete(0, 0);
                    	Debug.Log("buying : blueberry_jam");
					}
					else{
						Debug.Log("This Item has be upgraded");
						this.PlaySoundWarning();
					}
                }
				else {
					Debug.Log("Connot upgrade this item because availabel money is not enough.");
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
                }

                #endregion
            }
            else if(upgradeName == "upgrade_01")
            {
                #region <@-- "buying : blueberry_cream"

                if (Mz_StorageManage.AccountBalance >= firstPage_prices[0,1]) 
				{					
					if(CreamBeh.arr_CreamBehs[1] == string.Empty) {
						CreamBeh.arr_CreamBehs[1] = CreamBeh.BlueberryCream;

                        if (BakeryShop.NumberOfCansellItem.Contains(9)  // Chocolate_cupcake.
                            && BakeryShop.NumberOfCansellItem.Contains(10) == false)
                            BakeryShop.NumberOfCansellItem.Add(10);     // Add Blueberry_cupcake.

                        if (BakeryShop.NumberOfCansellItem.Contains(12) //Chocolate_minicake.
                            && BakeryShop.NumberOfCansellItem.Contains(13) == false)
                            BakeryShop.NumberOfCansellItem.Add(13);     // Add Blueberry_minicake.

                        if (BakeryShop.NumberOfCansellItem.Contains(15) // Chocolate_cake.
                            && BakeryShop.NumberOfCansellItem.Contains(16) == false)   
                            BakeryShop.NumberOfCansellItem.Add(16);     // Add Blueberry_cake.

						this.BuyingUpgradeComplete(0,1);
						Debug.Log("buying : blueberry_cream");
					}
					else{
						Debug.Log("This Item has be upgraded");
						this.PlaySoundWarning();
					}
                }
				else {
					Debug.Log("Connot upgrade this item because availabel money is not enough.");
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
                }

                #endregion
            }
			else if(upgradeName == "upgrade_02")
            {
                #region <@-- buying : Mini Cake.

                if (Mz_StorageManage.AccountBalance >= firstPage_prices[0,2])
				{
					if(!BakeryShop.NumberOfCansellItem.Contains(12) &&  CreamBeh.arr_CreamBehs[0] == CreamBeh.ChocolateCream
					   || !BakeryShop.NumberOfCansellItem.Contains(13) && CreamBeh.arr_CreamBehs[1] == CreamBeh.BlueberryCream
					   || !BakeryShop.NumberOfCansellItem.Contains(14) && CreamBeh.arr_CreamBehs[2] == CreamBeh.StrawberryCream)
					{
						if(BakeryShop.NumberOfCansellItem.Contains(12) == false && CreamBeh.arr_CreamBehs[0] == CreamBeh.ChocolateCream)
							BakeryShop.NumberOfCansellItem.Add(12);	//Chocolate_minicake.
						if(BakeryShop.NumberOfCansellItem.Contains(13) == false && CreamBeh.arr_CreamBehs[1] == CreamBeh.BlueberryCream)
							BakeryShop.NumberOfCansellItem.Add(13);	//Blueberry_minicake.
						if(BakeryShop.NumberOfCansellItem.Contains(14) == false && CreamBeh.arr_CreamBehs[2] == CreamBeh.StrawberryCream)
							BakeryShop.NumberOfCansellItem.Add(14); //Strawberry_minicake.

						this.BuyingUpgradeComplete(0,2);
						Debug.Log("buying : Mini Cake");
					}
					else{
						Debug.Log("This Item has be upgraded");
						this.PlaySoundWarning();
					}
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
                }

                #endregion
            }
			else if(upgradeName == "upgrade_03")
            {
                #region <@-- "buying : Cake".

                if (Mz_StorageManage.AccountBalance >= firstPage_prices[0, 3]) {
                    if(BakeryShop.NumberOfCansellItem.Contains(15) == false && CreamBeh.arr_CreamBehs[0] == CreamBeh.ChocolateCream
                        || BakeryShop.NumberOfCansellItem.Contains(16) == false && CreamBeh.arr_CreamBehs[1] == CreamBeh.BlueberryCream
                        || BakeryShop.NumberOfCansellItem.Contains(17) == false && CreamBeh.arr_CreamBehs[2] == CreamBeh.StrawberryCream)
                    {
					    if(BakeryShop.NumberOfCansellItem.Contains(15) == false && CreamBeh.arr_CreamBehs[0] == CreamBeh.ChocolateCream)
						    BakeryShop.NumberOfCansellItem.Add(15);	// Add Chocolate_cake.
					    if(BakeryShop.NumberOfCansellItem.Contains(16) == false && CreamBeh.arr_CreamBehs[1] == CreamBeh.BlueberryCream)
						    BakeryShop.NumberOfCansellItem.Add(16);	// Add Blueberry_cake.
						if(BakeryShop.NumberOfCansellItem.Contains(17) == false && CreamBeh.arr_CreamBehs[2] == CreamBeh.StrawberryCream)
							BakeryShop.NumberOfCansellItem.Add(17); // Add Strawberry_cake.

					    this.BuyingUpgradeComplete(0,3);
                        Debug.Log("buying : Cake");
                    }
					else{
						Debug.Log("This Item has be upgraded");
						this.PlaySoundWarning();
					}
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
                }

                #endregion
            }
			
			#endregion 
			#region <!-- page 0, Low 1.
			
			else if(upgradeName == "upgrade_10") {
				if(Mz_StorageManage.AccountBalance >= firstPage_prices[1, 0]) {
                    Debug.Log("buying : vanilla_icecream");
                    Mz_StorageManage.AccountBalance -= firstPage_prices[1, 0];
					
					if(BakeryShop.NumberOfCansellItem.Contains(19) == false)
						BakeryShop.NumberOfCansellItem.Add(19);	

					upgradeButton_Sprites[1,0].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= firstPage_prices[1, 0];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_11") {				
				if(Mz_StorageManage.AccountBalance >= firstPage_prices[1, 1]) {
                    Debug.Log("buying : tuna_sandwich");
					
					if(BakeryShop.NumberOfCansellItem.Contains(21) == false)
						BakeryShop.NumberOfCansellItem.Add(21);	

					upgradeButton_Sprites[1,1].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= firstPage_prices[1, 1];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_12") {				
				if(Mz_StorageManage.AccountBalance >= firstPage_prices[1, 2]) {
                    Debug.Log("buying : chocolate_chip_cookie");
					
					if(BakeryShop.NumberOfCansellItem.Contains(25) == false)
						BakeryShop.NumberOfCansellItem.Add(25);	

					upgradeButton_Sprites[1,2].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= firstPage_prices[1, 2];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_13") {				
				if(Mz_StorageManage.AccountBalance >= firstPage_prices[1, 3]) {
                    Debug.Log("buying : hotdog");
					
					if(BakeryShop.NumberOfCansellItem.Contains(28) == false)
						BakeryShop.NumberOfCansellItem.Add(28);	

					upgradeButton_Sprites[1,3].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= firstPage_prices[1, 3];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			
			#endregion
        }
		else if(pageIndex == 1) {
			#region <!-- Page 1 low 0.
			
			if(upgradeName == "upgrade_00") {				
                if(Mz_StorageManage.AccountBalance >= secondPage_prices[0,0]) {
                    Debug.Log("buying : appleJuiceTank");
					
					if(BakeryShop.NumberOfCansellItem.Contains(1) == false)
						BakeryShop.NumberOfCansellItem.Add(1);	
					
					upgradeButton_Sprites[0,0].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[0, 0];
                    CalculateObjectsToDisplay();
                }
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_01") {		
                if(Mz_StorageManage.AccountBalance >= secondPage_prices[0,1]) {
                    Debug.Log("buying : chocolateMilkTank");
					
					if(BakeryShop.NumberOfCansellItem.Contains(2) == false)
						BakeryShop.NumberOfCansellItem.Add(2);	
					
					upgradeButton_Sprites[0,1].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[0, 1];
                    CalculateObjectsToDisplay();
                }
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_02") {
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[0,2]) {
					Debug.Log("buying : butter_jam");
					
					if(BakeryShop.NumberOfCansellItem.Contains(7) == false)
						BakeryShop.NumberOfCansellItem.Add(7);	
					
					upgradeButton_Sprites[0,2].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[0, 2];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_03") {
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[0,3]) {
					Debug.Log("buying : strawberry_cream");
					
					if(BakeryShop.NumberOfCansellItem.Contains(11) == false) 
						BakeryShop.NumberOfCansellItem.Add(11);	
					if(BakeryShop.NumberOfCansellItem.Contains(14) == false)
						BakeryShop.NumberOfCansellItem.Add(14);	
					if(BakeryShop.NumberOfCansellItem.Contains(17) == false)
						BakeryShop.NumberOfCansellItem.Add(17);	
					
					upgradeButton_Sprites[0,3].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[0, 3];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			
			#endregion
			#region <!-- page1 low 1.

			if(upgradeName == "upgrade_10") {				
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[1,0]) {
					Debug.Log("buying : chocolate_icecream");
					
					if(BakeryShop.NumberOfCansellItem.Contains(20) == false)
						BakeryShop.NumberOfCansellItem.Add(20);	
					
					upgradeButton_Sprites[1,0].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[1, 0];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_11") {				
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[1,1]) {
					Debug.Log("buying : deep_fried_chicken_sandwich");
					
					if(BakeryShop.NumberOfCansellItem.Contains(22) == false)
						BakeryShop.NumberOfCansellItem.Add(22);	
					
					upgradeButton_Sprites[1,1].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[1, 1];
                    CalculateObjectsToDisplay();
				}
			}
			else if(upgradeName == "upgrade_12") {				
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[1,2]) {
					Debug.Log("buying : fruit_cookie");
					
					if(BakeryShop.NumberOfCansellItem.Contains(26) == false)
						BakeryShop.NumberOfCansellItem.Add(26);	
					
					upgradeButton_Sprites[1,2].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[1, 2];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_13") {				
				if(Mz_StorageManage.AccountBalance >= secondPage_prices[1,3]) {
					Debug.Log("buying : orangeJuiceTank");
					
					if(BakeryShop.NumberOfCansellItem.Contains(3) == false)
						BakeryShop.NumberOfCansellItem.Add(3);	
					
					upgradeButton_Sprites[1,3].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= secondPage_prices[1, 3];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}

			#endregion
		}
		else if(pageIndex == 2) {
			#region <!-- page2 low 0.
			
			if(upgradeName == "upgrade_00") {				
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[0,0]) {
					Debug.Log("buying : freshMilkTank");
					
					if(BakeryShop.NumberOfCansellItem.Contains(4) == false)
						BakeryShop.NumberOfCansellItem.Add(4);	
					
					upgradeButton_Sprites[0, 0].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[0, 0];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_01") {		
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[0,1]) {
					Debug.Log("buying : custard_jam");
					
					if(BakeryShop.NumberOfCansellItem.Contains(8) == false)
						BakeryShop.NumberOfCansellItem.Add(8);	
					
					upgradeButton_Sprites[0, 1].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[0, 1];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_02") {
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[0,2]) {
					Debug.Log("buying : ham_sandwich");
					
					if(BakeryShop.NumberOfCansellItem.Contains(23) == false)
						BakeryShop.NumberOfCansellItem.Add(23);	
					
					upgradeButton_Sprites[0, 2].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[0, 2];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_03") {
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[0,3]) {
					Debug.Log("buying : egg_sandwich");
					
					if(BakeryShop.NumberOfCansellItem.Contains(24) == false) 
						BakeryShop.NumberOfCansellItem.Add(24);	
					
					upgradeButton_Sprites[0, 3].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[0, 3];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}

			#endregion
			#region <!-- page2 low 1.

			if(upgradeName == "upgrade_10") {				
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[1,0]) {
					Debug.Log("buying : butter_cookie");
					
					if(BakeryShop.NumberOfCansellItem.Contains(27) == false)
						BakeryShop.NumberOfCansellItem.Add(27);	
					
					upgradeButton_Sprites[1, 0].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[1, 0];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}
			else if(upgradeName == "upgrade_11") {		
				if(Mz_StorageManage.AccountBalance >= thirdPage_prices[1,1]) {
					Debug.Log("buying : hotdog_cheese");
					
					if(BakeryShop.NumberOfCansellItem.Contains(29) == false)
						BakeryShop.NumberOfCansellItem.Add(29);	

					upgradeButton_Sprites[0, 1].spriteId = 28;
                    Mz_StorageManage.AccountBalance -= thirdPage_prices[1, 1];
                    CalculateObjectsToDisplay();
				}
				else {
					sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
				}
			}

			#endregion
		}
    }

	void BuyingUpgradeComplete (int i, int j)
	{
		Mz_StorageManage.AccountBalance -= firstPage_prices[i, j];
		upgradeButton_Sprites[i,j].spriteId = 28;
		CalculateObjectsToDisplay();
		sceneController.ManageAvailabelMoneyBillBoard();
		sceneController.effectManager.Create2DSpriteAnimationEffect(GameEffectManager.BLOOMSTAR_EFFECT_PATH, upgradeButton_Objs[i,j].transform);
		sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);
	}

	void PlaySoundWarning ()
	{
		sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);
	}
}
