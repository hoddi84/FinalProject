using System;
using UnityEngine;

namespace MazeVR {

	public class VRHandControllerManager : MonoBehaviour {

	public Action<Collider> onTriggerClicked = null;
	public Action<Collider> onTriggerUnclicked = null;
	
	public VRHandController controller1;
	public VRHandController controller2;

	void OnEnable()
	{
		if (controller1 != null)
		{
			controller1.onControllerTriggerClicked += OnTriggerClicked;
			controller1.onControllerTriggerUnclicked += OnTriggerUnclicked;
		}

		if (controller2 != null)
		{
			controller2.onControllerTriggerClicked += OnTriggerClicked;
			controller2.onControllerTriggerUnclicked += OnTriggerUnclicked;
		}
	}

	void OnDisable()
	{
		if (controller1 != null)
		{
			controller1.onControllerTriggerClicked -= OnTriggerClicked;
			controller1.onControllerTriggerUnclicked -= OnTriggerUnclicked;
		}

		if (controller2 != null)
		{
			controller2.onControllerTriggerClicked -= OnTriggerClicked;
			controller2.onControllerTriggerUnclicked -= OnTriggerUnclicked;
		}
	}

	void OnTriggerClicked(Collider clickedCollider)
	{
		if (onTriggerClicked != null) 
		{
			onTriggerClicked(clickedCollider);
		}
	}

	void OnTriggerUnclicked(Collider unclickedCollider)
	{
		if (onTriggerUnclicked != null) 
		{
			onTriggerUnclicked(unclickedCollider);
		}
	}
}
}
