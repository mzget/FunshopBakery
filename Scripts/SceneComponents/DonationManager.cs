using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class ConservationAnimals {
    public static int Level = 0;
    public int[] DonationPrices = new int[5] { 
        500, 1000, 1500, 2000, 2500,
    }; 
};
public class EcoFoundation {
    public static int Level = 0;
    public int[] donationPrice = new int[5] {
        500, 1000, 1500, 2000, 2500,
    };
};
public class AIDSFoundation {
    public static int Level = 0;
    public int[] donationPrice = new int[5] {
        500, 1000, 1500, 2000, 2500,
    };
};
public class LoveDogConsortium { 
    public static int Level = 0;
    public int[] donationPrice = new int[5] {
        500, 1000, 1500, 2000, 2500,
    };
};
public class LoveKidsFoundation { 
    public static int Level = 0;
    public int[] donationPrice = new int[5] {
        500, 1000, 1500, 2000, 2500,
    };
};

public class DonationManager : MonoBehaviour
{
    private string[] arr_nameOfDonationTopic = new string[] {
        "ConservationAnimals_plate", "GlobalAIDFund_plate", "LoveDog_plate", "LoveKids_plate", "Eco_plate",
    };
    public const string TOP_RED = "Top_red";
    public const string TOP_ORANGE = "Top_orange";
    public const string TOP_YELLOW = "Top_yellow";
    public const string TOP_LIGHTGREEN = "Top_lightGreen";
    public const string TOP_DARKGREEN = "Top_darkGreen";
    public const string DOWN_RED = "Down_red";
    public const string DOWN_ORANGE = "Down_orange";
    public const string DOWN_YELLOW = "Down_yellow";
    public const string DOWN_LIGHTGREEN = "Down_lightGreen";
    public const string DOWN_DARKGREEN = "Down_darkGreen";
    public const string TOP_DONATEBUTTONNAME = "TopDonate_button";
    public const string DOWN_DONATEBUTTONNAME = "DownDonate_button";

    Mz_BaseScene sceneController;
    ConservationAnimals conservationAnimal = new ConservationAnimals();
    EcoFoundation ecoDonation = new EcoFoundation();
    AIDSFoundation aidsFoundation = new AIDSFoundation();
    LoveDogConsortium loveDogFound = new LoveDogConsortium();
    LoveKidsFoundation loveKidsFound = new LoveKidsFoundation();
    public tk2dSprite topicIcon_0;
    public tk2dSprite topicIcon_1;
    public tk2dTextMesh topDonationPrice;
    public tk2dTextMesh downDonationPrice;
    public GameObject[] arr_topBarColor = new GameObject[5];
    public GameObject[] arr_downBarColor = new GameObject[5];
    public Transform topDonateButtonTransform;
    public Transform downDonateButton_Transform;

    const int MAX_PageNumber = 3;
    private int currentPageId = 0;
    Hashtable MoveToDonationTopic_hash = new Hashtable();
	
	
	
	void Awake() {		
		print("DonationManager.Awake");
		
        sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Mz_BaseScene>();
	}
	
    void Start() {
		print("DonationManager.Start");
		
        currentPageId = 0;
        this.ResetDatafields();
		this.ChangeTopicIcon();
    }

    public void ReInitializeData() {         
        this.ResetDatafields();
        this.ReActiveColorBarPicker();
    }

    private void ResetDatafields()
    {			
		for (int i = 0; i < arr_topBarColor.Length; i++) {
			arr_topBarColor[i].active = false;
		}
        for (int i = 0; i < arr_downBarColor.Length; i++) {
            arr_downBarColor[i].active = false;
        }
    }

    private void ReActiveColorBarPicker()
    {
        //<@-- Active color bar button.

        int temp_topLv = 0;
        int temp_downLv = 0;
        if (currentPageId == 0) {
            temp_topLv = ConservationAnimals.Level;
            temp_downLv = AIDSFoundation.Level;

	        topDonationPrice.text = conservationAnimal.DonationPrices[ConservationAnimals.Level].ToString();
	        topDonationPrice.Commit();
	        downDonationPrice.text = aidsFoundation.donationPrice[AIDSFoundation.Level].ToString();
	        downDonationPrice.Commit();
        }
        else if (currentPageId == 1) {
            temp_topLv = LoveDogConsortium.Level;
            temp_downLv = LoveKidsFoundation.Level;
			
	        topDonationPrice.text = loveDogFound.donationPrice[LoveDogConsortium.Level].ToString();
	        topDonationPrice.Commit();
	        downDonationPrice.text = loveKidsFound.donationPrice[LoveKidsFoundation.Level].ToString();
	        downDonationPrice.Commit();
        }
        else if (currentPageId == 2) {
            temp_topLv = EcoFoundation.Level;
			
	        topDonationPrice.text = ecoDonation.donationPrice[EcoFoundation.Level].ToString();
	        topDonationPrice.Commit();
//	            downDonationPrice.text = AIDSFoundation.donationPrice[AIDSFoundation.Level].ToString();
//	            downDonationPrice.Commit();
        }

        switch (temp_topLv)
            {
                case 0:
                    arr_topBarColor[0].active = true;
                    break;
                case 1:
                    arr_topBarColor[0].active = true;
                    arr_topBarColor[1].active = true;
                    break;
                case 2:
                    for (int i = 0; i <= 2; i++)
                        arr_topBarColor[i].active = true;
                    break;
                case 3:
                    for (int i = 0; i <= 3; i++)
                        arr_topBarColor[i].active = true;
                    break;
                case 4:
                    for (int i = 0; i <= 4; i++)
                        arr_topBarColor[i].active = true;
                    break;
                default:
                    break;
            }

            switch (temp_downLv)
            {
                case 0:
                    arr_downBarColor[0].active = true;
                    break;
                case 1:
                    arr_downBarColor[0].active = true;
                    arr_downBarColor[1].active = true;
                    break;
                case 2:
                    for (int i = 0; i <= 2; i++)
                        arr_downBarColor[i].active = true;
                    break;
                case 3:
                    for (int i = 0; i <= 3; i++)
                        arr_downBarColor[i].active = true;
                    break;
                case 4:
                    for (int i = 0; i <= 4; i++)
                        arr_downBarColor[i].active = true;
                    break;
                default:
                    break;
            }
    }

