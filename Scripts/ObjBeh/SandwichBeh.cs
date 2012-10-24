using UnityEngine;
using System.Collections;

public class SandwichBeh : GoodsBeh {

	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		
		base._canDragaable = true;
	}
	
	protected override void ImplementDraggableObject ()
	{
		base.ImplementDraggableObject ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
	}
}
