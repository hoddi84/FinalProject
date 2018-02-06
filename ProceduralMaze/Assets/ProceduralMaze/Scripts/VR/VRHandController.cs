using System;
using UnityEngine;
using MazeUtiliy;

namespace MazeVR {

	public class VRHandController : MonoBehaviour {

		private SteamVR_TrackedController _trackedController = null;
		private GameObject _selectedGameObject = null;
		private bool _controllerTriggerPressed = false;

		private const string WALL = "Wall";
		private const string DOOR_HANDLE = "DoorHandle";

		public Action<Collider> onControllerTriggerClicked = null;
		public Action<Collider> onControllerTriggerUnclicked = null;

		void Awake()
		{
			_trackedController = GetComponent<SteamVR_TrackedController>();

			if (_trackedController == null)
			{
				gameObject.AddComponent<SteamVR_TrackedController>();
				_trackedController = GetComponent<SteamVR_TrackedController>();
			}
		}

		void OnEnable()
		{
			if (_trackedController != null)
			{
				_trackedController.TriggerClicked += OnControllerTriggerClicked;
				_trackedController.TriggerUnclicked += OnControllerTriggerUnclicked;
			}
		}

		void OnDisable()
		{
			if (_trackedController != null)
			{
				_trackedController.TriggerClicked -= OnControllerTriggerClicked;
				_trackedController.TriggerUnclicked -= OnControllerTriggerUnclicked;
			}
		}

		void VibrateOnInteracted(GameObject interactedGameObject, float strength, float length)
		{
			switch (interactedGameObject.tag) 
			{
				case WALL:
					StartCoroutine(UtilityTools.TriggerVibration(_trackedController, 1, .1f));
				break;

				case DOOR_HANDLE:
					StartCoroutine(UtilityTools.TriggerVibration(_trackedController, 1, .1f));
				break;
			}
		}

		void OnControllerTriggerClicked(object sender, ClickedEventArgs e)
		{
			_controllerTriggerPressed = true;

			if (onControllerTriggerClicked != null)
			{
				onControllerTriggerClicked(_selectedGameObject.GetComponent<Collider>());
			}

			VibrateOnInteracted(_selectedGameObject, 1, .1f);
		}

		void OnControllerTriggerUnclicked(object sender, ClickedEventArgs e)
		{
			_controllerTriggerPressed = false;

			if (onControllerTriggerUnclicked != null)
			{
				onControllerTriggerUnclicked(_selectedGameObject.GetComponent<Collider>());
			}
		}

		void OnTriggerStay(Collider other)
		{
			VibrateOnInteracted(other.gameObject, 1, .1f);
		}

		void OnTriggerEnter(Collider other)
		{
			_selectedGameObject = other.gameObject;

			VibrateOnInteracted(_selectedGameObject, 1, .1f);
		}

		void OnTriggerExit(Collider other)
		{
			if (_selectedGameObject.GetInstanceID() == other.gameObject.GetInstanceID())
			{
				_selectedGameObject = null;
			}
		}
	}
}

