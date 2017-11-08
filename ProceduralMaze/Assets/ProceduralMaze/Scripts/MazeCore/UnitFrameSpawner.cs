﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFrameSpawner : MonoBehaviour {

	public GameObject framePrefab;
	private Transform[] frameSpawnPoints;
	private UnitFrameManager frameManager;
	private float spawnChance;
	private List<GameObject> listOfSpawned;
	private List<Transform> listOfSpawnPoints;

	void OnEnable()
	{
		if (frameManager == null)
		{
			frameManager = FindObjectOfType(typeof(UnitFrameManager)) as UnitFrameManager;
			spawnChance = frameManager.frameSpawnChance;
		}

		if (frameSpawnPoints == null)
		{
			frameSpawnPoints = gameObject.GetComponentsInChildren<Transform>();
		}
	}

	void Start()
	{
		listOfSpawnPoints = new List<Transform>();

		foreach (Transform transform in frameSpawnPoints)
		{
			listOfSpawnPoints.Add(transform);
		}

		listOfSpawnPoints = RandomizeSpawnList(listOfSpawnPoints);

		Initialize();
	}

	void OnDisable()
	{
		if (listOfSpawned.Count != 0)
		{
			foreach (GameObject obj in listOfSpawned)
			{
				Destroy(obj);
			}
		}
	}

	private List<T> RandomizeSpawnList<T>(List<T> someList)
	{
		List<T> randomized = new List<T>();
		List<T> original = new List<T>(someList);

		while (original.Count > 0)
		{
			int index = Random.Range(0, original.Count);
			randomized.Add(original[index]);
			original.RemoveAt(index);
		}
		return randomized;
	}

	void Initialize()
	{
		listOfSpawned = new List<GameObject>();

		foreach (Transform spawn in listOfSpawnPoints)
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