    public void PreviousDonationPage() {
        if (currentPageId > 0)
            currentPageId--;
        else
            currentPageId = MAX_PageNumber - 1;

        // Do something.
        this.ChangeTopicIcon();
    }
    public void NextDonationPage() {
        if (currentPageId < MAX_PageNumber - 1)
            currentPageId++;
        else
            currentPageId = 0;

        // Do something.
        this.ChangeTopicIcon();
    }

    private void ChangeTopicIcon()
    {
        if (currentPageId == 0) {
            topicIcon_0.spriteId = topicIcon_0.GetSpriteIdByName(arr_nameOfDonationTopic[0]);
            topicIcon_1.spriteId = topicIcon_1.GetSpriteIdByName(arr_nameOfDonationTopic[1]);
        }
        else if (currentPageId == 1) {
            topicIcon_0.spriteId = topicIcon_0.GetSpriteIdByName(arr_nameOfDonationTopic[2]);
            topicIcon_1.spriteId = topicIcon_1.GetSpriteIdByName(arr_nameOfDonationTopic[3]);
        }
        else if (currentPageId == 2) {
            topicIcon_0.spriteId = topicIcon_0.GetSpriteIdByName(arr_nameOfDonationTopic[4]);
            //topicIcon_1.spriteId = topicIcon_1.GetSpriteIdByName(arr_nameOfDonationTopic[3]);
        }

        this.ReInitializeData();
        print("DonationManager.ChangeTopicIcon");
    }

    internal void GetInput(string inputName)
    {
        switch (inputName)
        {
            case TOP_RED:
                if (currentPageId == 0) {
                    topDonationPrice.text = conservationAnimal.DonationPrices[0].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    topDonationPrice.text = loveDogFound.donationPrice[0].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    topDonationPrice.text = ecoDonation.donationPrice[0].ToString();
                    topDonationPrice.Commit();
                }
                break;
            case TOP_ORANGE:
                if (currentPageId == 0) {
                    topDonationPrice.text = conservationAnimal.DonationPrices[1].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    topDonationPrice.text = loveDogFound.donationPrice[1].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    topDonationPrice.text = ecoDonation.donationPrice[1].ToString();
                    topDonationPrice.Commit();
                }
                break;
            case TOP_YELLOW:
                if (currentPageId == 0) {
                    topDonationPrice.text = conservationAnimal.DonationPrices[2].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    topDonationPrice.text = loveDogFound.donationPrice[2].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    topDonationPrice.text = ecoDonation.donationPrice[2].ToString();
                    topDonationPrice.Commit();
                }
                break;
            case TOP_LIGHTGREEN:
                if (currentPageId == 0) {
                    topDonationPrice.text = conservationAnimal.DonationPrices[3].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    topDonationPrice.text = loveDogFound.donationPrice[3].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    topDonationPrice.text = ecoDonation.donationPrice[3].ToString();
                    topDonationPrice.Commit();
                }
                break;
            case TOP_DARKGREEN:
                if (currentPageId == 0) {
                    topDonationPrice.text = conservationAnimal.DonationPrices[4].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    topDonationPrice.text = loveDogFound.donationPrice[4].ToString();
                    topDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    topDonationPrice.text = ecoDonation.donationPrice[4].ToString();
                    topDonationPrice.Commit();
                }
                break;
            case DOWN_RED:
                if (currentPageId == 0) {
                    downDonationPrice.text = aidsFoundation.donationPrice[0].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    downDonationPrice.text = loveKidsFound.donationPrice[0].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    print("Not Implement !");
                }
                break;
            case DOWN_ORANGE:
                if (currentPageId == 0) {
                    downDonationPrice.text = aidsFoundation.donationPrice[1].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    downDonationPrice.text = loveKidsFound.donationPrice[1].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    print("Not Implement !");
                }
                break;
            case DOWN_YELLOW:
                if (currentPageId == 0) {
                    downDonationPrice.text = aidsFoundation.donationPrice[2].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    downDonationPrice.text = loveKidsFound.donationPrice[2].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    print("Not Implement !");
                }
                break;
            case DOWN_LIGHTGREEN:
                if (currentPageId == 0) {
                    downDonationPrice.text = aidsFoundation.donationPrice[3].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    downDonationPrice.text = loveKidsFound.donationPrice[3].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    print("Not Implement !");
                }
                break;
            case DOWN_DARKGREEN:
                if (currentPageId == 0) {
                    downDonationPrice.text = aidsFoundation.donationPrice[4].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 1) {
                    downDonationPrice.text = loveKidsFound.donationPrice[4].ToString();
                    downDonationPrice.Commit();
                }
                else if (currentPageId == 2) {
                    print("Not Implement !");
                }
                break;
            case TOP_DONATEBUTTONNAME:
                if (currentPageId == 0)
                    this.DonationProcessing(arr_nameOfDonationTopic[0]);
                else if (currentPageId == 1)
                    this.DonationProcessing(arr_nameOfDonationTopic[2]);
                else if (currentPageId == 2)
                    this.DonationProcessing(arr_nameOfDonationTopic[4]);
                break;
            case DOWN_DONATEBUTTONNAME:
                if (currentPageId == 0)
                    this.DonationProcessing(arr_nameOfDonationTopic[1]);
                else if (currentPageId == 1)
                    this.DonationProcessing(arr_nameOfDonationTopic[3]);
                //else if (currentPageId == 2)
                //    this.DonationProcessing();
                break;
            default:
                break;
        }
    }

