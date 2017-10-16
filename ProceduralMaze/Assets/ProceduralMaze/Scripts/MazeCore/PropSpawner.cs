using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {

	public TestUnit[] unitToSpawnProp;
	public GameObject[] allProps;
	private List<GameObject> activeProps = null;
	private float spawnChance = 1f;

	void Awake()
	{
		Initialize();
	}

	/*
	 * PropManager delegates this event and subscribes it to the UnitManager, in such
	 * a way so that when the UnitTriggers are activated this event gets called.
	 * This event looks in the Path Dictionary from the UnitManager and checks which Unit Types
	 * are active and this activates the props for that Unit Type. 
	 */
	public void CheckForActivePath(Dictionary<TestUnit, GameObject> pathDict)
	{
		foreach (TestUnit type in unitToSpawnProp)
		{
			foreach (KeyValuePair<TestUnit, GameObject> entry in pathDict)
			{
				if (type == entry.Key)
				{
					if (entry.Value.activeInHierarchy)
					{
						//Activate these props.
						EnableProps();
						return;
					}
				}
			}
		}
		// Disable these props.
		DisableProps();
	}

	void DisableProps()
	{
		foreach (GameObject obj in allProps)
		{
			obj.SetActive(false);
		}
	}

	void EnableProps()
	{
		if (activeProps != null)
		{
			foreach (GameObject obj in activeProps)
			{
				obj.SetActive(true);
			}
			return;
		}

		activeProps = new List<GameObject>();

		foreach (GameObject obj in allProps)
		{
			float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

			if (rnd <= spawnChance)
			{
				obj.SetActive(true);
				activeProps.Add(obj);
			}
		}
	}

	void Initialize()
	{
		foreach (GameObject obj in allProps)
		{
			obj.SetActive(false);
		}
	}
}
