using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSpawner : MonoBehaviour {

	public GameObject[] bloodDecals;

	private DecalManager decalManager;

	void Awake()
	{
		decalManager = FindObjectOfType(typeof(DecalManager)) as DecalManager;
	}

	void Start()
	{
		SpawnDecals(decalManager.bloodDecal);
	}

	void SpawnDecals(float value)
	{
		if (value >= .7)
		{
			int nrOfDecals = (int)(bloodDecals.Length * value / 2);

			List<GameObject> listOfDecals = new List<GameObject>(bloodDecals);
			listOfDecals = RandomizeSpawnList(listOfDecals);

			for (int i = 0; i < nrOfDecals; i++)
			{
				listOfDecals[i].SetActive(true);
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
}
