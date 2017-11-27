using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexInterfaceController : MonoBehaviour {

	public Slider lightIntensitySlider;
	public Slider lightAmbienceSlider;

	private LightManager lightManager;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
		lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbience);
	}

	void UpdateLightIntensity(float value)
	{

	}

	void UpdateAmbience(float value)
	{

	}
}
