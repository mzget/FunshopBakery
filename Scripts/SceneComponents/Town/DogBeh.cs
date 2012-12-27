using UnityEngine;
using System.Collections;

public class DogBeh : MonoBehaviour {
	
	const string WALKLEFT_FUNC = "WalkLeft";
	const string FUNC_WALKRIGHT = "WalkRight";
    const string FUNC_RANDOM_BEH = "RandomBeh";

    public enum NameAnimationList { None = 0, moveBackward, moveforward, Glad, Glad2, Eat, Crap, ChaseBite, };
	public static NameAnimationList nameAnimationList;
	tk2dAnimatedSprite animatedSprite;
	
	
	void Awake() {
		iTween.Init(this.gameObject);
		
		animatedSprite = this.gameObject.GetComponent<tk2dAnimatedSprite>();
	}
	
	// Use this for initialization
	void Start () {
		this.WalkLeft();
	}
	
	void WalkLeft() {
		animatedSprite.Play(NameAnimationList.moveforward.ToString());
		iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(1.2f, -0.52f, -2f), "islocal", true, "time", 5f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.none,
			"oncomplete", FUNC_WALKRIGHT, "oncompletetarget", this.gameObject));
	} 
	
	void WalkRight() {
		animatedSprite.Play(NameAnimationList.moveBackward.ToString());
		iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(1.7f, -0.52f, -2f), "islocal", true, "time", 3f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.none,
			"oncomplete", FUNC_RANDOM_BEH, "oncompletetarget", this.gameObject));
	}

    void RandomBeh() {
        int r = Random.Range(3, 7);
        NameAnimationList nameAnimated = (NameAnimationList)r;
        animatedSprite.Play(nameAnimated.ToString());
        animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite sprite, int clipId) {
            WalkLeft();
        };
    }

    internal static void ChaseBite() {
        nameAnimationList = NameAnimationList.ChaseBite;
    }

    void ChaseBakeryTruck() {
		iTween.Stop(this.gameObject);
		
        animatedSprite.Play(NameAnimationList.moveBackward.ToString());
        this.transform.position = new Vector3(-3f, -0.9f, -4f);
        iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(3f, -0.9f, -4f), "islocal", false, "time", 12f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.none,
            "oncomplete", WALKLEFT_FUNC, "oncompletetarget", this.gameObject));
    }
	
	// Update is called once per frame
	void Update () {
        if (nameAnimationList == NameAnimationList.ChaseBite) {
            nameAnimationList = NameAnimationList.None;

            this.ChaseBakeryTruck();
        }
	}
}
