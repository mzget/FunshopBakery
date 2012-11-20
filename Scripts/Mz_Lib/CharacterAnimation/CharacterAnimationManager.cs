using UnityEngine;
using System.Collections;

public class CharacterAnimationManager : MonoBehaviour {

    public tk2dAnimatedSprite hair_anim;
	public tk2dAnimatedSprite eye_anim;
	public tk2dAnimatedSprite lefthand_anim;
	public tk2dAnimatedSprite righthand_anim;
	
	public enum NameAnimationsList {
		idle = 0,
		talk = 1,
		good1,
		good2,
		good3,
		sad1,
		sad2,
		sad3,
        agape = 8,
        agape2 = 9,

        //<!-- Left hand --->
        lefthand_active,
        lefthand_good1,
        //<!-- right hand.
        righthand,
        //<!-- Hair animations list.
        hair,
	};

    double timer;

	
	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 2) {
            timer = 0;

            PlayEyeAnimation(NameAnimationsList.idle);
        }
	}

	public void PlayAnimationByName(NameAnimationsList nameAnimation) {
		eye_anim.Play(nameAnimation.ToString());
		lefthand_anim.Play(nameAnimation.ToString());
		righthand_anim.Play(nameAnimation.ToString());
	}

	public void PlayEyeAnimation(CharacterAnimationManager.NameAnimationsList nameAnimated) {
		eye_anim.Play(nameAnimated.ToString());
	}

    public void PlayLeftHandAnimation(NameAnimationsList nameAnimated) {
        lefthand_anim.Play(nameAnimated.ToString());
    }

	public void PlayRightHandAnimation(CharacterAnimationManager.NameAnimationsList nameAnimated) {
		righthand_anim.Play(nameAnimated.ToString());
	}
	
	public void PlayRampageAnimation() {
        int r = Random.Range(8, 10);
        NameAnimationsList nameList = (NameAnimationsList)r;
        eye_anim.Play(nameList.ToString());
	}
	
	public void PlaySad2Animation() {
        eye_anim.Play(NameAnimationsList.sad2.ToString());
	}
}
