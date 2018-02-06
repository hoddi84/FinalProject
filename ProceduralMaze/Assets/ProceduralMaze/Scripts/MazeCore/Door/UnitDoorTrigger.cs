using System;
using UnityEngine;

namespace MazeCore.Door {

	public class UnitDoorTrigger : MonoBehaviour {

		private const string PLAYER = "Player";
		
		public Action onTriggerEntered = null;
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == PLAYER)
			{
				if (onTriggerEntered != null)
				{
					onTriggerEntered();
				}
			}
		}
	}
}

