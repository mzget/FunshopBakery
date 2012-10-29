using UnityEngine;
using System.Collections;

public class Mz_SmartDeviceInput : MonoBehaviour {
	
	/// <summary>
	/// SmartDevice input call with Update or FixUpdate function.
	/// </summary>
	public static void ImplementTouchInput () {
		if(Camera.main == null) {
			Debug.Log("MainCamera has null");
			return;		
		}

		if(Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray cursorRay = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (touch.phase == TouchPhase.Began) {
                if (Physics.Raycast(cursorRay, out hit)) {
                    hit.collider.SendMessage("OnTouchBegan", SendMessageOptions.DontRequireReceiver);
                }
            }

			if (touch.phase == TouchPhase.Stationary) {
				if( Physics.Raycast(cursorRay, out hit)) {
					hit.collider.gameObject.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
				}
			}

			if(touch.phase == TouchPhase.Moved) {
				if(Physics.Raycast(cursorRay, out hit)) {
					hit.collider.SendMessage("OnTouchDrag", SendMessageOptions.DontRequireReceiver);
				}
			}
			
            if(Input.GetTouch(0).phase == TouchPhase.Ended) {
				if(Physics.Raycast(cursorRay, out hit)) {
					hit.collider.SendMessage("OnTouchEnded", SendMessageOptions.DontRequireReceiver);
				}	
			}
        
            Debug.DrawRay(cursorRay.origin, cursorRay.direction, Color.red);
		}
	}	
    
    public static void ImplementMouseInput () {
		if(Camera.main == null) {
			Debug.Log("MainCamera has null");
			return;		
		}

		Ray cursorRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (cursorRay, out hit)) {
				hit.collider.SendMessage ("OnTouchBegan", SendMessageOptions.DontRequireReceiver);
			}
		}

		if (Input.GetMouseButton (0)) {
			if(Physics.Raycast(cursorRay, out hit)) {
				hit.collider.SendMessage("OnTouchDrag", SendMessageOptions.DontRequireReceiver);
			}		
		}

        //if (touch.phase == TouchPhase.Stationary) {
        //    if( Physics.Raycast( cursorRay, out hit)) {
        //        hit.collider.gameObject.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
        //    }
        //}
			
        if(Input.GetMouseButtonUp(0)) {
            if(Physics.Raycast(cursorRay, out hit)) {
                hit.collider.SendMessage("OnTouchEnded", SendMessageOptions.DontRequireReceiver);
            }	
        }
	}
}
