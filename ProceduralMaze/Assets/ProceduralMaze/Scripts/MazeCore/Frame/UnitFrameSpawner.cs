using System.Collections.Generic;
using UnityEngine;
using MazeUtiliy;

namespace MazeCore.Frame {

	public class UnitFrameSpawner : MonoBehaviour {

		public GameObject framePrefab;

		private UnitFrameManager _frameManager = null;
		private Transform[] _frameSpawnPoints = null;
		private List<GameObject> _listOfSpawned = null;
		private List<Transform> _listOfSpawnPoints = null;
		private GameObject _parentOfFrames = null;
		private float _spawnChance = 0;

		private void Awake()
		{
			_parentOfFrames = GameObject.Find("FRAMES");

			if (_parentOfFrames == null)
			{
				_parentOfFrames = new GameObject("FRAMES");
			}

			_frameManager = FindObjectOfType<UnitFrameManager>();
			_frameSpawnPoints = gameObject.GetComponentsInChildren<Transform>();
		}

		private void OnEnable()
		{
			if (_frameManager != null) { _spawnChance = _frameManager.frameSpawnChance; }

			ToggleSpawnedPhotoFrames(true);
		}

		private void OnDisable()
		{
			ToggleSpawnedPhotoFrames(false);
		}

		private void Start()
		{
			_listOfSpawnPoints = new List<Transform>();

			foreach (Transform transform in _frameSpawnPoints)
			{
				_listOfSpawnPoints.Add(transform);
			}

			_listOfSpawnPoints = UtilityTools.RandomizeList(_listOfSpawnPoints);

			if (_frameManager.availablePhotos.Count != 0) { Initialize(); }
		}

		/// <summary>
		/// Toggle the photo frame gameObjects.
		/// </summary>
		/// <param name="active">Photo frame stata.</param>
		private void ToggleSpawnedPhotoFrames(bool active)
		{
			if (_listOfSpawned != null)
			{
				foreach (GameObject obj in _listOfSpawned)
				{
					if (obj != null)
					{
						if (active) { obj.SetActive(true); }
						else { obj.SetActive(false); }
					}
				}
			}
		}

		/// <summary>
		/// Initializes a Maze Unit with photo frames.
		/// </summary>
		private void Initialize()
		{
			_listOfSpawned = new List<GameObject>();

			foreach (Transform spawn in _listOfSpawnPoints)
			{
				if (spawn.gameObject.GetInstanceID() != gameObject.GetInstanceID())
				{
					float rnd = Random.Range(0.0f, 1.0f);

					// spawn
					if (rnd <= _spawnChance)
					{
						if (_listOfSpawned.Count == 0)
						{
							GameObject t = Instantiate(framePrefab, spawn.transform.position, spawn.rotation);

							AddRotation(t, _frameManager.GetScaryMeterValue());
							AssignPhotoToFrame(t);
							t.transform.parent = _parentOfFrames.transform;
							_listOfSpawned.Add(t);
						}
						else
						{
							GameObject t = Instantiate(framePrefab, spawn.transform.position, spawn.rotation);

							AddRotation(t, _frameManager.GetScaryMeterValue());
							AssignPhotoToFrame(t);
							t.transform.parent = _parentOfFrames.transform;
							if (CheckIfCollision(_listOfSpawned, t))
							{
								Destroy(t);
							}
							else
							{
								_listOfSpawned.Add(t);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Add rotation to a frame.
		/// </summary>
		/// <param name="frame">The frame to rotate.</param>
		/// <param name="scaryMeterValue">Amount of rotation.</param>
		private void AddRotation(GameObject frame, float scaryMeterValue)
		{
			UnitFrameSpawnerRotation frameToRotate = frame.GetComponentInChildren<UnitFrameSpawnerRotation>();
			BoxCollider collider = frame.GetComponentInChildren<BoxCollider>();

			Vector3 center = collider.bounds.center;
			Transform rotatingFrame = frameToRotate.gameObject.transform;
			Transform parentRotation = rotatingFrame.parent.transform;

			float yExtents = collider.bounds.extents.y;
			float zExtents = collider.bounds.extents.z;

			Vector3 pointEndZ = new Vector3(center.x, center.y, center.z + zExtents);
			Vector3 pointEndY = new Vector3(center.x, center.y + yExtents, center.z);

			Vector3 dir = pointEndZ - center;
			dir = Quaternion.Euler(0,parentRotation.rotation.eulerAngles.y, 0) * dir;
			pointEndZ = dir + center;

			Vector3 heightLine = pointEndY - center;
			Vector3 widthLine = pointEndZ - center;

			Vector3 rotationAxis = Vector3.Cross(heightLine, widthLine);

			float randomRotation = Random.Range(-_frameManager.maxFrameRotation, _frameManager.maxFrameRotation);
			rotatingFrame.RotateAround(center, rotationAxis, randomRotation*scaryMeterValue);
		}

		/// <summary>
		/// Assign a random photo to a frame from available photos.
		/// </summary>
		/// <param name="t">The frame to add a photo to.</param>
		private void AssignPhotoToFrame(GameObject t)
		{
			int rnd = Random.Range(0, _frameManager.availablePhotos.Count);

			t.GetComponentInChildren<SpriteRenderer>().sprite = _frameManager.availablePhotos[rnd];
		}

		/// <summary>
		/// Check if a new photo frame collides with existing frames.
		/// </summary>
		/// <param name="listOfSpawned">List of frames that have been spawned.</param>
		/// <param name="newSpawn">Frame to spawn.</param>
		/// <returns>Returns true if new frame to spawn collides with existing frames.</returns>
		private bool CheckIfCollision(List<GameObject> listOfSpawned, GameObject newSpawn)
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
}


