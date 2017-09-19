using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject doorHandle;
	public GameObject doorFrame;
	public GameObject rotatingDoor;
	private float currentDoorRotation;
	public float rotationTimeOpening = 5f;
	public float rotationTimeClosing = .2f;
	public float rotationAngle = 65f;
	private Axis rotationAxis = Axis.Y;

	private bool isDoorOpen = false;
	private bool isDoorBusy = false;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;
	private Action onDoneMoving = null;
	private const float MAX_ROTATION_DIFF = 0.1f;

	// Sounds
	public AudioClip openSqueek;
	public AudioClip openHandle;
	public AudioClip closeSlam;

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
				AudioSource.PlayClipAtPoint(openHandle, doorFrame.transform.position);
				StartCoroutine(UnitUtilities.RotateRoundAxis(rotationTimeOpening, rotationAngle, rotationAxis, rotatingDoor, onDoneMoving, openHandle.length));
			}
			else 
			{
				StartCoroutine(UnitUtilities.RotateRoundAxis(rotationTimeClosing, -rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
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
		AudioSource.PlayClipAtPoint(openSqueek, doorFrame.transform.position);
	}

	private void OnDoorClosed()
	{
		print("The door is now closed");
		AudioSource.PlayClipAtPoint(closeSlam, doorFrame.transform.position);
	}

	private void OnDoneMoving() 
	{
		print("I am done moving");
		isDoorBusy = false;
	}
}
