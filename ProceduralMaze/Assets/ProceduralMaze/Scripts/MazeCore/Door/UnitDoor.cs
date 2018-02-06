using System;
using UnityEngine;
using MazeVR;

namespace MazeCore.Door {

	public class UnitDoor : MonoBehaviour {

	private UnitDoorManager _doorManager;
	private VRInteractable _vrInteractable;
	private UIController _uiController;
	public GameObject doorFrame;
	public GameObject rotatingDoor;
	public GameObject doorHandle;

	private Axis _rotationAxis = Axis.Y;

	private float _currentDoorRotation;
	private bool _isDoorOpen = false;
	private bool _isDoorBusy = false;
	private bool _isDoorLocked = true;
	private const float MAX_ROTATION_DIFF = 0.1f;

	public GameObject doorLockMechanic;
	public GameObject doorOpenMechanic;
	private SpriteRenderer lockRenderer;
	private SpriteRenderer openRenderer;
	private DirectorClickable directorClickableLock;
	private DirectorClickable directorClickableOpen;

	private void Awake()
	{
		_doorManager = FindObjectOfType<UnitDoorManager>();
		_uiController = FindObjectOfType<UIController>();
		_vrInteractable = doorHandle.GetComponent<VRInteractable>();
		
		lockRenderer = doorLockMechanic.GetComponentInChildren<SpriteRenderer>();
		openRenderer = doorOpenMechanic.GetComponentInChildren<SpriteRenderer>();

		directorClickableLock = doorLockMechanic.GetComponentInChildren<DirectorClickable>();
		directorClickableOpen = doorOpenMechanic.GetComponentInChildren<DirectorClickable>();	
	}

	void OnEnable()
	{
		if (_vrInteractable != null)
		{
			_vrInteractable.onControllerInteracted += InteractWithDoor;
		}

		directorClickableLock.onHit += OnDirectorDoorLock;
		directorClickableOpen.onHit += OnDirectorDoorOpen;

		PerformPresenceMeterActions(_doorManager.presenceSliderValue);
	}

	void OnDisable()
	{
		if (_vrInteractable != null)
		{
			_vrInteractable.onControllerInteracted -= InteractWithDoor;
		}

		directorClickableLock.onHit -= OnDirectorDoorLock;
		directorClickableOpen.onHit -= OnDirectorDoorOpen;
	}

	private void Start()
	{
		_currentDoorRotation = rotatingDoor.transform.eulerAngles.y;
	}

	private void Update() 
	{

		CheckDoorState();

		if (_uiController != null)
		{
			if (_uiController.isSimpleCheckOn)
			{
				if (doorLockMechanic.activeInHierarchy && doorOpenMechanic.activeInHierarchy)
				{
					doorLockMechanic.SetActive(false);
					doorOpenMechanic.SetActive(false);
				}
			}
			else
			{
				if (!doorLockMechanic.activeInHierarchy && !doorOpenMechanic.activeInHierarchy)
				{
					doorLockMechanic.SetActive(true);
					doorOpenMechanic.SetActive(true);
				}
			}
		}
	}

	/// <summary>
	/// Event executed when we enter a door trigger.
	/// </summary>
	/// <param name="action">Additional executed commands.</param>
	public void PerformScaryMeterActions(Action action)
	{
		if (!_isDoorBusy)
		{
			float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

			if (rnd <= _doorManager.scarySliderValue && _isDoorOpen)
			{
				CloseDoor();
			}

			if (action != null)
			{
				action();
			}
		}
	}
	
