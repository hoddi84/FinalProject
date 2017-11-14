using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	private UnitDoorManager doorManager;
	public GameObject doorHandle;
	private VRInteractable vrInteractable;
	public GameObject doorFrame;
	public GameObject rotatingDoor;
	private float currentDoorRotation;
	private Axis rotationAxis = Axis.Y;

	private bool isDoorOpen = false;
	private bool isDoorBusy = false;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;
	private Action onDoneMoving = null;
	private const float MAX_ROTATION_DIFF = 0.1f;

	private void Awake()
	{
		doorManager = FindObjectOfType(typeof(UnitDoorManager)) as UnitDoorManager;

		vrInteractable = doorHandle.GetComponent<VRInteractable>();
		vrInteractable.onInteracted += InteractWithDoor;

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
				AudioSource.PlayClipAtPoint(doorManager.openHandleClip, doorFrame.transform.position);
				StartCoroutine(UnitUtilities.RotateRoundAxis(doorManager.doorOpenTime, doorManager.rotationAngle, rotationAxis, rotatingDoor, onDoneMoving, doorManager.openHandleClip.length));
			}
			else 
			{
				StartCoroutine(UnitUtilities.RotateRoundAxis(doorManager.doorCloseTime, -doorManager.rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
			}
		}
	}

	/// <summary>
	/// Sends information to listeners when the door is being opened,
	/// and when the door has been closed.
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
		AudioSource.PlayClipAtPoint(doorManager.openDoorClip, doorFrame.transform.position);
	}

	private void OnDoorClosed()
	{
		print("The door is now closed");
		AudioSource.PlayClipAtPoint(doorManager.closeDoorClip, doorFrame.transform.position);
	}

	private void OnDoneMoving() 
	{
		print("I am done moving");
		isDoorBusy = false;
	}
}
