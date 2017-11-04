using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFrameSpawner : MonoBehaviour {

	public GameObject framePrefab;
	private Transform[] frameSpawnPoints;
	private UnitFrameManager frameManager;
	private float spawnChance;

	void OnEnable()
	{
		
	}

	void OnDisable()
	{

	}

	void Start()
	{
		frameManager = FindObjectOfType(typeof(UnitFrameManager)) as UnitFrameManager;
		spawnChance = frameManager.frameSpawnChance;

		frameSpawnPoints = gameObject.GetComponentsInChildren<Transform>();

		Initialize();
	}

	void Initialize()
	{
		List<GameObject> listOfSpawned = new List<GameObject>();

		foreach (Transform spawn in frameSpawnPoints)
		{
			if (spawn.gameObject.GetInstanceID() != gameObject.GetInstanceID())
			{
				float rnd = Random.Range(0.0f, 1.0f);

				// spawn
				if (rnd <= spawnChance)
				{
					if (listOfSpawned.Count == 0)
					{
						GameObject t = Instantiate(framePrefab, spawn.transform.position, spawn.rotation);
						listOfSpawned.Add(t);
					}
					else
					{
						GameObject t = Instantiate(framePrefab, spawn.transform.position, spawn.rotation);
						if (CheckIfCollision(listOfSpawned, t))
						{
							Destroy(t);
						}
						else
						{
							listOfSpawned.Add(t);
						}
					}
				}
			}
		}
	}

	bool CheckIfCollision(List<GameObject> listOfSpawned, GameObject newSpawn)
	{
		foreach (GameObject spawned in listOfSpawned)
		{
			BoxCollider spawnedCollider = spawned.GetComponentInChildren<BoxCollider>();
			BoxCollider newSpawnCollider = newSpawn.GetComponentInChildren<BoxCollider>();

			if (spawnedCollider.bounds.Intersects(newSpawnCollider.bounds))
			{
				return true;
			}
		}
		return false;
	}
}
