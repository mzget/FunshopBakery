using UnityEngine;
using System.Collections;

public class Dressing : Mz_BaseScene {

    public Transform background_transform;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];
	public GameObject back_button_Obj;
	public CharacterAnimationManager TK_animationManager;
    public CostumeManager costomeManager;
	
	
	
	
	// Use this for initialization
	void Start () {
        base.InitializeAudio();
        Mz_ResizeScale.ResizingScale(background_transform);

		TK_animationManager.PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList.idle);
		
		iTween.MoveTo(cloudAndFog_Objs[0].gameObject, iTween.Hash("y", 0.2f, "time", 2f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[1].gameObject, iTween.Hash("y", 0.4f, "time", 3f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[2].gameObject, iTween.Hash("y", 0.6f, "time", 4f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", .3f, "time", 5f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
	}

	public override void OnInput (string nameInput)
	{
		base.OnInput (nameInput);

        switch (nameInput)
        {
            case "shirt_button":
                TK_animationManager.PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList.talk);
                TK_animationManager.PlayLeftHandAnimation(CharacterAnimationManager.NameAnimationsList.lefthand_active);
                costomeManager.ShowTab(CostumeManager.TabMenuState.shirt);
                break;
            case "hat_button": 
                TK_animationManager.PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList.talk);
                TK_animationManager.PlayLeftHandAnimation(CharacterAnimationManager.NameAnimationsList.lefthand_active);
                costomeManager.ShowTab(CostumeManager.TabMenuState.hat);
                break;
            case "Previous_button":
                TK_animationManager.PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList.good1);
                TK_animationManager.PlayLeftHandAnimation(CharacterAnimationManager.NameAnimationsList.lefthand_good1);
			costomeManager.BackToPreviousPage();
                break;
            case "Next_button":
                TK_animationManager.PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList.good1);
                TK_animationManager.PlayLeftHandAnimation(CharacterAnimationManager.NameAnimationsList.lefthand_good1);
			costomeManager.GotoNextPage();
                break;
            case "Low0_1": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            case "Low0_2": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            case "Low0_3": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            case "Low1_1": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            case "Low1_2": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            case "Low1_3": costomeManager.HaveChooseClotheCommand(nameInput);
                break;
            default:
                break;
        }

		if(nameInput == back_button_Obj.name) {
			if(Application.isLoadingLevel == false) {
				Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.Town.ToString();
				Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
			}
		}

	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
}
