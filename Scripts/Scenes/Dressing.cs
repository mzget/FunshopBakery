using UnityEngine;
using System.Collections;

public class Dressing : Mz_BaseScene {

    public Transform background_transform;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];
	public GameObject back_button_Obj;
	public CharacterAnimationManager TK_animationManager;
    public CostumeManager costomeManager;

    public Transform gameEffect_transform;
	
	
	// Use this for initialization
	void Start () {
        StartCoroutine(InitializeAudio());

        this.gameObject.AddComponent<GameEffectManager>();
        effectManager = this.gameObject.GetComponent<GameEffectManager>();
		
        Mz_ResizeScale.ResizingScale(background_transform);
		
		iTween.MoveTo(cloudAndFog_Objs[0].gameObject, iTween.Hash("y", 0.2f, "time", 2f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[1].gameObject, iTween.Hash("y", 0.4f, "time", 3f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[2].gameObject, iTween.Hash("y", 0.6f, "time", 4f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", .3f, "time", 5f, "easetype", iTween.EaseType.easeInSine, "looptype", iTween.LoopType.pingPong)); 
	}

    protected new IEnumerator InitializeAudio()
    {
        base.InitializeAudio();
        
        audioBackground_Obj.audio.clip = base.background_clip;
        audioBackground_Obj.audio.loop = true;
        audioBackground_Obj.audio.Play();
		 
		yield return null; 
    }

	public override void OnInput (string nameInput)
	{
		base.OnInput (nameInput);

        switch (nameInput)
        {
            case "shirt_button":
                TK_animationManager.PlayTalkingAnimation();
                costomeManager.ShowTab(CostumeManager.TabMenuState.shirt);
                break;
            case "hat_button":
                TK_animationManager.PlayTalkingAnimation();
                costomeManager.ShowTab(CostumeManager.TabMenuState.hat);
                break;
            case "Previous_button":
                costomeManager.BackToPreviousPage();
                break;
            case "Next_button":
                costomeManager.GotoNextPage();
                break;
            case "Low0_1": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
                break;
            case "Low0_2": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
                break;
            case "Low0_3": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
                break;
            case "Low1_1": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
                break;
            case "Low1_2": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
                break;
            case "Low1_3": costomeManager.HaveChooseClotheCommand(nameInput);
                TK_animationManager.PlayGoodAnimation();
                effectManager.Create2DSpriteAnimationEffect("Iridescent", gameEffect_transform);
                audioEffect.PlayOnecWithOutStop(audioEffect.longBring_clip);
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
}
