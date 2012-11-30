using UnityEngine;
using System.Collections;

public class Startup : Mz_BaseScene {
	
	
	// Use this for initialization
	void Start () {		
		//<!-- get name quality.
//		qualities_list = QualitySettings.names;
		StartCoroutine(this.AutomaticSetup_QualitySetting());
	}
	
	private IEnumerator AutomaticSetup_QualitySetting() {
#if UNITY_IPHONE
		if(iPhone.generation == iPhoneGeneration.iPad1Gen ||
			iPhone.generation == iPhoneGeneration.iPhone3G || iPhone.generation == iPhoneGeneration.iPhone3GS) {
			QualitySettings.SetQualityLevel(0);	
		    Application.targetFrameRate = 30;
		}
		else {
			QualitySettings.SetQualityLevel(3);
		    Application.targetFrameRate = 60;
		}
#elif UNITY_ANDROID
        QualitySettings.SetQualityLevel(1);
		Application.targetFrameRate = 60;
#else 
		QualitySettings.SetQualityLevel(3);
		Application.targetFrameRate = 60;
#endif

		yield return null;
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
