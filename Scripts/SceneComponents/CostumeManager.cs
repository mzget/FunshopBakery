using UnityEngine;
using System.Collections;

public class CostumeManager : MonoBehaviour {

    private string[] nameClothesSpecify = new string[] {
        "clothe_001", "clothe_002", "clothe_003", "clothe_004", "clothe_005", "clothe_006",
         "clothe_007", "clothe_008", "clothe_009", "clothe_010", "clothe_011", "clothe_012",
          "clothe_013", "clothe_014", "clothe_015", "none", "none", "none",
    };
	private string[] nameHatSpecifyIndex = new string[] {
		"Hat_0001", "Hat_0002", "Hat_0003", "Hat_0004", "Hat_0005", "Hat_0006",
		"Hat_0007", "Hat_0008", "Hat_0009", "Hat_0010", "Hat_0011", "Hat_0012", 
		"Hat_0013", "Hat_0014", "Hat_0015", "Hat_0016", "Hat_0017", "Hat_0018",  
		"Hat_0019", "Hat_0020", "Hat_0021", "Hat_0022", "Hat_0023", "none",
	};

    public GameObject[] low0_Obj;
    private tk2dSprite[] low0_Sprite = new tk2dSprite[3];
    public GameObject[] low1_Obj;
    private tk2dSprite[] low1_Sprite = new tk2dSprite[3];
    public tk2dSprite shirt_button;
    public tk2dSprite hat_button;
	public tk2dTextMesh displayCurrentPageID_textmesh;
    public CharacterCustomization characterCustomization;
    private Dressing sceneController;

    public enum TabMenuState { shirt = 0, hat, };
    public TabMenuState currentTabMenuState;
    private int maxPage = 0;
    private int currentPageIndex = 0;


    void Awake() {
        var main = GameObject.FindGameObjectWithTag("GameController");
        sceneController = main.GetComponent<Dressing>();
    }

