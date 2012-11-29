using UnityEngine;
using System.Collections;

public class DisplayRewards_Scene : Mz_BaseScene {
	
	const string BACK_BUTTON_NAME = "Back_button";
	const string NEXT_BUTTON_NAME = "Next_button";
	const string PREVIOUS_BUTTON_NAME = "Previous_button";

	public RewardManager rewardManager;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(this.InitializeAudio());
	}
	
	private new IEnumerator InitializeAudio() {
		base.InitializeAudio();		
		
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();

        yield return null;
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
	
	public override void OnInput (string nameInput)
	{
//		base.OnInput (nameInput);
		
		switch (nameInput) {
		case BACK_BUTTON_NAME : if(!Application.isLoadingLevel) {
				Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString();
				Application.LoadLevel(Mz_BaseScene.SceneNames.LoadingScene.ToString());
			}
			break;
		case NEXT_BUTTON_NAME:rewardManager.HaveNextPageCommand();
			break;
		case PREVIOUS_BUTTON_NAME : rewardManager.HavePreviousCommand();
			break;
		default:
		break;
		}
	}
}
