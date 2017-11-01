using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	}

	void InitializeSlider()
	{
		slider.value = lightManager.lightIntensity;
		slider.maxValue = lightManager.MAX_LIGHT_INTENSITY;
	}
}