    private void DonationProcessing(string targetDonation)
    {
        int currentPriceToDonate = 0;
        if (targetDonation == arr_nameOfDonationTopic[0])
        {
            currentPriceToDonate = conservationAnimal.DonationPrices[ConservationAnimals.Level];
            if (currentPriceToDonate <= Mz_StorageManage.AccountBalance)
            {
                //@! Can donation.
                Mz_StorageManage.AccountBalance -= currentPriceToDonate;
                sceneController.effectManager.Create2DSpriteAnimationEffect("BloomStar", topDonateButtonTransform);
                sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);

                print("DonationProcessing... complete !");
            }
            else
            {
                print("Cannot donation !, Your account balance are less than requirement.");
            }
        }
        else if (targetDonation == arr_nameOfDonationTopic[1])
        {
            currentPriceToDonate = aidsFoundation.donationPrice[AIDSFoundation.Level];
            if (currentPriceToDonate <= Mz_StorageManage.AccountBalance)
            {
                Mz_StorageManage.AccountBalance -= currentPriceToDonate;
                sceneController.effectManager.Create2DSpriteAnimationEffect("BloomStar", downDonateButton_Transform);
                sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);

                print("DonationProcessing... complete !");
            }
            else
            {
                print("Cannot donation !, Your account balance are less than requirement.");
            }
        }
        else if (targetDonation == arr_nameOfDonationTopic[2])
        {
            currentPriceToDonate = loveDogFound.donationPrice[LoveDogConsortium.Level];
            if (currentPriceToDonate <= Mz_StorageManage.AccountBalance)
            {
                Mz_StorageManage.AccountBalance -= currentPriceToDonate;
                sceneController.effectManager.Create2DSpriteAnimationEffect("BloomStar", topDonateButtonTransform);
                sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);

                print("DonationProcessing... complete !");
            }
            else
            {
                print("Cannot donation !, Your account balance are less than requirement.");
            }
        }
        else if (targetDonation == arr_nameOfDonationTopic[3]) {
            currentPriceToDonate = loveKidsFound.donationPrice[LoveKidsFoundation.Level];
            if (currentPriceToDonate <= Mz_StorageManage.AccountBalance)
            {
                Mz_StorageManage.AccountBalance -= currentPriceToDonate;
                sceneController.effectManager.Create2DSpriteAnimationEffect("BloomStar", downDonateButton_Transform);
                sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);

                print("DonationProcessing... complete !");
            }
            else
            {
                print("Cannot donation !, Your account balance are less than requirement.");
            }
        }
        else if (targetDonation == arr_nameOfDonationTopic[4])
        {
            currentPriceToDonate = ecoDonation.donationPrice[LoveDogConsortium.Level];
            if (currentPriceToDonate <= Mz_StorageManage.AccountBalance)
            {
                Mz_StorageManage.AccountBalance -= currentPriceToDonate;
                sceneController.effectManager.Create2DSpriteAnimationEffect("BloomStar", topDonateButtonTransform);
                sceneController.audioEffect.PlayOnecWithOutStop(sceneController.audioEffect.longBring_clip);

                print("DonationProcessing... complete !");
            }
            else
            {
                print("Cannot donation !, Your account balance are less than requirement.");
            }
        }
    }
}