	// Use this for initialization
	void Start () {
        //<!-- Initialize sprite data.
        for (int i = 0; i < low0_Obj.Length; i++)
        {
            low0_Sprite[i] = low0_Obj[i].GetComponent<tk2dSprite>();
        }

        for (int i = 0; i < low1_Obj.Length; i++)
        {
            low1_Sprite[i] = low1_Obj[i].GetComponent<tk2dSprite>();
        }

		this.ShowTab(TabMenuState.shirt);
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void GotoNextPage() {
        if (currentPageIndex < maxPage - 1) currentPageIndex += 1;
        else currentPageIndex = 0;
		

        CalculateObjectsToDisplay();
	}	
	public void BackToPreviousPage() {		
        if(currentPageIndex > 0)             
			currentPageIndex -= 1;
		else
			currentPageIndex = maxPage - 1;
		
		CalculateObjectsToDisplay();
	}

    internal void ShowTab(TabMenuState tabMenuState)
    {
        if (tabMenuState == TabMenuState.shirt) {
            maxPage = 3;
            currentTabMenuState = tabMenuState;
            shirt_button.spriteId = shirt_button.GetSpriteIdByName("shirt_button_down");
            hat_button.spriteId = hat_button.GetSpriteIdByName("hat_button_up");
        }
        else if (tabMenuState == TabMenuState.hat) {
            maxPage = 4;
            currentTabMenuState = tabMenuState;
            shirt_button.spriteId = shirt_button.GetSpriteIdByName("shirt_button_up");
            hat_button.spriteId = hat_button.GetSpriteIdByName("hat_button_down");
        }

        currentPageIndex = 0;
        CalculateObjectsToDisplay();
    }

    private void CalculateObjectsToDisplay()
    {
        if (currentTabMenuState == TabMenuState.shirt)
        {
            if (currentPageIndex == 0)
            {
                for (int i = 0; i < low0_Sprite.Length; i++)
                {
                    low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameClothesSpecify[i]);
                }
                for (int j = 0; j < low1_Sprite.Length; j++)
                {
                    low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameClothesSpecify[j + 3]);
                }
            }
            else if (currentPageIndex == 1)
            {
                for (int i = 0; i < low0_Sprite.Length; i++)
                {
                    low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameClothesSpecify[i + 6]);
                }
                for (int j = 0; j < low1_Sprite.Length; j++)
                {
                    low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameClothesSpecify[j + 9]);
                }
            }
            else if (currentPageIndex == 2)
            {
                for (int i = 0; i < low0_Sprite.Length; i++)
                {
                    low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameClothesSpecify[i + 12]);
                }
                for (int j = 0; j < low1_Sprite.Length; j++)
                {
                    low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameClothesSpecify[j + 15]);
                }
            }
        }
        else if (currentTabMenuState == TabMenuState.hat) {
            switch (currentPageIndex)
            {
                case 0:
                    for (int i = 0; i < low0_Sprite.Length; i++)
                    {
                        low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameHatSpecifyIndex[i]);
                    }
                    for (int j = 0; j < low1_Sprite.Length; j++)
                    {
                        low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameHatSpecifyIndex[j + 3]);
                    }
                    break;
                case 1:
                    for (int i = 0; i < low0_Sprite.Length; i++)
                    {
                        low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameHatSpecifyIndex[i + 6]);
                    }
                    for (int j = 0; j < low1_Sprite.Length; j++)
                    {
                        low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameHatSpecifyIndex[j + 9]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < low0_Sprite.Length; i++)
                    {
                        low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameHatSpecifyIndex[i + 12]);
                    }
                    for (int j = 0; j < low1_Sprite.Length; j++)
                    {
                        low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameHatSpecifyIndex[j + 15]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < low0_Sprite.Length; i++)
                    {
                        low0_Sprite[i].spriteId = low0_Sprite[i].GetSpriteIdByName(nameHatSpecifyIndex[i + 18]);
                    }
                    for (int j = 0; j < low1_Sprite.Length; j++)
                    {
                        low1_Sprite[j].spriteId = low1_Sprite[j].GetSpriteIdByName(nameHatSpecifyIndex[j + 21]);
                    }
                    break;
                default:
                    break;
            }
        }

		int temp_pageID = currentPageIndex + 1;
		displayCurrentPageID_textmesh.text = temp_pageID + "/" + maxPage;
		displayCurrentPageID_textmesh.Commit();
    }
    
    public void HaveChooseClotheCommand(string nameInput) {
        if (currentTabMenuState == TabMenuState.shirt)
        {
            #region <@-- Shirt.

            switch (nameInput)
            {
                case "Low0_1":
                    {
                        int index = 0 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    } 
                    break;
                case "Low0_2":
                    {
                        int index = 1 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    } 
                    break;
                case "Low0_3":
                    {
                        int index = 2 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    } 
                    break;
                case "Low1_1":
                    {
                        int index = 3 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    } 
                    break;
                case "Low1_2":
                    {
                        int index = 4 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    } 
                    break;
                case "Low1_3":
                    {
                        int index = 5 + (6 * currentPageIndex);
                        if (index < CharacterCustomization.AvailableClothesNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeClotheAtRuntime(index);
                    }
                    break;
                default:
                    break;
            }

            #endregion
        }
        else if (currentTabMenuState == TabMenuState.hat)
        {
            #region <@-- Hat section.

            switch (nameInput)
            {
                case "Low0_1":
                    {
                        int id = 0 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                case "Low0_2":
                    {
                        int id = 1 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                case "Low0_3":
                    {
                        int id = 2 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                case "Low1_1":
                    {
                        int id = 3 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                case "Low1_2":
                    {
                        int id = 4 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                case "Low1_3":
                    {
                        int id = 5 + (6 * currentPageIndex);
                        if (id < CharacterCustomization.AvailableHatNumber)
                            sceneController.PlayGreatEffect();
                        characterCustomization.ChangeHatAtRuntime(id);
                    } break;
                default:
                    break;
            }

            #endregion
        }
    }
}
