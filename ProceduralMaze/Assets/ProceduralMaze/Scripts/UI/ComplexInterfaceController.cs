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

	public GameObject lightingPanel;
	public GameObject soundPanel;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
		lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbienceIntensity);
	}

	void Start()
	{
		Initialize();
	}

	void Initialize()
	{
		lightingPanel.SetActive(true);
		soundPanel.SetActive(false);
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

	public void EnableSoundPanel()
	{
		soundPanel.SetActive(true);
		lightingPanel.SetActive(false);
	}

	public void EnableLightingPanel()
	{
		lightingPanel.SetActive(true);
		soundPanel.SetActive(false);
	}
}
