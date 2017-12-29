using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

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
	private ComplexInterfaceController complexController;

	private CUIColorPicker colorPicker;
	private Action<Color> onColorChanged = null;

	void Awake()
	{
		simpleController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
		
		if (simpleController != null)
		{
			simpleController.onScarySliderChanged += UpdateAmbienceIntensity;
			simpleController.onScarySliderChanged += UpdateLightIntensity;
		}

		complexController = FindObjectOfType(typeof(ComplexInterfaceController)) as ComplexInterfaceController;

		if (complexController != null)
		{
			complexController.onLightIntensityChanged += UpdateLightIntensity;
			complexController.onAmbienceIntensityChanged += UpdateAmbienceIntensity;
		}

		colorPicker = FindObjectOfType(typeof(CUIColorPicker)) as CUIColorPicker;

		if (colorPicker != null)
		{
			onColorChanged = OnColorPickerChanged;
			colorPicker.SetOnValueChangeCallback(onColorChanged);
		}
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
		CheckIfAmbienceColorChanged();
	}

	void OnColorPickerChanged(Color newColor)
	{
		ambienceColor = newColor;
		lightColor = newColor;
	}

	void UpdateAmbienceIntensity(float newAmbienceIntensity)
	{
		RenderSettings.ambientLight = Color.Lerp(ambienceColor, Color.black, newAmbienceIntensity);

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

	void CheckIfAmbienceColorChanged()
	{
		if (_ambienceColor != ambienceColor)
		{
			RenderSettings.ambientLight = Color.Lerp(ambienceColor, Color.black, ambienceIntensity);
			_ambienceColor = ambienceColor;
		}
	}
}
