using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	// General Light Intensity
	[HideInInspector]
	public float MAX_LIGHT_INTENSITY = 1;
	[Range(0.0f, 1.0f)]
	public float lightIntensity;
	private float _currentLightIntensity;

	// General Light Color.
	public Color lightColor;
	private Color _currentLightColor;

	// Ambience Intensity.
	[Range(0.0f, 1.0f)]
	public float ambienceIntensity;
	public Color ambienceColor;

	public Action<float> onLightIntensityChanged = null;
	public Action<Color> onLightColorChanged = null;

	void Start()
	{
		_currentLightIntensity = lightIntensity;
		_currentLightColor = lightColor;
	}

	void Update()
	{
		CheckIfColorChanged();
		CheckIfIntensityChanged();

		SetAmbienceChanges();
	}

	void SetAmbienceChanges()
	{
		RenderSettings.ambientLight = ambienceColor;

		RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Color.black, ambienceIntensity);
	}

	void CheckIfColorChanged()
	{
		if (_currentLightColor != lightColor)
		{
			_currentLightColor = lightColor;
		
			if (onLightColorChanged != null)
			{
				onLightColorChanged(_currentLightColor);
			}
		}
	}

	void CheckIfIntensityChanged()
	{
		if (_currentLightIntensity != lightIntensity)
		{
			_currentLightIntensity = lightIntensity;

			if (onLightIntensityChanged != null)
			{
				onLightIntensityChanged(_currentLightIntensity);
			}
		}
	}
	
}
