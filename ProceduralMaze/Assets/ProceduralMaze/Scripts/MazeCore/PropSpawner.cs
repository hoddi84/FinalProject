using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {

	public TestUnit[] unitToSpawnProp;
	public GameObject[] allProps;
	private GameObject[] activeProps;

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
						foreach (GameObject obj in allProps)
						{
							if (obj != null)
							{
								obj.SetActive(true);
							}
						}
						return;
					}
				}
			}
		}
		foreach (GameObject obj in activeProps)
		{
			if (obj != null)
			{
				obj.SetActive(false);
			}
		}
	}

	/*
	 * Choose a random range from the allProps array of prop gameobjects.
	 * This random range of gameobjects are the props which get to be shown
	 * in the scene.
	 */
	void Initialize()
	{
		foreach (GameObject obj in allProps)
		{
			obj.SetActive(false);
		}

		int rndIndexMin = UnityEngine.Random.Range(0, allProps.Length);
		int rndIndexMax = UnityEngine.Random.Range(0, allProps.Length);

		int rangeLength;

		if (rndIndexMax == rndIndexMin)
		{
			activeProps = new GameObject[1];
			activeProps[0] = allProps[rndIndexMax];
			return;
		}
		else
		{
			rangeLength = Mathf.Abs(rndIndexMax - rndIndexMin);
			activeProps = new GameObject[rangeLength + 1];

			if (rndIndexMin > rndIndexMax) 
			{
				rndIndexMin = rndIndexMax;
			}

			int counter = 0;

			for (int i = rndIndexMin; i < activeProps.Length; i++)
			{
				activeProps[counter] = allProps[i];
				counter++;
			}
		}
	}
}
