using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType {

	DOOR_HANDLE,
	WALL,
}

public class VRHandController : MonoBehaviour {

	private SteamVR_TrackedController controller;

	public Action onTriggerClicked = null;
	public Action onTriggerUnclicked = null;
	public Action<Collider> onTriggerClickedCollider = null;
	private bool triggerPressed = false;

	void Awake()
	{
		controller = GetComponent<SteamVR_TrackedController>();
		if (controller == null)
		{
			gameObject.AddComponent<SteamVR_TrackedController>();
			controller = GetComponent<SteamVR_TrackedController>();

			controller.TriggerClicked += OnTriggerClicked;
			controller.TriggerUnclicked += OnTriggerUnclicked;
		}
	}

	void OnTriggerClicked(object sender, ClickedEventArgs e)
	{
		triggerPressed = true;

		if (onTriggerClicked != null)
		{
			onTriggerClicked();
		}
	}

	void OnTriggerUnclicked(object sender, ClickedEventArgs e)
	{
		triggerPressed = false;

		if (onTriggerUnclicked != null)
		{
			onTriggerUnclicked();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Wall")
		{
			print("LOL");
			StartCoroutine(UnitUtilities.TriggerVibration(controller, 1, .1f));
		}
		else
		{
			if (triggerPressed)
			{
				if (onTriggerClickedCollider != null)
				{
					onTriggerClickedCollider(other);
					triggerPressed = false; // added, so only happens once.
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{

	}

	// TODO
	// Initialize with a rigidbody and a collider sphere.


}
