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

	void ControllerInteracted(Collider other)
	{
		if (other.gameObject.GetInstanceID() == gameObject.GetInstanceID())
		{
			if (onInteracted != null) 
			{
				onInteracted();
			}
		}
	}
}
