﻿using UnityEngine;
using UnityEngine.UI;

public class SimpleInterfaceController : MonoBehaviour {

	public Slider slider;
	private LightManager lightManager;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		slider.onValueChanged.AddListener(UpdateLightIntensity);
	}

	void Start()
	{
		InitializeSlider();
	}

	void UpdateLightIntensity(float value)
	{
		lightManager.lightIntensity = value;
		lightManager.ambienceIntensity = value;
	}

	void InitializeSlider()
	{
		slider.minValue = 0;
		slider.value = lightManager.lightIntensity;
		slider.maxValue = lightManager.MAX_LIGHT_INTENSITY;
	}
}
