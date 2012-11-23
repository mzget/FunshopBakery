using UnityEngine;
using System.Collections;

public class GameEffectManager : MonoBehaviour {
	
    public const string GameEffect_PATH = "GameEffects/";
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void Create2DSpriteAnimationEffect(string targetName, Vector3 targetPos) {
        GameObject effect = Instantiate(Resources.Load(GameEffect_PATH + targetName, typeof(GameObject)), targetPos, Quaternion.identity) as GameObject;
        tk2dAnimatedSprite animatedSprite = effect.GetComponent<tk2dAnimatedSprite>();
        animatedSprite.animationCompleteDelegate = delegate(tk2dAnimatedSprite anim, int id) {
            Destroy(effect);
            animatedSprite = null;
        };
	}
 }
