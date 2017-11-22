using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorSpawner : MonoBehaviour {

	public GameObject unitDoorPrefab;
	private Transform[] doorSpawnPoints;
	private List<GameObject> spawnedDoors;

	void Awake()
	{
		doorSpawnPoints = gameObject.GetComponentsInChildren<Transform>();
		//spawnedDoors = new List<GameObject>();
	}

	void OnEnable()
	{
		spawnedDoors = new List<GameObject>();	
		
		SpawnDoors();
	}

	void OnDisable()
	{
		DestroySpawnedDoors();
	}

	void SpawnDoors()
	{
		foreach (Transform spawnPoint in doorSpawnPoints)
		{
			if (spawnPoint.gameObject.GetInstanceID() != gameObject.GetInstanceID())
			{
				GameObject tmp = Instantiate(unitDoorPrefab, spawnPoint.position, spawnPoint.rotation);
				spawnedDoors.Add(tmp);
			}
		}
	}

	void DestroySpawnedDoors()
	{
		foreach (GameObject door in spawnedDoors)
		{
			Destroy(door);
		}
		spawnedDoors.Clear();
	}
}
