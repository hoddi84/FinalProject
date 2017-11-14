using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLightSpawner : MonoBehaviour {

	public GameObject unitLight;
	private GameObject tmp;

	private GameObject parentOfLights;

	void Awake()
	{
		parentOfLights = GameObject.Find("LIGHTS");
		
		if (parentOfLights == null)
		{
			parentOfLights = new GameObject("LIGHTS");
		}

	}

	void OnEnable()
	{
		tmp = Instantiate(unitLight, transform.position, transform.rotation);
		tmp.transform.parent = parentOfLights.transform;
	}

	void OnDisable()
	{
		if (tmp != null)
		{
			tmp.GetComponent<LightSettings>().DisableLight();
		}
	}
}
