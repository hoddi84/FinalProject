using UnityEngine;

namespace MazeCore.Door {

	public class UnitDoorSpawner : MonoBehaviour {

		private Transform[] _doorSpawnPoints;
		
		public GameObject unitDoorPrefab;

		private void Awake()
		{
			_doorSpawnPoints = gameObject.GetComponentsInChildren<Transform>();
		}

		private void Start()
		{
			SpawnDoors();
		}

		private void SpawnDoors()
		{
			foreach (Transform spawnPoint in _doorSpawnPoints)
			{
				if (spawnPoint.gameObject.GetInstanceID() != gameObject.GetInstanceID())
				{
					GameObject tmp = Instantiate(unitDoorPrefab, spawnPoint.position, spawnPoint.rotation);
					tmp.transform.parent = transform;
				}
			}
		}
	}
}
