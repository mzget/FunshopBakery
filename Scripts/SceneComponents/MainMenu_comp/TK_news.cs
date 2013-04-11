using UnityEngine;
using System.Collections;

public class TK_news : MonoBehaviour {
	private const int AMOUNT_OF_NEWS_TAG = 3;
    private readonly int AmountOfPage = 2; 
    private int currentPage = 0;

	
	public GameObject facebook_button;
	public tk2dSprite[] news_tags = new tk2dSprite[AMOUNT_OF_NEWS_TAG];
	private readonly string[] arr_NameOfNewsSprite = new string[6] {
		"Bakery Shop", "Sushi Shop", "Icecream Shop",
        "Pizza Shop", "None", "None",
	};
	
	public const string TK_NEWS_BUTTON_NAME = "TK_news_button";
	public const string FACEBOOK_LIKE_BUTTON_NAME = "FacebookLike_button";
	public const string FACEBOOK_FANPAGE_URL = "https://www.facebook.com/Taokaenoi.game";
	public const string ITUNES_STORE_LINK = "http://itunes.apple.com/app/id";
	public const string ITUNES_STORE_USER_REVIEW_LINK = "http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=APP_ID";
	public const string TK_BAKERY_SHOP_APP_ID = "626645567";
	public static void GotoiTunes_ViewSoftware() {
		Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewSoftware?id=626645567&mt=8");
	}

	public static void GoToiTunes_UserReview() {
		Application.OpenURL("http://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=626645567");
	}

	// Use this for initialization
    void Start()
    {
        currentPage = 0;
        this.SynchronizeNewsTag();
	}

    private void SynchronizeNewsTag()
    {
        for (int i = 0; i < AMOUNT_OF_NEWS_TAG; i++)
        {
            news_tags[i].spriteId = news_tags[i].GetSpriteIdByName(arr_NameOfNewsSprite[i + (currentPage * AMOUNT_OF_NEWS_TAG)]);
			news_tags[i].gameObject.name = arr_NameOfNewsSprite[i + (currentPage * AMOUNT_OF_NEWS_TAG)];
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    internal void ShakeFacebookButton() {
        iTween.ShakePosition(facebook_button.gameObject, iTween.Hash("name", "ShakePosFacebook","amount", new Vector3(0.01f, 0.01f, 0f), "islocal", false, "time", 1f, "looptype", iTween.LoopType.pingPong));
    }

    internal void StopShakeFacebookButton() {
        iTween.StopByName(facebook_button.gameObject, "ShakePosFacebook");
    }

    internal void MoveUpPage()
    {
        if (currentPage > 0)
            currentPage--;
        else
            currentPage = AmountOfPage - 1;

        SynchronizeNewsTag();
    }

    internal void MoveDownPage()
    {
        if (currentPage < AmountOfPage-1)
            currentPage++;
        else
            currentPage = 0;

        SynchronizeNewsTag();
    }
}
