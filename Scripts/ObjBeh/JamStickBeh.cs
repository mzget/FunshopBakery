using UnityEngine;
using System.Collections;

public class JamStickBeh : ObjectsBeh {


    BakeryShop sceneManager;

    private Vector3 original_Position = new Vector3(-0.35f, 0.11f, -1); 

	// Use this for initialization
    protected override void Start()
    {
        base.Start();

        sceneManager = baseScene.GetComponent<BakeryShop>();
    }
	
	// Update is called once per frame
	protected override void Update() {

        base.Update();

		if(base._isDraggable) {			
            Vector3 screenPoint;
            Ray ray;
            RaycastHit hit;
            this.transform.parent = sceneManager.bakeryShop_backgroup_group.transform;
            if (Input.touchCount >= 1) {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                screenPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            this.transform.position = new Vector3(screenPoint.x, screenPoint.y, -5f);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if(hit.collider.name == sceneManager.strawberryJam_instance.name) {
                    sceneManager.strawberryJam_instance.PlayActiveAnimation();
                }
            }

            Debug.DrawRay(ray.origin, ray.direction, Color.green);
		}
		
	}

    #region <!-- Input Events.

    protected override void OnTouchEnded ()
    {
        base.OnTouchEnded();

        this.transform.parent = sceneManager.toastObj_transform_group;
        this.transform.localPosition = original_Position;
    }

    #endregion
}
