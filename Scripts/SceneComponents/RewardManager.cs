using UnityEngine;
using System.Collections;

public class RewardManager : MonoBehaviour
{
	public tk2dSprite titleIcon_0;
	public tk2dSprite titleIcon_1;
	public tk2dSprite titleIcon_2;
    public tk2dAnimatedSprite[] spriteEffects = new tk2dAnimatedSprite[3];
	
	private const int MAX_PAGENUMBER = 2;
	private int currentPageID = 0;
	private string[] arr_nameOfPlates = new string[5] {
		"ConservationAnimals_plate", "GlobalAIDFund_plate", "LoveDog_plate", "LoveKids_plate", "Eco_plate",
	};
	
	
	// Use this for initialization
	private IEnumerator Start ()
	{		
		yield return new WaitForEndOfFrame();

		currentPageID = 0;

        foreach (tk2dAnimatedSprite item in spriteEffects)
        {
            item.CurrentClip.wrapMode = tk2dSpriteAnimationClip.WrapMode.Loop;
        }
	}
	
	// Update is called once per frame
//	void Update ()
//	{
//	
//	}
	
//	internal void GetInput(string targetName) {
//		switch (targetName) {
//		default:
//		break;
//		}
//	}
	
	internal void HaveNextPageCommand() {
		if(currentPageID < MAX_PAGENUMBER - 1)
			currentPageID++;
		else 
			currentPageID = 0;

		this.ChangePageProcessing();
	}
	
	internal void HavePreviousCommand() {
		if(currentPageID > 0)
			currentPageID--;
		else
			currentPageID = MAX_PAGENUMBER - 1;
		
		this.ChangePageProcessing();
	}

	void ChangePageProcessing ()
	{
		if (currentPageID == 0) {
				titleIcon_0.spriteId = titleIcon_0.GetSpriteIdByName (arr_nameOfPlates [0]);
				titleIcon_1.spriteId = titleIcon_1.GetSpriteIdByName (arr_nameOfPlates [1]);
				titleIcon_2.spriteId = titleIcon_2.GetSpriteIdByName (arr_nameOfPlates [2]);
		} else if (currentPageID == 1) {
				titleIcon_0.spriteId = titleIcon_0.GetSpriteIdByName (arr_nameOfPlates [3]);
				titleIcon_1.spriteId = titleIcon_1.GetSpriteIdByName (arr_nameOfPlates [4]);
//			titleIcon_2.spriteId = titleIcon_2.GetSpriteIdByName (arr_nameOfPlates [2]);
		}
	}
}

