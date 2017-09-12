using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject frame;
	public GameObject door;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.E)) {
			StartCoroutine(UnitUtilities.RotateRoundAxis(5f, 45, Axis.X, door));
		}
	}

	private bool IsDoorClosed() 
	{
		if (frame.transform.forward == door.transform.forward) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}
}
