using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFrameSpawner : MonoBehaviour {

	public GameObject framePrefab;
	private Transform[] frameSpawnPoints;
	private UnitFrameManager frameManager;
	private float spawnChance;
	private List<GameObject> listOfSpawned;
	private List<Transform> listOfSpawnPoints;
	private GameObject parentOfFrames;

	void Awake()
	{
		parentOfFrames = GameObject.Find("FRAMES");

		if (parentOfFrames == null)
		{
			parentOfFrames = new GameObject("FRAMES");
		}
	}

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

		if (listOfSpawned != null)
		{
			foreach (GameObject obj in listOfSpawned)
			{
				obj.SetActive(true);
			}
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
		if (listOfSpawned != null)
		{
			foreach (GameObject obj in listOfSpawned)
			{
				if (obj != null)
				{
					obj.SetActive(false);
				}
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

						AddVariationToFrame(t, frameManager.GetScaryMeterValue());

						AssignPhotoToFrame(t);
						t.transform.parent = parentOfFrames.transform;
						listOfSpawned.Add(t);
					}
					else
					{
						GameObject t = Instantiate(framePrefab, spawn.transform.position, spawn.rotation);

						AddVariationToFrame(t, frameManager.GetScaryMeterValue());

						AssignPhotoToFrame(t);
						t.transform.parent = parentOfFrames.transform;
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

	// Add a variation to the frames depending on scary meter.
	// DOING
	// Need to check which axes to rotate about, X or Z axis.
	void AddVariationToFrame(GameObject frame, float scaryMeterValue)
	{
		UnitFrameSpawnerRotation frameRotation = frame.GetComponentInChildren<UnitFrameSpawnerRotation>();
		BoxCollider collider = frame.GetComponentInChildren<BoxCollider>();

		Vector3 colliderCenter = collider.bounds.center;
		Transform rotatingFrame = frameRotation.gameObject.transform;

		Vector3 widthAlign;
		Vector3 heightAlign;

		float xExtents = collider.bounds.extents.x;
		float yExtents = collider.bounds.extents.y;
		float zExtents = collider.bounds.extents.z;

		if (xExtents > zExtents)
		{
			widthAlign = new Vector3(colliderCenter.x + xExtents, colliderCenter.y, colliderCenter.z);
		}
		else
		{
			widthAlign = new Vector3(colliderCenter.x, colliderCenter.y, colliderCenter.z + zExtents);
		}

		heightAlign = new Vector3(colliderCenter.x, colliderCenter.y + yExtents, colliderCenter.z);

		Vector3 widthLine = widthAlign - colliderCenter;
		Vector3 heightLine = heightAlign - colliderCenter;
		Vector3 rotationPoint = Vector3.Cross(widthLine, heightLine);
		rotationPoint = new Vector3(rotationPoint.x, 0, rotationPoint.z);

		float randomRotation = Random.Range(-frameManager.maxFrameRotation, frameManager.maxFrameRotation);

		rotatingFrame.RotateAround(colliderCenter, rotationPoint, randomRotation*scaryMeterValue);
	}


	// Assign random picture from manager.
	void AssignPhotoToFrame(GameObject t)
	{
		int rnd = Random.Range(0, frameManager.availablePhotos.Count);

		t.GetComponentInChildren<SpriteRenderer>().sprite = frameManager.availablePhotos[rnd];
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
