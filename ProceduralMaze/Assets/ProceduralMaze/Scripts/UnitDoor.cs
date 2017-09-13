using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject doorFrame;
	public GameObject rotatingDoor;
	public float currentDoorRotation;
	public float rotationTime = 5f;
	public float rotationAngle = 65f;
	public Axis rotationAxis = Axis.Y;

	private bool isDoorOpen = false;
	private bool isDoorBusy = false;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;
	private Action onDoneMoving = null;

	private const float MAX_ROTATION_DIFF = 0.1f;

	private void Awake()
	{
		onDoorClosed += OnDoorClosed;
		onDoorOpen += OnDoorOpen;
		onDoneMoving += OnDoneMoving;
	}
	private void Start()
	{
		currentDoorRotation = rotatingDoor.transform.eulerAngles.y;
	}

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.E)) {
			InteractWithDoor();
		}

		CheckDoorState();
	}

	private void InteractWithDoor() 
	{
		if (!isDoorBusy)
		{
			isDoorBusy = true;
			if (!isDoorOpen) 
			{
				StartCoroutine(UnitUtilities.RotateRoundAxis(rotationTime, rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
			}
			else 
			{
				StartCoroutine(UnitUtilities.RotateRoundAxis(rotationTime, -rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
			}
		}
	}

	/// <summary>
	/// Sends information to listeners when the door is open,
	/// and when the door is closed.
	/// </summary>
	private void CheckDoorState() 
	{
		if (currentDoorRotation != rotatingDoor.transform.eulerAngles.y)
		{
			float frameRotation = doorFrame.transform.eulerAngles.y;
			float doorRotation = rotatingDoor.transform.eulerAngles.y;

			float rotationDiff = Math.Abs(frameRotation - doorRotation);

			if (rotationDiff < MAX_ROTATION_DIFF) 
			{
				if (isDoorOpen) 
				{
					if (onDoorClosed != null) 
					{
						onDoorClosed();
					}
					isDoorOpen = false;
				}
			}
			else 
			{
				if (!isDoorOpen)
				{
					if (onDoorOpen != null) 
					{
						onDoorOpen();
					}
					isDoorOpen = true;
				}
			}
			currentDoorRotation = rotatingDoor.transform.eulerAngles.y;
		}	
	}

	private void OnDoorOpen() 
	{
		print("The door is now open");
	}

	private void OnDoorClosed()
	{
		print("The door is now closed");
	}

	private void OnDoneMoving() 
	{
		print("I am done moving");
		isDoorBusy = false;
	}
}
