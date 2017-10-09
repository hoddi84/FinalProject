using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour {

	public GameObject[] lights;
	public GameObject lightPrefab;

	public bool spawnLights = false;

	private UnitManager unitManager;

	private Dictionary<TestUnit, GameObject> lightDict = new Dictionary<TestUnit, GameObject>();

	void Awake()
	{
		if (spawnLights)
		{
			unitManager = FindObjectOfType(typeof(UnitManager)) as UnitManager;
			unitManager.onPathDictUpdate += CheckForSpawnableLights;
		}
	}

	void CheckForSpawnableLights(Dictionary<TestUnit, GameObject> pathDict)
	{
		List<TestUnit> activeUnits = new List<TestUnit>();

		// Find active Units.
		foreach (KeyValuePair<TestUnit, GameObject> entry in pathDict)
		{
			if (entry.Value.activeInHierarchy)
			{
				activeUnits.Add(entry.Key);
			}
		}

		foreach (GameObject light in lights)
		{
			if (CheckIfActiveLight(light, activeUnits))
			{
				light.SetActive(true);
			}
			else
			{
				if (light.activeInHierarchy)
				{
					light.GetComponentInChildren<LightSettings>().DisableLight();
				}
			}
		}
	}

	bool CheckIfActiveLight(GameObject light, List<TestUnit> activeUnits)
	{
		foreach (TestUnit unit in light.GetComponent<LightSpawnType>().lightSpawnTypes)
		{
			foreach (TestUnit activeUnit in activeUnits)
			{
				if (unit == activeUnit)
				{
					return true;
				}
			}
		}
		return false;
	}
}
