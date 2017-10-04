using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpawner : MonoBehaviour {

	public GameObject[] lights;
	public GameObject lightPrefab;

	private UnitManager unitManager;

	private Dictionary<TestUnit, GameObject> lightDict = new Dictionary<TestUnit, GameObject>();

	void Awake()
	{
		unitManager = FindObjectOfType(typeof(UnitManager)) as UnitManager;
		unitManager.onPathDictUpdate += CheckForSpawnableLights;
	}

	void CheckForSpawnableLights(Dictionary<TestUnit, GameObject> pathDict)
	{
		foreach (KeyValuePair<TestUnit, GameObject> entry in pathDict)
		{
			foreach (GameObject light in lights)
			{
				foreach (TestUnit unit in light.GetComponent<LightSpawnType>().lightSpawnTypes)
				{
					if (unit == entry.Key)
					{
						if (entry.Value.activeInHierarchy)
						{
							if (!lightDict.ContainsKey(unit))
							{
								GameObject t = Instantiate(lightPrefab, light.transform.position, light.transform.rotation);
								lightDict.Add(unit, t);
							}
							else
							{
								GameObject t;
								lightDict.TryGetValue(unit, out t);
								t.SetActive(true);
							}
							
						}
						else
						{
							GameObject t;
							lightDict.TryGetValue(unit, out t);
							t.SetActive(false);
						}
					}
				}
			}
		}
	}
}
