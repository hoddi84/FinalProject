﻿using System;
using UnityEngine;

namespace MazeCore.Lighting {

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

		private SimpleInterfaceController _simpleController;
		private ComplexInterfaceController _complexController;

		private CUIColorPicker _colorPicker;
		private Action<Color> onColorChanged = null;

		private void Awake()
		{
			_simpleController = FindObjectOfType<SimpleInterfaceController>();
			_complexController = FindObjectOfType<ComplexInterfaceController>();
			_colorPicker = FindObjectOfType<CUIColorPicker>();

			if (_colorPicker != null)
			{
				onColorChanged = OnColorPickerChanged;
				_colorPicker.SetOnValueChangeCallback(onColorChanged);
			}
		}

		private void OnEnable()
		{
			if (_simpleController != null)
			{
				_simpleController.onScarySliderChanged += UpdateAmbienceIntensity;
				_simpleController.onScarySliderChanged += UpdateLightIntensity;
			}

			if (_complexController != null)
			{
				_complexController.onLightIntensityChanged += UpdateLightIntensity;
				_complexController.onAmbienceIntensityChanged += UpdateAmbienceIntensity;
			}
		}

		private void OnDisable()
		{
			if (_simpleController != null)
			{
				_simpleController.onScarySliderChanged -= UpdateAmbienceIntensity;
				_simpleController.onScarySliderChanged -= UpdateLightIntensity;
			}

			if (_complexController != null)
			{
				_complexController.onLightIntensityChanged -= UpdateLightIntensity;
				_complexController.onAmbienceIntensityChanged -= UpdateAmbienceIntensity;
			}
		}

		private void Start()
		{
			_lightColor = lightColor;
			_ambienceColor = ambienceColor;
		}

		private void Update()
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

		private void OnColorPickerChanged(Color newColor)
		{
			ambienceColor = newColor;
			lightColor = newColor;
		}

		private void UpdateAmbienceIntensity(float newAmbienceIntensity)
		{
			RenderSettings.ambientLight = Color.Lerp(ambienceColor, Color.black, newAmbienceIntensity);

			ambienceIntensity = newAmbienceIntensity;
			_ambienceIntensity = ambienceIntensity;
		}

		private void UpdateLightIntensity(float newIntensity)
		{
			if (onLightIntensityChanged != null)
			{
				onLightIntensityChanged(newIntensity);
			}

			lightIntensity = newIntensity;
			_lightIntensity = lightIntensity;
		}	

		private void CheckIfLightColorChanged()
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

		private void CheckIfAmbienceColorChanged()
		{
			if (_ambienceColor != ambienceColor)
			{
				RenderSettings.ambientLight = Color.Lerp(ambienceColor, Color.black, ambienceIntensity);
				_ambienceColor = ambienceColor;
			}
		}
	}
}
