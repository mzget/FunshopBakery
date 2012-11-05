using UnityEngine;
using System.Collections;

public class UpgradeOutsideManager : MonoBehaviour
{
	public const string Roof_button = "roof_button";
	public const string Table_button = "table_button";
	public const string Awning_button = "awning_button";
	public const string Accessories_button = "accessories_button";

    [System.NonSerialized]
	public string[] roofsNameSpecify = new string[] {
		"roof_0001", "roof_0002", "roof_0003", "roof_0004", "roof_0005", "roof_0006", "roof_0007",
	};
    [System.NonSerialized]
	public string[] awningNameSpecify = new string[] {
		"awning_0001", "awning_0002", "awning_0003", "awning_0004", "awning_0005", "awning_0006", "awning_0007",
	};
    [System.NonSerialized]
	public string[] tablesNameSpecify = new string[] {
		"Table_0001", "Table_0002", "Table_0003", "Table_0004", "Table_0005", "Table_0006", "Table_0007",
		"Table_0008", "Table_0009", "Table_0010", "Table_0011", "Table_0012", "Table_0013", "Table_0014",
		"Table_0015", "Table_0016", 
	};
    [System.NonSerialized]
	public string[] accessoriesNameSpeccify = new string[] {
		"access_0001", "access_0002", "access_0003", "access_0004", "access_0005", "access_0006", "access_0007", 
		"access_0008", "access_0009", "access_0010", "access_0011", "access_0012", "access_0013", "access_0014", 
	};

    public tk2dSprite roofDecoration_Sprite;
    public tk2dSprite awningDecoration_Sprite;
    public tk2dSprite tableDecoration_Sprite;
    public tk2dSprite accessories_Sprite; 
	public GameObject[] upgrade_Objs = new GameObject[7];
	tk2dSprite[] upgrade_sprites = new tk2dSprite[7];	
	public GameObject closeButton_Obj;
	public GameObject noneButton_Obj;
	public GameObject previousButton_Obj;
	public GameObject nextButton_Obj;

