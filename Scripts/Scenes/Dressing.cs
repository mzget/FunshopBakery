using UnityEngine;
using System.Collections;

public class Dressing : Mz_BaseScene {
	
	public GameObject back_button_Obj;
	
	
	
	
	
	// Use this for initialization
	void Start () {
        base.InitializeAudio();
	}
	
	public override void OnPointerOverName (string nameInput)
	{
		base.OnPointerOverName (nameInput);
	}
	public override void OnInput (string nameInput)
	{
		base.OnInput (nameInput);
		
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
