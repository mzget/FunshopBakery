using UnityEngine;
using System.Collections;

public class JamBeh : ObjectsBeh {
	
	public const string StrawberryJam = "StrawberryJam";
	public const string BlueberryJam = "BlueberryJam";
	public const string ButterJam = "ButterJam";
	public const string CustardJam = "CustardJam";
	

	void Awake() {
		iTween.Init(this.gameObject);
	}

	// Use this for initialization
	protected override void  Start()
    {
        base.Start();
		
		sceneManager = base.baseScene.GetComponent<BakeryShop>();
		
		if(this.gameObject.name == StrawberryJam) {
			base.animationName_001 = "strawberry_open";
			base.animationName_002 = "strawberry_active";
		}
		else if(this.gameObject.name == BlueberryJam) {
			base.animationName_001 = "blueberry_open";
			base.animationName_002 = "blueberry_active";
		}
		else if(this.gameObject.name == ButterJam) {
			base.animationName_001 = "freshButter_open";
			base.animationName_002 = "freshButter_active";
		}
		else if(this.gameObject.name == CustardJam) {
			base.animationName_001 = "custard_open";
			base.animationName_002 = "custard_active";
		}
    }

    #region <!-- OnInput Events.

	protected override void OnTouchDown()
    {
		sceneManager.SetAnimatedJamInstance(false);

        if(base.animationName_001 != "") {
            base.animatedSprite.Play(base.animationName_001);
            base.animatedSprite.animationCompleteDelegate = AnimationComplete;
        }
		
		for (int i = 0; i < sceneManager.toasts.Length; i++) {
			sceneManager.toasts[i].WaitForIngredient(this.gameObject.name);
		}

        base.OnTouchDown();
    }

    #endregion

    public void AnimationComplete(tk2dAnimatedSprite sprite, int clipId) {
        animatedSprite.StopAndResetFrame();

        base.animatedSprite.animationCompleteDelegate -= AnimationComplete;
    }

    internal void PlayActiveAnimation()
    {
        animatedSprite.Play(animationName_002);
    }
}