	public enum StateBehavior { activeRoof = 0, activeAwning, activeTable, activeAccessories };
	public StateBehavior currentStateBehavior;
	int amountPages;
	int currentPage;
	
	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < upgrade_Objs.Length; i++) {
			upgrade_sprites[i] = upgrade_Objs[i].GetComponent<tk2dSprite>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public void InitializeDecorationObjects()
    {
        if(Mz_StorageManage.Roof_id == 255) {
            roofDecoration_Sprite.gameObject.active = false;
        }
        else {
            roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[Mz_StorageManage.Roof_id]);
        }
        
        if(Mz_StorageManage.Awning_id == 255) {
			awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName("DefaultShop_Awning");		
        }
        else {
            awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[Mz_StorageManage.Awning_id]);
        }

        if(Mz_StorageManage.Table_id == 255) {
            tableDecoration_Sprite.gameObject.active = false;
        }
        else {
            tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[Mz_StorageManage.Table_id]);
        }

        if(Mz_StorageManage.Accessory_id == 255) {
            accessories_Sprite.gameObject.active = false;
        }
        else {
            accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[Mz_StorageManage.Accessory_id]);
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
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (roofsNameSpecify [i]);
		}

		previousButton_Obj.active = false;
		nextButton_Obj.active = false;

		currentStateBehavior = StateBehavior.activeRoof;
		currentPage = 0;
	}

	public void ActiveAwning() {		
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (awningNameSpecify[i]);
		}
		
		previousButton_Obj.active = false;
		nextButton_Obj.active = false;

		currentStateBehavior = StateBehavior.activeAwning;
		currentPage = 0;
	}

	public void ActiveTable ()
	{
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (tablesNameSpecify[i]);
		}
		
		previousButton_Obj.active = true;
		nextButton_Obj.active = true;

		currentStateBehavior = StateBehavior.activeTable;
		currentPage = 0;
	}

	public void ActiveAccessories ()
	{
		for (int i = 0; i < 7; i++) {
			upgrade_sprites [i].spriteId = upgrade_sprites [0].GetSpriteIdByName (accessoriesNameSpeccify[i]);
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
				if (j < tablesNameSpecify.Length) {
					int spriteID = upgrade_sprites [i].GetSpriteIdByName (tablesNameSpecify [j]);
					upgrade_sprites [i].spriteId = spriteID;
				}
			}		
		} 
		else if (currentStateBehavior == StateBehavior.activeAccessories) {
			for (int i = 0; i < 7; i++) {
				int j = i + (7 * pageSpecify);
				if (j < accessoriesNameSpeccify.Length) {
					int spriteID = upgrade_sprites [i].GetSpriteIdByName (accessoriesNameSpeccify [j]);
					upgrade_sprites [i].spriteId = spriteID;
				}
			}	
		}
	}

    internal void BuyDecoration(string blockName)
    {
        if(currentStateBehavior == StateBehavior.activeRoof)
        {
            #region <-- Active roof.

            roofDecoration_Sprite.gameObject.active = true;

            switch (blockName)
            {
                case "Block_00": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[0]);
                    Mz_StorageManage.Roof_id = 0;
                    break;
                case "Block_01": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[1]);
                    Mz_StorageManage.Roof_id = 1;
                    break;
                case "Block_02": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[2]);
                    Mz_StorageManage.Roof_id = 2;
                    break;
                case "Block_03": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[3]);
                    Mz_StorageManage.Roof_id = 3;
                    break;
                case "Block_04": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[4]);
                    Mz_StorageManage.Roof_id = 4;
                    break;
                case "Block_05": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[5]);
                    Mz_StorageManage.Roof_id = 5;
                    break;
                case "Block_06": roofDecoration_Sprite.spriteId = roofDecoration_Sprite.GetSpriteIdByName(roofsNameSpecify[6]);
                    Mz_StorageManage.Roof_id = 6;
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
                case "Block_00": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[0]);
                    Mz_StorageManage.Awning_id = 0;
                    break;
                case "Block_01": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[1]);
                    Mz_StorageManage.Awning_id = 1;
                    break;
                case "Block_02": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[2]);
                    Mz_StorageManage.Awning_id = 2;
                    break;
                case "Block_03": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[3]);
                    Mz_StorageManage.Awning_id = 3;
                    break;
                case "Block_04": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[4]);
                    Mz_StorageManage.Awning_id = 4;
                    break;
                case "Block_05": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[5]);
                    Mz_StorageManage.Awning_id = 5;
                    break;
                case "Block_06": awningDecoration_Sprite.spriteId = awningDecoration_Sprite.GetSpriteIdByName(awningNameSpecify[6]);
                    Mz_StorageManage.Awning_id = 6;
                    break;
			default:
			break;
            }

            #endregion
        }
		else if(currentStateBehavior == StateBehavior.activeTable)
        {
            #region <!-- Active table.

            tableDecoration_Sprite.gameObject.active = true;

			switch (blockName) {				
            case "Block_00": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[0 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 0 + (7 * currentPage);
                break;
            case "Block_01": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[1 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 1 + (7 * currentPage);
                break;
            case "Block_02": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[2 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 2 + (7 * currentPage);
                break;
            case "Block_03": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[3 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 3 + (7 * currentPage);
                break;
            case "Block_04": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[4 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 4 + (7 * currentPage);
                break;
            case "Block_05": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[5 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 5 + (7 * currentPage);
                break;
            case "Block_06": tableDecoration_Sprite.spriteId = tableDecoration_Sprite.GetSpriteIdByName(tablesNameSpecify[6 + (7 *currentPage)]);
                    Mz_StorageManage.Table_id = 6 + (7 * currentPage);
                break;
            default:
                break;
            }

            #endregion
        }
		else if(currentStateBehavior == StateBehavior.activeAccessories)
        {
            #region <!-- Active accessory.

            accessories_Sprite.gameObject.active = true;

			switch (blockName) {				
            case "Block_00": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[0 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 0 + (7 * currentPage);
                break;
            case "Block_01": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[1 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 1 + (7 * currentPage);
                break;
            case "Block_02": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[2 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 2 + (7 * currentPage);
                break;
            case "Block_03": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[3 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 3 + (7 * currentPage);
                break;
            case "Block_04": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[4 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 4 + (7 * currentPage);
                break;
            case "Block_05": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[5 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 5 + (7 * currentPage);
                break;
            case "Block_06": accessories_Sprite.spriteId = accessories_Sprite.GetSpriteIdByName(accessoriesNameSpeccify[6 + (7 *currentPage)]);
                    Mz_StorageManage.Accessory_id = 6 + (7 * currentPage);
                break;
			default:
			break;
            }

            #endregion
        }
    }
}

