using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandControllerManager : MonoBehaviour {

	public Action onTriggerClicked = null;
	public Action onTriggerUnclicked = null;
	public Action<Collider, SteamVR_TrackedController> onTriggerClickedCollider = null;
	
	public VRHandController controller1;
	public VRHandController controller2;

	void Awake()
	{
		if (controller1 != null)
		{
			controller1.onTriggerClicked += OnTriggerClicked;
			controller1.onTriggerClickedCollider += OnTriggerClickedCollider;
			controller1.onTriggerUnclicked += OnTriggerUnclicked;
		}

		if (controller2 != null)
		{
			controller2.onTriggerClicked += OnTriggerClicked;
			controller2.onTriggerClickedCollider += OnTriggerClickedCollider;
			controller2.onTriggerUnclicked += OnTriggerUnclicked;
		}
	}

	void OnTriggerClicked()
	{
		if (onTriggerClicked != null) 
		{
			onTriggerClicked();
		}
		print("Clicekd Trigger");
	}

	void OnTriggerClickedCollider(Collider clickedCollider, SteamVR_TrackedController controller)
	{
		if (onTriggerClickedCollider != null)
		{
			onTriggerClickedCollider(clickedCollider, controller);
		}
		print("Clicked Trigger on Collider: " + clickedCollider.name);
	}

	void OnTriggerUnclicked()
	{
		if (onTriggerUnclicked != null) 
		{
			onTriggerUnclicked();
		}
		print("Unclicked Trigger");
	}
}
