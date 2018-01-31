using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	private Color currentLightColor;
	private Color currentAmbienceColor;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{

		}
	}

	IEnumerator FlickerLights()
	{
		yield return null;
	}

	void TurnOffAllLights(Light[] lights, float duration)
	{
		foreach (Light light in lights)
		{
			
		}
	}

	void TurnOnAllLights(Light[] lights, float duration)
	{

	}
}
