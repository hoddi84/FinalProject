using System.Collections.Generic;
using UnityEngine;
using MazeUtiliy;

namespace MazeCore.Decals {

	public class DecalSpawner : MonoBehaviour {

		private DecalManager _decalManager;

		public GameObject[] bloodDecals;

		private void Awake()
		{
			_decalManager = FindObjectOfType(typeof(DecalManager)) as DecalManager;
		}

		private void Start()
		{
			if (_decalManager != null)
			{
				SpawnDecals(_decalManager.bloodDecalAmount);
			}
		}

		/// <summary>
		/// Spawn decals when a Unit is instantiated, amount of decals depends
		/// on scary meter value in simple mode or decal value in complex mode.
		/// </summary>
		/// <param name="value">Decal amount value.</param>
		private void SpawnDecals(float value)
		{
			if (value >= 0)
			{
				int decalAmount = (int)(bloodDecals.Length/2 * value);

				List<GameObject> listOfDecals = new List<GameObject>(bloodDecals);
				listOfDecals = UtilityTools.RandomizeList(listOfDecals);

				for (int i = 0; i < decalAmount; i++)
				{
					listOfDecals[i].SetActive(true);
				}
			}
		}
	}
}
