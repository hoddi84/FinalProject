using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorSpawner : MonoBehaviour {

	public GameObject unitDoorPrefab;
	private Transform[] doorSpawnPoints;

	void Awake()
	{
		doorSpawnPoints = gameObject.GetComponentsInChildren<Transform>();

		SpawnDoors();
	}

	void SpawnDoors()
	{
		foreach (Transform spawnPoint in doorSpawnPoints)
		{
			if (spawnPoint.gameObject.GetInstanceID() != gameObject.GetInstanceID())
			{
				GameObject tmp = Instantiate(unitDoorPrefab, spawnPoint.position, spawnPoint.rotation);
				tmp.transform.parent = this.transform;
			}
		}
	}
}
