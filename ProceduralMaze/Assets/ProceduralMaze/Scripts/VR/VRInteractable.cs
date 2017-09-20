using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VRInteractable : MonoBehaviour {

	public VRHandControllerManager controllerManager;
	public Action onInteracted = null;

	void Awake() 
	{
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
