using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLightSpawner : MonoBehaviour {

	public GameObject unitLight;
	private GameObject tmp;

	void OnEnable()
	{
		tmp = Instantiate(unitLight, transform.position, transform.rotation);
	}

	void OnDisable()
	{
		if (tmp != null)
		{
			tmp.GetComponent<LightSettings>().DisableLight();
		}
	}
}
