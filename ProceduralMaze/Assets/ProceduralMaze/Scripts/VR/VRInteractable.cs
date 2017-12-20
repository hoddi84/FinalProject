using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VRInteractable : MonoBehaviour {

	private VRHandControllerManager controllerManager;
	public Action onInteracted = null;

	void Awake() 
	{

		controllerManager = FindObjectOfType(typeof(VRHandControllerManager)) as VRHandControllerManager;
		
		if (controllerManager != null)
		{
			controllerManager.onTriggerClickedCollider += ControllerInteracted;
		}

	}

	void ControllerInteracted(Collider other, SteamVR_TrackedController controller)
	{
		if (other.gameObject.GetInstanceID() == gameObject.GetInstanceID())
		{
			StartCoroutine(UnitUtilities.TriggerVibration(controller, 1, .1f));

			if (onInteracted != null) 
			{
				onInteracted();
			}
		}
	}
}
