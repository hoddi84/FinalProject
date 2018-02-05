using System;
using UnityEngine;
using MazeVR;

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
	private Action onDoneMoving = null;
	private const float MAX_ROTATION_DIFF = 0.1f;

	public bool isDoorLocked = true;
	public GameObject doorLockMechanic;
	public GameObject doorOpenMechanic;
	private SpriteRenderer lockRenderer;
	private SpriteRenderer openRenderer;
	private DirectorClickable directorClickableLock;
	private DirectorClickable directorClickableOpen;

	private UIController uiController;

	private void Awake()
	{
		doorManager = FindObjectOfType(typeof(UnitDoorManager)) as UnitDoorManager;

		vrInteractable = doorHandle.GetComponent<VRInteractable>();
		
		lockRenderer = doorLockMechanic.GetComponentInChildren<SpriteRenderer>();
		openRenderer = doorOpenMechanic.GetComponentInChildren<SpriteRenderer>();

		directorClickableLock = doorLockMechanic.GetComponentInChildren<DirectorClickable>();
		directorClickableOpen = doorOpenMechanic.GetComponentInChildren<DirectorClickable>();	

		uiController = FindObjectOfType(typeof(UIController)) as UIController;
	}

	void OnEnable()
	{
		vrInteractable.onControllerInteracted += InteractWithDoor;

		onDoneMoving += OnDoneMoving;

		directorClickableLock.onHit += OnDirectorDoorLock;
		directorClickableOpen.onHit += OnDirectorDoorOpen;

		PerformPresenceMeterActions(doorManager.presenceSliderValue);
	}

	void OnDisable()
	{
		vrInteractable.onControllerInteracted -= InteractWithDoor;

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

		if (uiController != null)
		{
			if (uiController.isSimpleCheckOn)
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
		if (!isDoorBusy)
		{
			float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

			if (rnd <= doorManager.scarySliderValue && isDoorOpen)
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
				if (isDoorOpen && !isDoorBusy)
				{
					CloseDoor(false);
				}
			}
			// Play door open sound.
			// Door has normal open delay.
			else if (randomAction >= .3 && randomAction < .7)
			{
				if (!isDoorOpen && !isDoorBusy)
				{
					OpenDoor();
				}
			}
			// No sound.
			// Door is open.
			else
			{
				if (!isDoorOpen && !isDoorBusy)
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
		if (!isDoorBusy && !isDoorLocked)
		{
			if (!isDoorOpen) 
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
		isDoorBusy = true;
		float delayTime;
		delayTime = useDelay ? doorManager.doorOpenTime : 0;

		if (useSound)
		{
			AudioSource.PlayClipAtPoint(doorManager.openHandleClip, doorFrame.transform.position);
		}
		StartCoroutine(UnitUtilities.RotateRoundAxis(delayTime, doorManager.rotationAngle, rotationAxis, rotatingDoor,
			delegate() {
				if (useSound)
				{
					OnDoorOpen();
				}
		},
		delegate() {
			OnDoneMoving();
		}, doorManager.openHandleClip.length));
	}

	void CloseDoor(bool useDelay = true)
	{
		isDoorBusy = true;
		float delayTime;
		delayTime = useDelay ? doorManager.doorCloseTime : 0;

		StartCoroutine(UnitUtilities.RotateRoundAxis(delayTime, -doorManager.rotationAngle, rotationAxis, rotatingDoor, 
		delegate(){
			OnDoneMoving();
			OnDoorClosed();
		}));
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
					isDoorOpen = false;
				}
			}
			else 
			{
				if (!isDoorOpen)
				{
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
