using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexInterfaceController : MonoBehaviour {

	public Slider lightIntensitySlider;
	public Slider lightAmbienceSlider;

	private LightManager lightManager;

	public Action<float> onLightIntensityChanged = null;
	public Action<float> onAmbienceIntensityChanged = null;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
		lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbienceIntensity);
	}

	void UpdateLightIntensity(float newLightIntensity)
	{
		if (onLightIntensityChanged != null)
		{
			onLightIntensityChanged(newLightIntensity);
		}
	}

	void UpdateAmbienceIntensity(float newAmbienceIntensity)
	{
		if (onAmbienceIntensityChanged != null)
		{
			onAmbienceIntensityChanged(newAmbienceIntensity);
		}
	}
}
