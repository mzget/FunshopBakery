using UnityEngine;
using System.Collections;

public class Startup : Mz_BaseScene {
	
	
	// Use this for initialization
	void Start () {
		//<!-- Set startup quality to "fast", --> full resolution texture.
		QualitySettings.SetQualityLevel(1);
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		
        if(Application.isLoadingLevel == false) {
            Application.LoadLevelAsync(Mz_BaseScene.SceneNames.WaitForStart.ToString());
        }
	}
}
