using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject frame;
	public GameObject door;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;

	private const float MAX_ROTATION_DIFF = 0.1f;

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.E)) {
			StartCoroutine(UnitUtilities.RotateRoundAxis(5f, 45, Axis.Y, door));
		}
		if (Input.GetKeyDown(KeyCode.R)) 
		{
			IsDoorClosed();
		}
	}

	private bool IsDoorClosed() 
	{
		float frameRotation = frame.transform.eulerAngles.y;
		float doorRotation = door.transform.eulerAngles.y;

		float rotationDiff = Math.Abs(frameRotation - doorRotation);

		if (rotationDiff < MAX_ROTATION_DIFF) 
		{
			print("true");
			return true;
		}
		else 
		{
			print("false");
			return false;
		}
	}
}
