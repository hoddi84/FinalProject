using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour {

	private UnitManager unitManager;
	public GameObject[] unitA;

	Dictionary<TestUnit, GameObject[]> propsDict = new Dictionary<TestUnit, GameObject[]>();

	void Awake()
	{
		unitManager = GetComponent<UnitManager>();
	}
	
	void InstantiateProps(TestUnit type) 
	{
		switch(type)
		{
			case TestUnit.TypeA:
			break;
		}
	}

	void CheckInstantiatedProps(TestUnit type, GameObject[] propsType, GameObject instantiatedUnit)
	{
		if (propsDict.ContainsKey(type))
		{
			GameObject[] tmp;
			propsDict.TryGetValue(type, out tmp);

			if (!instantiatedUnit.activeInHierarchy)
			{
				foreach (GameObject obj in tmp)
				{
					obj.SetActive(true);
				}
			}
			else 
			{
				foreach (GameObject obj in tmp)
				{
					obj.SetActive(false);
				}
			}
		}
		else
		{
			int rndIndexMin = Random.Range(0, propsType.Length);
			int rndIndexMax = Random.Range(0, propsType.Length);

			int rangeLength = Mathf.Abs(rndIndexMax - rndIndexMin);

			if (rndIndexMin == rndIndexMax)
			{
				rangeLength = 1;
			}
			else if (rndIndexMin > rndIndexMax)
			{
				int t = rndIndexMax;
				rndIndexMax = rndIndexMin;
				rndIndexMin = t;
			}

			GameObject[] tmp = new GameObject[rangeLength];
			int tmpCounter = 0;

			for (int i = rndIndexMin; i < rndIndexMax + 1; i++) 
			{
				tmp[tmpCounter] = propsType[i];
				tmpCounter++;
			}

			UpdatePropsDictionary(type, tmp);
		}
	}

	void UpdatePropsDictionary(TestUnit type, GameObject[] props)
	{
		if (!propsDict.ContainsKey(type))
		{
			propsDict.Add(type, props);
		}
	}
}
