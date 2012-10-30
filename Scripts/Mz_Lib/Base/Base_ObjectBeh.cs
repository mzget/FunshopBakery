using UnityEngine;
using System.Collections;

public class Base_ObjectBeh : MonoBehaviour {
    
    protected bool _OnTouchBegin = false;
	protected bool _OnTouchRelease = false;


	// Use this for initialization
	void Start () {
	
	}
	
	protected virtual void Update() {       
        //Debug.Log(this.name + " : update");

        if (_OnTouchBegin && _OnTouchRelease) {
            OnTouchDown();
        }
		        
//        if (Input.touchCount > 0) {
//            Touch touch = Input.GetTouch(0);
//
//            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Moved) {
//                _OnTouchBegin = false;
//                _OnTouchRelease = false;
//            }
//        }
	}
	
	protected IEnumerator WaitForEndUpdate() {
		yield return new WaitForFixedUpdate();
	}

	#region <!-- On Mouse Events.

	protected virtual void OnTouchBegan() {
        _OnTouchBegin = true;
	}
    protected virtual void OnTouchDown()
    {
        /// do something.
		
        _OnTouchBegin = false;
        _OnTouchRelease = false;
    }
    protected virtual void OnTouchEnded() {
		_OnTouchRelease = true;
    }
    protected virtual void OnTouchDrag() {
    	Debug.Log("Class : Base_ObjectBeh." + "OnTouchDrag");
    }

    #endregion
}
