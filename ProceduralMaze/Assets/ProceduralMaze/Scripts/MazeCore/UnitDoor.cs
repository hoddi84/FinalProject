﻿using System;
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

	public bool isDoorOpen = false;
	private bool isDoorBusy = false;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;
	private Action onDoneMoving = null;
	private const float MAX_ROTATION_DIFF = 0.1f;

	// Testing for sprites. Needs refactoring.
	public bool isDoorLocked = true;
	public GameObject doorLockMechanic;
	public GameObject doorOpenMechanic;
	private SpriteRenderer lockRenderer;
	private SpriteRenderer openRenderer;
	private DirectorClickable directorClickableLock;
	private DirectorClickable directorClickableOpen;

	private void Awake()
	{
		doorManager = FindObjectOfType(typeof(UnitDoorManager)) as UnitDoorManager;

		vrInteractable = doorHandle.GetComponent<VRInteractable>();
		
		lockRenderer = doorLockMechanic.GetComponentInChildren<SpriteRenderer>();
		openRenderer = doorOpenMechanic.GetComponentInChildren<SpriteRenderer>();

		directorClickableLock = doorLockMechanic.GetComponentInChildren<DirectorClickable>();
		directorClickableOpen = doorOpenMechanic.GetComponentInChildren<DirectorClickable>();	

		//doorTriggerManager = parent.GetComponentInChildren<UnitDoorTriggerManager>();

	}

	void OnEnable()
	{
		vrInteractable.onInteracted += InteractWithDoor;

		onDoorClosed += OnDoorClosed;
		onDoorOpen += OnDoorOpen;
		onDoneMoving += OnDoneMoving;

		directorClickableLock.onHit += OnDirectorDoorLock;
		directorClickableOpen.onHit += OnDirectorDoorOpen;

		//PerformScaryMeterActions(doorManager.scarySliderValue);
		PerformPresenceMeterActions(doorManager.presenceSliderValue);
	}

	void OnDisable()
	{
		vrInteractable.onInteracted -= InteractWithDoor;

		onDoorClosed -= OnDoorClosed;
		onDoorOpen -= OnDoorOpen;
		onDoneMoving -= OnDoneMoving;

		directorClickableLock.onHit -= OnDirectorDoorLock;
		directorClickableOpen.onHit -= OnDirectorDoorOpen;
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

	/*
	 * High scary meter will increase the chance a door 
	 * will close after entering through it.
	 */
	public void PerformScaryMeterActions()
	{
		float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

		if (rnd <= doorManager.scarySliderValue && isDoorOpen)
		{
			// Slam door shut.
			StartCoroutine(UnitUtilities.RotateRoundAxis(doorManager.doorCloseTime, -doorManager.rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
		}
	}

	/*
	 * High presence will increase the chance that you hear
	 * a door slam or door being open.
	 */
	private void PerformPresenceMeterActions(float value)
	{
		float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

		float choice = UnityEngine.Random.Range(0.0f, 1.0f);

		if (choice < .5)
		{
			if (rnd <= value)
			{
				AudioSource.PlayClipAtPoint(doorManager.closeDoorClip, doorFrame.transform.position);
			}
		}
		else 
		{
			if (rnd <= value && !isDoorOpen)
			{
				isDoorBusy = true;
				StartCoroutine(UnitUtilities.RotateRoundAxis(doorManager.doorOpenTime, doorManager.rotationAngle, rotationAxis, rotatingDoor, onDoneMoving));
			}
		}
	}

	private void InteractWithDoor() 
	{
		if (!isDoorBusy && !isDoorLocked)
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

		if (!isDoorLocked)
		{
			lockRenderer.sprite = doorManager.doorSpriteLockOpen;
		}
		else
		{
			lockRenderer.sprite = doorManager.doorSpriteLockClosed;
		}

		if (!isDoorOpen)
		{
			openRenderer.sprite = doorManager.doorSpriteClosed;
		}
		else
		{
			openRenderer.sprite = doorManager.doorSpriteOpen;
		}
	}

	private void OnDirectorDoorOpen()
	{
		InteractWithDoor();
	}

	private void OnDirectorDoorLock()
	{
		isDoorLocked = !isDoorLocked;
	}

	private void OnDoorOpen() 
	{
		AudioSource.PlayClipAtPoint(doorManager.openDoorClip, doorFrame.transform.position);
	}

	private void OnDoorClosed()
	{
		AudioSource.PlayClipAtPoint(doorManager.closeDoorClip, doorFrame.transform.position);
	}

	private void OnDoneMoving() 
	{
		isDoorBusy = false;
	}
}
