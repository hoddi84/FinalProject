using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandControllerManager : MonoBehaviour {

	public Action onTriggerClicked = null;
	public Action onTriggerUnclicked = null;
	public VRHandController controller1;
	public VRHandController controller2;

	void Awake()
	{
		if (controller1 != null)
		{
			controller1.onTriggerClicked += OnTriggerClicked;
			controller1.onTriggerUnclicked += OnTriggerUnclicked;
		}
		if (controller2 != null)
		{
			controller2.onTriggerClicked += OnTriggerClicked;
			controller2.onTriggerUnclicked += OnTriggerUnclicked;
		}
	}

	void OnTriggerClicked()
	{
		if (onTriggerClicked != null) 
		{
			onTriggerClicked();
		}
	}

	void OnTriggerUnclicked()
	{
		if (onTriggerUnclicked != null) 
		{
			onTriggerUnclicked();
		}
	}
}
