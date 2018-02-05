using System;
using UnityEngine;

namespace MazeCore.Door {

	public class UnitDoorTriggerManager : MonoBehaviour {

		private BoxCollider _activeCollider = null;
		private UnitDoor _unitDoor = null;

		public Action onDoorTriggerEntered = null;

		private void Awake()
		{
			_unitDoor = transform.parent.gameObject.GetComponentInChildren<UnitDoor>();
		}

		private void OnEnable()
		{
			RegisterCollider();
		}

		private void OnDisable()
		{
			DeregisterCollider();
		}

		private void Initialize()
		{
			BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>(true);

			int index = UnityEngine.Random.Range(0, colliders.Length);

			for (int i = 0; i < colliders.Length; i++)
			{
				if (i != index)
				{
					colliders[i].gameObject.SetActive(false);
				}
				else
				{
					colliders[i].gameObject.SetActive(true);
				}
			}

			_activeCollider = colliders[index];
		}

		private void RegisterCollider()
		{
			Initialize();
			_activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered += OnDoorTriggerEnter;
		}

		private void DeregisterCollider()
		{
			_activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered -= OnDoorTriggerEnter;
			_activeCollider = null;
		}

		private void OnDoorTriggerEnter()
		{
			_unitDoor.PerformScaryMeterActions(delegate() {
				DeregisterCollider();
				RegisterCollider();
			});
		}
	}
}

