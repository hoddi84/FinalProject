using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeCore.Door {

	/// <summary>
	/// The only purpose of this script is to attach to doors,
	/// which we want to be visible in the editor and prefabs views
	/// but when we play we want them deactivated since we instantiate
	/// the door prefab.
	/// </summary>

	public class UnitDoorDisabler : MonoBehaviour {

		private void Awake()
		{
			gameObject.SetActive(false);
		}
	}
}

