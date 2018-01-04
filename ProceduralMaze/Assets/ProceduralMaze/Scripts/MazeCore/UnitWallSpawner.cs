using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWallSpawner : MonoBehaviour {

	private UnitWallTypes wallTypes;

	private enum WallType { Large, Medium, Small, Smallest, VerySmall, Corner};

	void Awake() 
	{
		wallTypes = FindObjectOfType(typeof(UnitWallTypes)) as UnitWallTypes;
	}

	void Start()
	{
		Setup();
	}

	void Setup()
	{
		Dictionary<GameObject, WallType> contents = new Dictionary<GameObject, WallType>();

		foreach (Transform wall in transform) {
			
			if (wall.name.Contains("WallLarge")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.Large);
			}
			else if (wall.name.Contains("WallMedium")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.Medium);
			}
			else if (wall.name.Contains("WallSmallest")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.Smallest);
			}
			else if (wall.name.Contains("WallSmall")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.Small);
			}
			else if (wall.name.Contains("WallVerySmall")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.VerySmall);
			}
			else if (wall.name.Contains("Corner")) {
				GameObject t = wall.gameObject;
				contents.Add(t, WallType.Corner);
			}
		}

		foreach (KeyValuePair<GameObject, WallType> entry in contents)
		{
			GameObject tmp = null;

			switch (entry.Value) {
				case WallType.Large:
					tmp = wallTypes.wallLarge;
				break;

				case WallType.Medium:
					tmp = wallTypes.wallMedium;
				break;

				case WallType.Small:
					tmp = wallTypes.wallSmall;
				break;

				case WallType.Smallest:
					tmp = wallTypes.wallSmallest;
				break;

				case WallType.VerySmall:
					tmp = wallTypes.wallVerySmall;
				break;

				case WallType.Corner:
					tmp = wallTypes.wallCorner;
				break;
			}

			if (tmp != null)
			{
				GameObject t = Instantiate(tmp, entry.Key.transform.position, entry.Key.transform.rotation);
				t.transform.parent = transform;
				Destroy(entry.Key);
			}
		}
	}
}