	/// <summary>
	/// Event executed when new Unit is being activated/instantiated.
	/// </summary>
	/// <param name="value">Scary meter value.</param>
	private void PerformPresenceMeterActions(float value)
	{
		float chanceOfAction = UnityEngine.Random.Range(0.0f, 1.0f);
		float randomAction = UnityEngine.Random.Range(0.0f, 1.0f);

		if (chanceOfAction <= value)
		{
			// Play door close sound.
			// No delay on closing door.
			if (randomAction < .3)
			{
				if (_isDoorOpen && !_isDoorBusy)
				{
					CloseDoor(false);
				}
			}
			// Play door open sound.
			// Door has normal open delay.
			else if (randomAction >= .3 && randomAction < .7)
			{
				if (!_isDoorOpen && !_isDoorBusy)
				{
					OpenDoor();
				}
			}
			// No sound.
			// Door is open.
			else
			{
				if (!_isDoorOpen && !_isDoorBusy)
				{
					OpenDoor(false, false);
				}
			}
		}
	}

	/// <summary>
	/// Call to open a closed door, or close an opened door.
	/// </summary>
	private void InteractWithDoor() 
	{
		if (!_isDoorBusy && !_isDoorLocked)
		{
			if (!_isDoorOpen) 
			{
				OpenDoor();
			}
			else 
			{
				CloseDoor();
			}
		}
	}

	void OpenDoor(bool useDelay = true, bool useSound = true)
	{
		_isDoorBusy = true;
		float delayTime;
		delayTime = useDelay ? _doorManager.doorOpenTime : 0;

		if (useSound)
		{
			AudioSource.PlayClipAtPoint(_doorManager.openHandleClip, doorFrame.transform.position);
		}
		StartCoroutine(UnitUtilities.RotateRoundAxis(delayTime, _doorManager.rotationAngle, _rotationAxis, rotatingDoor,
			delegate() {
				if (useSound)
				{
					OnDoorOpen();
				}
			},
			delegate() {
				OnDoneMoving();
			}, 
			_doorManager.openHandleClip.length));
	}

	void CloseDoor(bool useDelay = true)
	{
		_isDoorBusy = true;
		float delayTime;
		delayTime = useDelay ? _doorManager.doorCloseTime : 0;

		StartCoroutine(UnitUtilities.RotateRoundAxis(delayTime, -_doorManager.rotationAngle, _rotationAxis, rotatingDoor, 
			delegate(){
				OnDoneMoving();
				OnDoorClosed();
			}
		));
	}

	/// <summary>
	/// Sends information to listeners when the door is being opened,
	/// and when the door has been closed.
	/// </summary>
	private void CheckDoorState() 
	{
		if (_currentDoorRotation != rotatingDoor.transform.eulerAngles.y)
		{
			float frameRotation = doorFrame.transform.eulerAngles.y;
			float doorRotation = rotatingDoor.transform.eulerAngles.y;

			float rotationDiff = Math.Abs(frameRotation - doorRotation);

			if (rotationDiff < MAX_ROTATION_DIFF) 
			{
				if (_isDoorOpen) 
				{
					_isDoorOpen = false;
				}
			}
			else 
			{
				if (!_isDoorOpen)
				{
					_isDoorOpen = true;
				}
			}
			_currentDoorRotation = rotatingDoor.transform.eulerAngles.y;
		}	

		if (!_isDoorLocked)
		{
			lockRenderer.sprite = _doorManager.doorSpriteLockOpen;
		}
		else
		{
			lockRenderer.sprite = _doorManager.doorSpriteLockClosed;
		}

		if (!_isDoorOpen)
		{
			openRenderer.sprite = _doorManager.doorSpriteClosed;
		}
		else
		{
			openRenderer.sprite = _doorManager.doorSpriteOpen;
		}
	}

	private void OnDirectorDoorOpen()
	{
		InteractWithDoor();
	}

	private void OnDirectorDoorLock()
	{
		_isDoorLocked = !_isDoorLocked;
	}

	private void OnDoorOpen() 
	{
		AudioSource.PlayClipAtPoint(_doorManager.openDoorClip, doorFrame.transform.position);
	}

	private void OnDoorClosed()
	{
		AudioSource.PlayClipAtPoint(_doorManager.closeDoorClip, doorFrame.transform.position);
	}

	private void OnDoneMoving() 
	{
		_isDoorBusy = false;
	}
}
}


