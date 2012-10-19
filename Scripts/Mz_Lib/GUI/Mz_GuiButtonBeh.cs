using UnityEngine;
using System.Collections;

public class Mz_GuiButtonBeh : MonoBehaviour {

	private Mz_BaseScene gameController;
    private Vector3 originalScale = Vector3.one;
    public bool _ignoreScale;
	
	
		
	
	// Use this for initialization
    void Start ()
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<Mz_BaseScene> ();
        originalScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnMouseDown ()
	{
        if(_ignoreScale) {
            this.OnMouseEnter();
            this.OnMouseExit();
        }

        gameController.OnInput(this.gameObject.name);
        gameController.audioEffect.PlayOnecSound(gameController.audioEffect.buttonDown_Clip);
	}

    void OnMouseEnter()
    {
        this.transform.localScale = originalScale * 1.1f;
//        gameController.audioEffect.PlayOnecSound(gameController.audioEffect.buttonHover_Clip);
    }
	
	void OnMouseOver() {
        this.transform.localScale = originalScale * 1.15f;
        gameController.OnPointerOverName(this.gameObject.name);
	}

    void OnMouseExit()
    {
        this.transform.localScale = originalScale;
    }
	
	void OnMouseUp() {
		
	}
}
