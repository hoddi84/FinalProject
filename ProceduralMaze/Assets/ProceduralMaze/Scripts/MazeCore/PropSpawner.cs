using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour {

	public TestUnit[] propsAtUnit;

	public void CheckForActivePath(Dictionary<TestUnit, GameObject> pathDict)
	{
		foreach (TestUnit type in propsAtUnit)
		{
			foreach (KeyValuePair<TestUnit, GameObject> entry in pathDict)
			{
				if (type == entry.Key)
				{
					if (entry.Value.activeInHierarchy)
					{
						gameObject.SetActive(true);
						return;
					}
				}
			}
		}
		gameObject.SetActive(false);	
	}
}
