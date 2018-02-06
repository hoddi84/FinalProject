using System.Collections.Generic;
using UnityEngine;

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
				listOfDecals = RandomizeSpawnList(listOfDecals);

				for (int i = 0; i < decalAmount; i++)
				{
					listOfDecals[i].SetActive(true);
				}
			}
		}

		/// <summary>
		/// Randomize elements in a list.
		/// </summary>
		/// <param name="someList">List to be randomized.</param>
		/// <returns>Randomized List.</returns>
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
	}
}
