using UnityEngine;
using System.Collections;

public class WaitForStart : Mz_BaseScene {

    public GameObject background_Obj;
	public GameObject[] cloudAndFog_Objs = new GameObject[4];

	// Use this for initialization
	void Start () {
        Mz_ResizeScale.ResizingScale(background_Obj.transform);
		
		iTween.MoveTo(cloudAndFog_Objs[0].gameObject, iTween.Hash("x", 0.3f, "time", 10f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[1].gameObject, iTween.Hash("x", 0.3f, "time", 11f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[2].gameObject, iTween.Hash("x", 0.3f, "time", 12f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong)); 
		iTween.MoveTo(cloudAndFog_Objs[3].gameObject, iTween.Hash("x", 0.3f, "time", 13f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong)); 
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
