using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandController : MonoBehaviour {

	public Action onTriggerClicked = null;
	public Action onTriggerUnclicked = null;
	private SteamVR_TrackedController controller;

	void Awake() 
	{
		controller = GetComponent<SteamVR_TrackedController>();
		if (controller == null) 
		{
			gameObject.AddComponent<SteamVR_TrackedController>();
			controller = GetComponent<SteamVR_TrackedController>();
		}

		controller.TriggerClicked += OnTriggerClicked;
		controller.TriggerUnclicked += OnTriggerUnclicked;
	}

	private void OnTriggerClicked(object sender, ClickedEventArgs args)
	{
		if (onTriggerClicked != null) 
		{
			onTriggerClicked();
		}
	}

	private void OnTriggerUnclicked(object sender, ClickedEventArgs args)
	{
		if (onTriggerUnclicked != null) 
		{
			onTriggerUnclicked();
		}
	}
}
