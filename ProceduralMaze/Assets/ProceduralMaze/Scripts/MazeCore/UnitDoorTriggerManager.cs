using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorTriggerManager : MonoBehaviour {

	public Action onDoorTriggerEntered = null;
	private BoxCollider activeCollider = null;
	private UnitDoor unitDoor;

	void Awake()
	{
		unitDoor = transform.parent.gameObject.GetComponentInChildren<UnitDoor>();
	}

	void OnEnable()
	{
		RegisterCollider();
	}

	void OnDisable()
	{
		DeregisterCollider();
	}

	void RegisterCollider()
	{
		BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>(true);

		int rnd = UnityEngine.Random.Range(0, colliders.Length);
		print(rnd);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (i != rnd)
			{
				colliders[i].gameObject.SetActive(false);
				print("Disabling: " + colliders[i].name);
			}
			else
			{
				colliders[i].gameObject.SetActive(true);
				print("Activating: " + colliders[i].name);
			}
		}

		activeCollider = colliders[rnd];
		activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered += unitDoor.PerformScaryMeterActions;
	}

	void DeregisterCollider()
	{
		activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered -= unitDoor.PerformScaryMeterActions;
		activeCollider = null;
	}
}
