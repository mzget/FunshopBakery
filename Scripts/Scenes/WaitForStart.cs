using UnityEngine;
using System.Collections;

public class WaitForStart : Mz_BaseScene {

    public GameObject background_Obj;
	
//	public GameObject tabToStart_obj;

	// Use this for initialization
	void Start () {
        Mz_ResizeScale.ResizingScale(background_Obj.transform);
	}
	
	// Update is called once per frame
	protected override void Update () {
		
		base.Update();
		
		if(Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
           if(Application.isLoadingLevel == false) {
               Mz_LoadingScreen.LoadSceneName = Mz_BaseScene.SceneNames.MainMenu.ToString();
               Application.LoadLevelAsync(Mz_BaseScene.SceneNames.LoadingScene.ToString());
           }
       }
	}
}
