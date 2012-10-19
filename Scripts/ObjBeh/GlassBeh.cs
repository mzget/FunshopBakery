using UnityEngine;
using System.Collections;

public class GlassBeh : GoodsBeh {



	// Use this for initialization
    protected override void Start()
    {
        base.Start();

        _canActive = true;
        _canDragaable = true;
		originalPosition = this.transform.position;
    }
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
	
	public override void OnMouseUp ()
	{
		if(base._isDraggable)
			_isDropObject = true;
	}
}
