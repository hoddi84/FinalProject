using System;
using UnityEngine;

public class VRHandController : MonoBehaviour {

	private SteamVR_TrackedController _trackedController;
	private const string WALL = "Wall";
	private const string DOOR_HANDLE = "DoorHandle";

	public Action<Collider> onTriggerClicked = null;
	public Action<Collider> onTriggerUnclicked = null;
	private bool _triggerPressed = false;

	void Awake()
	{
		_trackedController = GetComponent<SteamVR_TrackedController>();
		if (_trackedController == null)
		{
			gameObject.AddComponent<SteamVR_TrackedController>();
			_trackedController = GetComponent<SteamVR_TrackedController>();

			_trackedController.TriggerClicked += OnTriggerClicked;
			_trackedController.TriggerUnclicked += OnTriggerUnclicked;
		}
	}

	void OnTriggerClicked(object sender, ClickedEventArgs e)
	{
		_triggerPressed = true;

		if (onTriggerClicked != null)
		{
			onTriggerClicked();
		}
	}

	void OnTriggerUnclicked(object sender, ClickedEventArgs e)
	{
		_triggerPressed = false;

		if (onTriggerUnclicked != null)
		{
			onTriggerUnclicked();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == WALL)
		{
			StartCoroutine(UnitUtilities.TriggerVibration(_trackedController, 1, .1f));
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == DOOR_HANDLE)
		{
			StartCoroutine(UnitUtilities.TriggerVibration(_trackedController, 1, .1f));
		}
	}
}
