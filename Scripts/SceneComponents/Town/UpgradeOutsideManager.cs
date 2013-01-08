using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class RoofDataCollection {
    public readonly Vector3[] offsetPosY = new Vector3[7] {
        Vector3.forward*0.8f, Vector3.forward*0.8f, Vector3.forward*0.8f, Vector3.forward*0.8f,
        Vector3.forward*0.8f, new Vector3(0, -.25f,  0.8f) , new Vector3(0, -.25f, 0.8f),
    };

	public readonly string[] NameSpecify = new string[7] {
		"roof_0001", "roof_0002", "roof_0003", "roof_0004", "roof_0005", "roof_0006", "roof_0007",
	};

	public readonly  int[] upgradePrice = new int[7] {
		1000, 1500, 2000, 2800, 3200, 3200, 3200, 
	};
};

class AwningDataCollection {

	public readonly string[] NameSpecify = new string[7] {
		"awning_0001", "awning_0002", "awning_0003", "awning_0004", "awning_0005", "awning_0006", "awning_0007",
	};

	public readonly int[] upgradePrice = new int[7] {
		1000, 1500, 2000, 2800, 3200, 3200, 3200, 
	} ;
}

class TableDataCollection {
	public readonly string[] NameSpecify = new string[16] {
		"Table_0001", "Table_0002", "Table_0003", "Table_0004", "Table_0005", "Table_0006", "Table_0007",
		"Table_0008", "Table_0009", "Table_0010", "Table_0011", "Table_0012", "Table_0013", "Table_0014",
		"Table_0015", "Table_0016", 
	};
	
	public readonly int[] upgradePrices = new int[16] {
		500, 600, 700, 800, 900, 1000, 1100, 
		1200, 1300, 1400, 1500, 1600, 1700, 1800,
		1900, 2000,
	} ;
};

class AccessoriesDatacollecction {
	public readonly string[] NameSpeccify = new string[14] {
		"access_0001", "access_0002", "access_0003", "access_0004", "access_0005", "access_0006", "access_0007", 
		"access_0008", "access_0009", "access_0010", "access_0011", "access_0012", "access_0013", "access_0014", 
	};

	public readonly int[] upgradePrices  = new int[14] {
		500, 600, 700, 800, 900, 1000, 1100, 
		1200, 1300, 1400, 1500, 1600, 1700, 1800,
	};
};

public class UpgradeOutsideManager : MonoBehaviour
{
	public const string Roof_button = "roof_button";
	public const string Table_button = "table_button";
	public const string Awning_button = "awning_button";
	public const string Accessories_button = "accessories_button";

    public static List<int> CanDecorateRoof_list = new List<int>();
	public static List<int> CanDecorateAwning_list = new List<int>();
	public static List<int> CanDecoration_Table_list = new List<int>();
	public static List<int> CanDecoration_Accessories_list = new List<int>();

    private Town sceneController;
	private RoofDataCollection roofData = new RoofDataCollection();
	private AwningDataCollection awningData = new AwningDataCollection();
	private TableDataCollection tableData = new TableDataCollection();
	private AccessoriesDatacollecction accessoriesData = new AccessoriesDatacollecction();

    public tk2dSprite roofDecoration_Sprite;
    public tk2dSprite awningDecoration_Sprite;
    public tk2dSprite tableDecoration_Sprite;
    public tk2dSprite accessories_Sprite; 
	public GameObject[] upgrade_Objs = new GameObject[7];
	tk2dSprite[] upgrade_sprites = new tk2dSprite[7];	
	public tk2dTextMesh[] itemPrice_textmesh = new tk2dTextMesh[7];
	public GameObject closeButton_Obj;
	public GameObject noneButton_Obj;
	public GameObject previousButton_Obj;
	public GameObject nextButton_Obj;
    public GameObject confirmWindow_Obj;
	public tk2dTextMesh accountBalance_Textmesh;


	public enum StateBehavior { activeRoof = 0, activeAwning, activeTable, activeAccessories };
	public StateBehavior currentStateBehavior;
	int amountPages;
	int currentPage;
    private int transaction_id = 0;
	
