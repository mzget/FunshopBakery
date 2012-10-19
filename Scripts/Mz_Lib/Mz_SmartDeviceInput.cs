using UnityEngine;
using System.Collections;

public class Mz_SmartDeviceInput {
	
	/// <summary>
	/// SmartDevice input call with Update or FixUpdate function.
	/// </summary>
	public static void IOS_INPUT () {
		if(Camera.main == null) {
			Debug.Log("MainCamera has null");
			return;		
		}
		
		RaycastHit hit ;
		if(Input.touchCount >= 1) {
			Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
			if(touch.phase == TouchPhase.Began) {
            	if (Physics.Raycast(ray, out hit)) {
                	hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
				}
			}
			
			if(touch.phase == TouchPhase.Moved) {
				if(Physics.Raycast(ray, out hit)) {
					hit.collider.SendMessage("OnMouseDrag", SendMessageOptions.DontRequireReceiver);
				}
			}
			
			if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				if(Physics.Raycast(ray, out hit)) {
					hit.collider.SendMessage("OnMouseUp", SendMessageOptions.DontRequireReceiver);
				}
			}

            Debug.DrawRay(ray.origin, ray.direction, Color.red);
		}
	}
	
	public static void IOS_GUITouch() 
	{
        GameObject cameraRaycast;
//        if (Camera.main) {
//            cameraRaycast = Camera.main.gameObject;
//        }
//        else {
//            cameraRaycast = GameObject.FindGameObjectWithTag("Camera_UI");
//        }
		cameraRaycast = GameObject.FindGameObjectWithTag("Camera_UI");
		
		if(Input.touchCount >= 1) 
		{
            Ray ray = cameraRaycast.camera.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;
		
			Debug.Log(cameraRaycast);
			Debug.DrawRay(ray.origin, ray.direction);

			if(Input.GetTouch(0).phase == TouchPhase.Began) 
			{				
	            if (Physics.Raycast(ray, out hit))
					hit.collider.gameObject.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
			}
			else if (Input.GetTouch(0).phase == TouchPhase.Moved) 
			{					
				if(Physics.Raycast(ray, out hit))
					hit.collider.gameObject.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
