using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

	private Color currentLightColor;
	private Color currentAmbienceColor;

	void Update()
	{
		currentAmbienceColor = RenderSettings.ambientLight;
	}

	public IEnumerator FlickerLights(Action action)
	{
		List<float> intensities;
		Light[] lights = GetActiveLights(out intensities);

		Color tmp = currentAmbienceColor;

		TurnOffAllLights(lights);
		RenderSettings.ambientLight = Color.black;

		if (action != null)
		{
			action();
		}

		yield return new WaitForSeconds(.5f);

		TurnOnAllLights(lights, intensities);
		RenderSettings.ambientLight = tmp;
		

		yield return null;
	}

	void TurnOffAllLights(Light[] lights)
	{
		foreach (Light light in lights)
		{
			light.intensity = 0;
		}
	}

	void TurnOnAllLights(Light[] lights, List<float> intensities)
	{
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i].intensity = intensities[i];
		}
	}

	Light[] GetActiveLights(out List<float> intensities)
	{
		Light[] lights = FindObjectsOfType<Light>();
		intensities = new List<float>();

		foreach (Light light in lights)
		{
			intensities.Add(light.intensity);
		}

		return lights;
	}
}