	// Use this for initialization
	void Start ()
	{
        GameObject scene = GameObject.FindGameObjectWithTag("GameController");
        sceneController = scene.GetComponent<Town>();

		for (int i = 0; i < upgrade_Objs.Length; i++) {
			upgrade_sprites[i] = upgrade_Objs[i].GetComponent<tk2dSprite>();
		}

		this.ReFreshAccountBalanceTextDisplay();
	}

	void ReFreshAccountBalanceTextDisplay ()
	{
		accountBalance_Textmesh.text = Mz_StorageManage.AccountBalance.ToString();
		accountBalance_Textmesh.Commit();
	}
	
	// Update is called once per frame
    void Update() { }

    public void InitializeDecorationObjects()
    {
        if(Mz_StorageManage.Roof_id == 255) {
            roofDecoration_Sprite.gameObject.active = false;
        }
        else {
			roofDecoration_Sprite.gameObject.active = true;
            roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofData.NameSpecify[Mz_StorageManage.Roof_id]);
            roofDecoration_Sprite.gameObject.transform.localPosition = roofData.offsetPosY[Mz_StorageManage.Roof_id]; 
        }
        
        if(Mz_StorageManage.Awning_id == 255) {
			awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName("DefaultShop_Awning");		
        }
        else {
			awningDecoration_Sprite.gameObject.active = true;
            awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningData.NameSpecify[Mz_StorageManage.Awning_id]);
        }

        if(Mz_StorageManage.Table_id == 255) {
            tableDecoration_Sprite.gameObject.active = false;
        }
        else {
			tableDecoration_Sprite.gameObject.active = true;
			tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tableData.NameSpecify[Mz_StorageManage.Table_id]);
        }

        if(Mz_StorageManage.Accessory_id == 255) {
            accessories_Sprite.gameObject.active = false;
        }
        else {
			accessories_Sprite.gameObject.active = true;
			accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesData.NameSpeccify[Mz_StorageManage.Accessory_id]);
        }
    }

	public void HaveNoneCommand ()
	{
		if (currentStateBehavior == StateBehavior.activeRoof) {
			roofDecoration_Sprite.gameObject.active = false;
			Mz_StorageManage.Roof_id = 255;
		}
        else if (currentStateBehavior == StateBehavior.activeAwning) {
			awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName("DefaultShop_Awning");
            Mz_StorageManage.Awning_id = 255;
		} 
        else if(currentStateBehavior == StateBehavior.activeTable) {
            tableDecoration_Sprite.gameObject.active = false;
            Mz_StorageManage.Table_id = 255;
        } 
        else if(currentStateBehavior == StateBehavior.activeAccessories) {
            accessories_Sprite.gameObject.active = false;
            Mz_StorageManage.Accessory_id = 255;
        }
	}
	
	#region <!-- Active header objects.
	
	public void ActiveRoof ()
	{
		for (int i = 0; i < 7; i++) {
			upgrade_sprites[i].spriteId = upgrade_sprites [0].GetSpriteIdByName (roofData.NameSpecify [i]);
			
			if(UpgradeOutsideManager.CanDecorateRoof_list.Contains(i) == false) {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
				itemPrice_textmesh[i].text = roofData.upgradePrice[i].ToString();
				itemPrice_textmesh[i].Commit();
			}
			else {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
			}
		}

		previousButton_Obj.active = false;
		nextButton_Obj.active = false;

		currentStateBehavior = StateBehavior.activeRoof;
		currentPage = 0;
	}

	public void ActiveAwning() {		
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (awningData.NameSpecify[i]);
			if(UpgradeOutsideManager.CanDecorateAwning_list.Contains(i) == false) {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
				itemPrice_textmesh[i].text = awningData.upgradePrice[i].ToString();
				itemPrice_textmesh[i].Commit();
			}
			else {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
			}
		}
		
		previousButton_Obj.active = false;
		nextButton_Obj.active = false;

		currentStateBehavior = StateBehavior.activeAwning;
		currentPage = 0;
	}

	public void ActiveTable ()
	{
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (tableData.NameSpecify[i]);

			if(UpgradeOutsideManager.CanDecoration_Table_list.Contains(i) == false) {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
				itemPrice_textmesh[i].text = tableData.upgradePrices[i].ToString();
				itemPrice_textmesh[i].Commit();
			}
			else {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
			}
		}
		
		previousButton_Obj.active = true;
		nextButton_Obj.active = true;

		currentStateBehavior = StateBehavior.activeTable;
		currentPage = 0;
	}

	public void ActiveAccessories ()
	{
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (accessoriesData.NameSpeccify[i]);

			if(UpgradeOutsideManager.CanDecoration_Accessories_list.Contains(i) == false) {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
				itemPrice_textmesh[i].text = accessoriesData.upgradePrices[i].ToString();
				itemPrice_textmesh[i].Commit();
			}
			else {
				itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
			}
		}
		
		previousButton_Obj.active = true;
		nextButton_Obj.active = true;
		
		currentStateBehavior = StateBehavior.activeAccessories;
		currentPage = 0;
	}
	
	#endregion
	
	public void HaveNextPageCommand() {
		if(currentStateBehavior == StateBehavior.activeTable)
			amountPages = 2;
		else if(currentStateBehavior == StateBehavior.activeAccessories)
			amountPages = 2;
		
		if(currentPage < amountPages - 1)
			currentPage += 1;
		else
			currentPage = 0;

		this.GoToPage(currentPage);
	}
	
	public void HavePreviousPageCommand() {		
		if(currentStateBehavior == StateBehavior.activeTable) {
			amountPages = 2;
		}
		else if(currentStateBehavior == StateBehavior.activeAccessories)
			amountPages = 2;
		
		if(currentPage > 0)
			currentPage -= 1;
		else
			currentPage = (amountPages - 1);
		
		this.GoToPage(currentPage);
	}

	void GoToPage (int pageSpecify)
	{
		if (currentStateBehavior == StateBehavior.activeTable) {
			for (int i = 0; i < 7; i++) {
				int j = i + (7 * pageSpecify);
				if (j < tableData.NameSpecify.Length) 
                {
                    /// Display item sprite.
					int spriteID = upgrade_sprites [i].GetSpriteIdByName (tableData.NameSpecify [j]);
					upgrade_sprites [i].spriteId = spriteID;
                    //@-- Display price.
					if(UpgradeOutsideManager.CanDecoration_Table_list.Contains(j) == false) {
						itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
						itemPrice_textmesh[i].text = tableData.upgradePrices[j].ToString();
						itemPrice_textmesh[i].Commit();
					}
					else {
						itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
					}
				}
			}		
		} 
		else if (currentStateBehavior == StateBehavior.activeAccessories) {
			for (int i = 0; i < 7; i++) {
				int j = i + (7 * pageSpecify);
				if (j < accessoriesData.NameSpeccify.Length) {
                    /// Display item sprite.
					int spriteID = upgrade_sprites [i].GetSpriteIdByName (accessoriesData.NameSpeccify [j]);
					upgrade_sprites [i].spriteId = spriteID;
                    //@-- Display price.
					if(UpgradeOutsideManager.CanDecoration_Accessories_list.Contains(j) == false) {
						itemPrice_textmesh[i].gameObject.SetActiveRecursively(true);
						itemPrice_textmesh[i].text = accessoriesData.upgradePrices[j].ToString();
						itemPrice_textmesh[i].Commit();
					}
					else {
						itemPrice_textmesh[i].gameObject.SetActiveRecursively(false);
					}
				}
			}	
		}
	}

    internal void BuyDecoration(string blockName)
    {
        if(currentStateBehavior == StateBehavior.activeRoof)
        {
            #region <-- Active roof.

            switch (blockName)
            {
                case "Block_00": this.CheckingCanBuyItem(0);
                    break;
                case "Block_01": this.CheckingCanBuyItem(1);
                    break;
                case "Block_02": this.CheckingCanBuyItem(2);
                    break;
                case "Block_03": this.CheckingCanBuyItem(3);
                    break;
                case "Block_04": this.CheckingCanBuyItem(4);
                    break;
                case "Block_05": this.CheckingCanBuyItem(5);
                    break;
                case "Block_06": this.CheckingCanBuyItem(6);
                    break;
                default:
                    break;
            }

            #endregion
        }
		else if(currentStateBehavior == StateBehavior.activeAwning)
        {
            #region <!-- Active awning.

            switch (blockName) {				
			case "Block_00": this.CheckingCanBuyItem(0);
                    break;
			case "Block_01": this.CheckingCanBuyItem(1);
                    break;
			case "Block_02": this.CheckingCanBuyItem(2);
                    break;
			case "Block_03": this.CheckingCanBuyItem(3);
                    break;
			case "Block_04": this.CheckingCanBuyItem(4);
                    break;
			case "Block_05": this.CheckingCanBuyItem(5);
                    break;
			case "Block_06": this.CheckingCanBuyItem(6);
                    break;
			default:
			break;
            }

            #endregion
        }
		else if(currentStateBehavior == StateBehavior.activeTable)
        {
            #region <!-- Active table.

			int id = 0;

			switch (blockName) {				
			case "Block_00": 
				id = 0 + (7 *currentPage);
				this.CheckingCanBuyItem(id); 
                break;
			case "Block_01":
				id = 1 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_02":
                    id = 2 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_03":
                    id = 3 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_04":
				id = 4 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_05":
				id = 5 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_06":
				id = 6 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
            default:
                break;
            }

            #endregion
        }
		else if(currentStateBehavior == StateBehavior.activeAccessories)
        {
            #region <!-- Active accessory.

			int id = 0;

			switch (blockName) {				
			case "Block_00":
				id = 0 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_01": 
				id = 1 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_02": 
				id = 2 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_03": 
				id = 3 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_04":
				id = 4 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_05":
				id = 5 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			case "Block_06":
				id = 6 + (7 * currentPage);
				this.CheckingCanBuyItem(id);
                break;
			default:
			break;
            }

            #endregion
        }
    }

    private void CheckingCanBuyItem(int targetItem_id)
    {
        if (currentStateBehavior == StateBehavior.activeRoof)
        {
			if(CanDecorateRoof_list.Contains(targetItem_id) == false) {
	            if (Mz_StorageManage.AccountBalance >= roofData.upgradePrice[targetItem_id]) {
	                /// Todo... Asking to buy item.
	                confirmWindow_Obj.SetActiveRecursively(true);
	                this.PlaySoundOpenComfirmationWindow();
	                transaction_id = targetItem_id;
				}
	            else {
	                /// Todo... warning.
	                this.PlaySoundWarning();
	            }
			}
			else {
				DisplayRoof(targetItem_id);
			}
        }
		else if(currentStateBehavior == StateBehavior.activeAwning) {
			if(CanDecorateAwning_list.Contains(targetItem_id) == false) {
				if(Mz_StorageManage.AccountBalance >= awningData.upgradePrice[targetItem_id]) {					
					/// Todo... Asking to buy item.
					confirmWindow_Obj.SetActiveRecursively(true);
					this.PlaySoundOpenComfirmationWindow();
					transaction_id = targetItem_id;
				}
				else 
					this.PlaySoundWarning();
			}
			else {
				DisplayAwning(targetItem_id);
			}
		}
		else if(currentStateBehavior == StateBehavior.activeTable) {
			if(CanDecoration_Table_list.Contains(targetItem_id) == false) {
				if(Mz_StorageManage.AccountBalance >= tableData.upgradePrices[targetItem_id]) {				
					/// Todo... Asking to buy item.
					confirmWindow_Obj.SetActiveRecursively(true);
					this.PlaySoundOpenComfirmationWindow();
					transaction_id = targetItem_id;
				}
				else 
					this.PlaySoundWarning();
			}
			else {
				DisplayTable(targetItem_id);
			}
		}
		else if(currentStateBehavior == StateBehavior.activeAccessories) {
			if(CanDecoration_Accessories_list.Contains(targetItem_id) == false) {
				if(Mz_StorageManage.AccountBalance >= accessoriesData.upgradePrices[targetItem_id]) {				
					/// Todo... Asking to buy item.
					confirmWindow_Obj.SetActiveRecursively(true);
					this.PlaySoundOpenComfirmationWindow();
					transaction_id = targetItem_id;
				}
				else 
					this.PlaySoundWarning();
			}
			else
				DisplayAccessories(targetItem_id);
		}
    }

    internal void UserConfirmTransaction()
    {
        if (currentStateBehavior == StateBehavior.activeRoof)
        {
			this.DisplayRoof(transaction_id);
			/// Add transaction item to CAN_DECORATION_LIST.
			UpgradeOutsideManager.CanDecorateRoof_list.Add(transaction_id);
			//@-- Reset transaction roof page.
			this.ActiveRoof();

            //@!-- Close confirmation window.
            //@!-- Deductions AvailableMoney and Redraw GUI identity.
            confirmWindow_Obj.SetActiveRecursively(false);
            Mz_StorageManage.AccountBalance -= roofData.upgradePrice[transaction_id];
			this.ReFreshAccountBalanceTextDisplay();
        }
		else if(currentStateBehavior == StateBehavior.activeAwning) {
			this.DisplayAwning(transaction_id);			
			/// Add transaction item to CAN_DECORATION_LIST.
			UpgradeOutsideManager.CanDecorateAwning_list.Add(transaction_id);
			//@!-- Reset transaction awning page.
			this.ActiveAwning();

			//@!-- Close confirmation window.
			//@!-- Deductions AvailableMoney and Redraw GUI identity.
			confirmWindow_Obj.SetActiveRecursively(false);
			Mz_StorageManage.AccountBalance -= awningData.upgradePrice[transaction_id];
			this.ReFreshAccountBalanceTextDisplay();
		}
		else if(currentStateBehavior == StateBehavior.activeTable) {
			this.DisplayTable(transaction_id);
			// Add transaction item to CAN_DECORATION_LIST.
			UpgradeOutsideManager.CanDecoration_Table_list.Add(transaction_id);
			//@!-- Reset transaction table page.
			this.GoToPage(currentPage);
			
			//@!-- Close confirmation window.
			//@!-- Deductions AvailableMoney and Redraw GUI identity.
			confirmWindow_Obj.SetActiveRecursively(false);
			Mz_StorageManage.AccountBalance -= tableData.upgradePrices[transaction_id];
			this.ReFreshAccountBalanceTextDisplay();
		}
		else if(currentStateBehavior == StateBehavior.activeAccessories) {			
			this.DisplayAccessories(transaction_id);
			// Add transaction item to CAN_DECORATION_LIST.
			UpgradeOutsideManager.CanDecoration_Accessories_list.Add(transaction_id);
			//@!-- Reset transaction accesorries page.
			this.GoToPage(currentPage);
			
			//@!-- Close confirmation window.
			//@!-- Deductions AvailableMoney and Redraw GUI identity.
			confirmWindow_Obj.SetActiveRecursively(false);
			Mz_StorageManage.AccountBalance -= accessoriesData.upgradePrices[transaction_id];
			this.ReFreshAccountBalanceTextDisplay();
		}
    }

    internal void UserCancleTransaction()
    {
        confirmWindow_Obj.SetActiveRecursively(false);
    }

    private void PlaySoundOpenComfirmationWindow()
    {
        sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.calc_clip);
    }

    private void PlaySoundWarning()
    {
        sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.wrong_Clip);

        Debug.LogWarning("Your AccountBalance is less than item price.");
    }

	void DisplayRoof (int active_id)
	{
		roofDecoration_Sprite.gameObject.active = true;
		roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofData.NameSpecify[active_id]);
        roofDecoration_Sprite.gameObject.transform.localPosition = roofData.offsetPosY[active_id]; 
		Mz_StorageManage.Roof_id = active_id;

		sceneController.PlaySoundRejoice();
	}

	void DisplayAwning (int active_id)
	{
		awningDecoration_Sprite.gameObject.active = true;
		awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningData.NameSpecify[active_id]);
		Mz_StorageManage.Awning_id = active_id;

		sceneController.PlaySoundRejoice();
	}

	void DisplayTable (int active_id)
	{		
		tableDecoration_Sprite.gameObject.active = true;
		tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tableData.NameSpecify[active_id]);
		Mz_StorageManage.Table_id = active_id;

		sceneController.PlaySoundRejoice();
	}

	void DisplayAccessories (int targetItem_id)
	{
		accessories_Sprite.gameObject.active = true;
		accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesData.NameSpeccify[targetItem_id]);
		Mz_StorageManage.Accessory_id = targetItem_id;

		sceneController.PlaySoundRejoice();
	}

}

