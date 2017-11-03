using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLightSpawner : MonoBehaviour {

	public GameObject unitLight;

	private GameObject tmp;
	// Use this for initialization
	void OnEnable()
	{
		tmp = Instantiate(unitLight, transform.position, transform.rotation);
	}

	void OnDisable()
	{
		tmp.GetComponent<LightSettings>().DisableLight();
	}
}
