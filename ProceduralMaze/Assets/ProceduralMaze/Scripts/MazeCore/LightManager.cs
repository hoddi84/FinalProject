using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

	public float lightIntensity = 1;
	private float _currentLightIntensity;
	public Color lightColor;
	private Color _currentLightColor;

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

		RenderSettings.ambientLight = ambienceColor;
	}

	void CheckIfIntensityChanged()
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

	void CheckIfColorChanged()
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
