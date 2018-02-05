using System;
using UnityEngine;

namespace MazeVR {

	/// <summary>
	/// This script should be placed on a gameObject
	/// that can be interacted on with the VIVE controllers.
	/// </summary>

	[RequireComponent(typeof(Rigidbody))]
	public class VRInteractable : MonoBehaviour {

		private VRHandControllerManager _controllerManager = null;

		public Action onControllerInteracted = null;

		void Awake() 
		{
			_controllerManager = FindObjectOfType<VRHandControllerManager>();
		}

		void OnEnable()
		{
			if (_controllerManager != null)
			{
				_controllerManager.onTriggerClicked += OnControllerInteracted;
			}
		}

		void OnDisable()
		{
			if (_controllerManager != null)
			{
				_controllerManager.onTriggerClicked -= OnControllerInteracted;
			}
		}

		void OnControllerInteracted(Collider clickedCollider)
		{
			if (clickedCollider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
			{
				if (onControllerInteracted != null) 
				{
					onControllerInteracted();
				}
			}
		}
	}
}



