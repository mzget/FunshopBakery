using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


        public class ConservationAnimals {
            public static int Level = 3;
            public int[] DonationPrices = new int[5] { 
                500, 1000, 1500, 2000, 2500,
            }; 
        };
        public class EcoDonation {
            public static int Level = 0;
        };

	public class DonationManager : MonoBehaviour
	{
        private string[] arr_nameOfDonationTopic = new string[] {
            "ConservationAnimals_plate", "Eco_plate", "GlobalAIDFund_plate", "LoveDog_plate", "LoveKids_plate",
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

        ConservationAnimals conservationAnimal = new ConservationAnimals();
        public tk2dSprite topicIcon_0;
        public tk2dSprite topicIcon_1;
        public tk2dTextMesh topDonationPrice;
        public tk2dTextMesh downDonationPrice;
        public GameObject[] arr_topBarColor = new GameObject[5];
        public GameObject[] arr_downBarColor = new GameObject[5];

        const int MAX_PageNumber = 3;
        private int currentPageId = 0;

        void Start() {
            this.ResetDatafields();
            this.Initialize();
        }

        private void ResetDatafields()
        {
            currentPageId = 0;
			
			for (int i = 0; i < arr_topBarColor.Length; i++) {
				arr_topBarColor[i].active = false;
			}
            for (int i = 0; i < arr_downBarColor.Length; i++) {
                arr_downBarColor[i].active = false;
            }
        }

        private void Initialize()
        {
            topDonationPrice.text = conservationAnimal.DonationPrices[ConservationAnimals.Level].ToString();
            topDonationPrice.Commit();

            #region <@-- Active color bar button.

            switch (ConservationAnimals.Level)
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

            switch (EcoDonation.Level) {                     
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

            #endregion
        }

        Hashtable MoveToDonationTopic_hash = new Hashtable();
        public void PreviousDonationPage() {
            if (currentPageId > 0)
                currentPageId--;
            else
                currentPageId = MAX_PageNumber - 1;

            // Do something.
            ChangeTopicIcon();
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
                    break;
                case TOP_ORANGE:
                    if (currentPageId == 1) { }
                    break;
                default:
                    break;
            }
        }
    }
