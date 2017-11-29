﻿using System;
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

	void Initialize()
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

		activeCollider = colliders[index];
	}

	void RegisterCollider()
	{
		Initialize();
		activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered += OnDoorTriggerEnter;
	}

	void DeregisterCollider()
	{
		activeCollider.gameObject.GetComponent<UnitDoorTrigger>().onTriggerEntered -= OnDoorTriggerEnter;
		activeCollider = null;
	}

	void OnDoorTriggerEnter()
	{
		unitDoor.PerformScaryMeterActions(delegate() {
			DeregisterCollider();
			RegisterCollider();
		});
	}
}
