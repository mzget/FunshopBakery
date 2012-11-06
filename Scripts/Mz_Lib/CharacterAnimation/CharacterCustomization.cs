using UnityEngine;
using System.Collections;

public class CharacterCustomization : MonoBehaviour {

    public string[] arr_clothesNameSpec = new string[] {
        "Clothe_0001", "Clothe_0002", "Clothe_0003", 
         "Clothe_0004", "Clothe_0005", "Clothe_0006",
          "Clothe_0007", "Clothe_0008", "Clothe_0009",
           "Clothe_0010", "Clothe_0011", "Clothe_0012",
            "Clothe_0013", "Clothe_0014", "Clothe_0015",
    };
    public string[] arrHatNameSpec = new string[] {
		"Hat_0001", "Hat_0002", "Hat_0003", 
        "Hat_0004", "Hat_0005", "Hat_0006", 
        "Hat_0007", "Hat_0008", "Hat_0009", 
        "Hat_0010", "Hat_0011", "Hat_0012", 
        "Hat_0013", "Hat_0014", "Hat_0015", 
        "Hat_0016", "Hat_0017", "Hat_0018", 
        "Hat_0019", "Hat_0020", "Hat_0021", 
        "Hat_0022", "Hat_0023", "Hat_0024", 
    };

    public tk2dSprite TK_clothe;
    public tk2dSprite TK_hat;
	public tk2dSprite TK_hair;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeClotheAtRuntime(int arr_index) {
        TK_clothe.spriteId = TK_clothe.GetSpriteIdByName(arr_clothesNameSpec[arr_index]);
    }

    public void ChangeHatAtRuntime(int arr_index) {
        TK_hat.spriteId = TK_hat.GetSpriteIdByName(arrHatNameSpec[arr_index]);
		
		if(arr_index <= 10) {
			TK_hat.transform.localPosition = new Vector3(0, -.095f, -.4f);
			if(arr_index == 8 || arr_index == 9 || arr_index == 10) {
				TK_hair.gameObject.active = false;	
			}
			else {
				TK_hair.gameObject.active = true;
			}
		}
		else if(arr_index > 10) {
			TK_hat.transform.localPosition = new Vector3(0.01f, -0.04f, -0.4f);
			
			if(arr_index == 11) {
				TK_hair.gameObject.active = false;
			}
			else{
				TK_hair.gameObject.active = true;
			}
		}
    }
}
