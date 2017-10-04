using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSettings : MonoBehaviour {

	private LightManager lightManager;
	private Light light;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;	
		light = GetComponentInChildren<Light>();	

		lightManager.onLightColorChanged += SetLightColor;		
		lightManager.onLightIntensityChanged += SetLightIntensity;
	}

	void SetLightIntensity(float newIntensity)
	{
		light.intensity = newIntensity;
	}

	void SetLightColor(Color newColor)
	{
		light.color = newColor;
	}

	IEnumerator LightingIntro(bool intro)
	{
		float duration = 3;
		float elapsedTime = 0;

		light.color = lightManager.lightColor;

		while (elapsedTime < duration)
		{
			if (intro)
			{
				light.intensity = Mathf.Lerp(0, lightManager.lightIntensity, (elapsedTime/duration));
			}
			else 
			{
				light.intensity = Mathf.Lerp(lightManager.lightIntensity, 0, (elapsedTime/duration));
			}
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	void OnEnable()
	{
		StartCoroutine(LightingIntro(true));
	}
}
