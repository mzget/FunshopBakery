using UnityEngine;
using System.Collections;

public class BinBeh : ObjectsBeh {

    const string OpenAnim = "open";
    const string CloseAnim = "close";

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
		
		base.animationName_001 = OpenAnim;
		base.animationName_002 = CloseAnim;
    }
	
	public void PlayOpenAnimation() {
        animatedSprite.Play(animationName_001);	
		animatedSprite.animationCompleteDelegate = animationCompleteDelegate;
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}

	#region <!-- On Mouse Events.

	protected override void OnTouchBegan ()
	{
		base.OnTouchBegan ();
	}
	protected override void OnTouchDown ()
	{
		// <!--- On object active.
		if(animation) {
			this.animation.Play();
		}
		else if(animatedSprite && animationName_001 != string.Empty) {
			PlayOpenAnimation();
		}
		
		baseScene.audioEffect.PlayOnecSound(baseScene.audioEffect.buttonDown_Clip);

		base.OnTouchDown ();
	}
	protected override void OnTouchEnded ()
	{
		base.OnTouchEnded ();
	}

	#endregion
}
