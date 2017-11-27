using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	// General Light Color.
	public Color lightColor;
	private Color _lightColor;
	public Color ambienceColor;
	private Color _ambienceColor;

	[Range(0.0f, 1.0f)]
	public float lightIntensity;
	private float _lightIntensity;

	[Range(0.0f, 1.0f)]
	public float ambienceIntensity;
	private float _ambienceIntensity;

	public Action<float> onLightIntensityChanged = null;
	public Action<Color> onLightColorChanged = null;

	private SimpleInterfaceController simpleController;

	void Awake()
	{
		simpleController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
		simpleController.onScarySliderChanged += UpdateAmbienceIntensity;
		simpleController.onScarySliderChanged += UpdateLightIntensity;
	}

	void Start()
	{
		_lightColor = lightColor;
		_ambienceColor = ambienceColor;
	}

	void Update()
	{
		if (_lightIntensity != lightIntensity)
		{
			UpdateLightIntensity(lightIntensity);
		}

		if (_ambienceIntensity != ambienceIntensity)
		{
			UpdateAmbienceIntensity(ambienceIntensity);
		}

		CheckIfLightColorChanged();

		if (_ambienceColor != ambienceColor)
		{
			RenderSettings.ambientLight = ambienceColor;
			_ambienceColor = ambienceColor;
		}
	}

	void UpdateAmbienceIntensity(float newAmbienceIntensity)
	{
		RenderSettings.ambientLight = ambienceColor;

		RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Color.black, newAmbienceIntensity);

		ambienceIntensity = newAmbienceIntensity;
		_ambienceIntensity = ambienceIntensity;
	}

	void UpdateLightIntensity(float newIntensity)
	{
		if (onLightIntensityChanged != null)
		{
			onLightIntensityChanged(newIntensity);
		}

		lightIntensity = newIntensity;
		_lightIntensity = lightIntensity;
	}	

	void CheckIfLightColorChanged()
	{
		if (_lightColor != lightColor)
		{	
			if (onLightColorChanged != null)
			{
				onLightColorChanged(lightColor);
			}

			_lightColor = lightColor;
		}
	}
}
