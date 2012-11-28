using UnityEngine;
using System.Collections;

public class GameEffectManager : MonoBehaviour {
	
    public const string GameEffect_PATH = "GameEffects/";
	
	// Use this for initialization
//	void Start () {
//	
//	}
	
	public void Create2DSpriteAnimationEffect(string targetName, Transform transform) {
        GameObject effect = Instantiate(Resources.Load(GameEffect_PATH + targetName, typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
        effect.transform.parent = transform;
        effect.transform.localScale = transform.localScale;
        effect.transform.position += Vector3.back;

        tk2dAnimatedSprite animatedSprite = effect.GetComponent<tk2dAnimatedSprite>();
        animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite anim, int id) {
            Destroy(effect);
            animatedSprite = null;
        };
	}
 }
