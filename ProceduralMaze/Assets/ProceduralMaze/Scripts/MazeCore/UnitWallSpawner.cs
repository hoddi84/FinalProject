using System.Collections.Generic;
using UnityEngine;

namespace MazeCore {

	public class UnitWallSpawner : MonoBehaviour {

		private UnitWallTypes _wallTypes;
		private const string WALL_LARGE = "WallLarge";
		private const string WALL_MEDIUM = "WallMedium";
		private const string WALL_SMALL = "WallSmall";
		private const string WALL_VERYSMALL = "WallVerySmall";
		private const string WALL_SMALLEST = "WallSmallest"; 
		private const string WALL_CORNER = "Corner";

		private enum WallType { Large, Medium, Small, Smallest, VerySmall, Corner, NULL};

		private void Awake() 
		{
			_wallTypes = FindObjectOfType<UnitWallTypes>();
		}

		private void Start()
		{
			InstantiateWalls();
		}

		/// <summary>
		/// Replaces existing walls in an instantiated Unit prefab with it's corresponding
		/// wall prefab.
		/// </summary>
		private void InstantiateWalls()
		{
			Dictionary<GameObject, WallType> contents = new Dictionary<GameObject, WallType>();
			WallType wallType = WallType.NULL;

			foreach (Transform wall in transform) {
	
				if (wall.name.Contains(WALL_LARGE)) {
					wallType = WallType.Large;
				}
				else if (wall.name.Contains(WALL_MEDIUM)) {
					wallType = WallType.Medium;
				}
				else if (wall.name.Contains(WALL_SMALLEST)) {
					wallType = WallType.Smallest;
				}
				else if (wall.name.Contains(WALL_SMALL)) {
					wallType = WallType.Small;
				}
				else if (wall.name.Contains(WALL_VERYSMALL)) {
					wallType = WallType.VerySmall;
				}
				else if (wall.name.Contains(WALL_CORNER)) {
					wallType = WallType.Corner;
				}

				if (wallType != WallType.NULL)
				{
					GameObject t = wall.gameObject;
					contents.Add(t, wallType);
				}
			}

			foreach (KeyValuePair<GameObject, WallType> entry in contents)
			{
				GameObject tmp = null;

				switch (entry.Value) {
					case WallType.Large:
						tmp = _wallTypes.wallLarge;
					break;

					case WallType.Medium:
						tmp = _wallTypes.wallMedium;
					break;

					case WallType.Small:
						tmp = _wallTypes.wallSmall;
					break;

					case WallType.Smallest:
						tmp = _wallTypes.wallSmallest;
					break;

					case WallType.VerySmall:
						tmp = _wallTypes.wallVerySmall;
					break;

					case WallType.Corner:
						tmp = _wallTypes.wallCorner;
					break;
				}

				if (tmp != null)
				{
					GameObject t = Instantiate(tmp, entry.Key.transform.position, entry.Key.transform.rotation);
					t.transform.parent = transform;
					Destroy(entry.Key);
				}
			}
			contents.Clear();
		}
	}
}


