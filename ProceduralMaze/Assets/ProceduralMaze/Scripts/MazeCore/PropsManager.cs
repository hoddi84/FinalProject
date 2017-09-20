using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour {

	public GameObject[] unitA_props;
	Dictionary<TestUnit, GameObject[]> propsDict = new Dictionary<TestUnit, GameObject[]>();

	private UnitManager unitManager;
	void Awake()
	{
		unitManager = GetComponent<UnitManager>();

		unitManager.onPathDictUpdate += CheckForActivePaths;
	}

	void Start()
	{
		AddPropsToDictionary(TestUnit.TypeA, unitA_props);
	}

	void CheckForActivePaths(Dictionary<TestUnit, GameObject> pathDict)
	{
		foreach (KeyValuePair<TestUnit, GameObject> entry in pathDict)
		{
			GameObject[] tmp;
			propsDict.TryGetValue(entry.Key, out tmp);

			if (!entry.Value.activeInHierarchy)
			{
				if (tmp != null)
				{
					foreach (GameObject obj in tmp)
					{
						obj.SetActive(false);
					}
				}
			}
			else 
			{
				if (tmp != null)
				{
					foreach (GameObject obj in tmp)
					{
						obj.SetActive(true);
					}
				}
			}
		}
	}

	void AddPropsToDictionary(TestUnit type, GameObject[] props)
	{
		GameObject PROPS;

		List<GameObject> tmpList = new List<GameObject>();
		GameObject[] tmp = null;

		if (!GameObject.Find("PROPS")) 
		{
			PROPS = new GameObject();
			PROPS.name = "PROPS";
		}
		else 
		{
			PROPS = GameObject.Find("PROPS");
		}
		
		foreach (GameObject obj in props)
		{
			print("instantiated");
			GameObject t = Instantiate(obj);
			t.transform.parent = PROPS.transform;
			tmpList.Add(t);
		}

		tmp = new GameObject[tmpList.Count];

		for (int i = 0; i < tmp.Length; i++)
		{
			tmp[i] = tmpList[i];
		}

		propsDict.Add(type, tmp);
	}

}
