using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleInterfaceController : MonoBehaviour {

	public Slider scarySlider;
	public Slider presenceSlider;
	public Action<float> onScarySliderChanged = null;
	public Action<float> onPresenceSliderChanged = null;

	public const float SLIDER_MIN_VALUE = 0;
	public const float SLIDER_START_VALUE = 0;
	public const float SLIDER_MAX_VALUE = 1;

	void Awake()
	{
		scarySlider.onValueChanged.AddListener(UpdateScarySlider);
		presenceSlider.onValueChanged.AddListener(UpdatePresenceSlider);
	}

	void Start()
	{
		InitializeSlider();
	}

	void UpdateScarySlider(float value)
	{
		if (onScarySliderChanged != null)
		{
			onScarySliderChanged(value);
		}
	}

	void UpdatePresenceSlider(float value)
	{
		if (onPresenceSliderChanged != null)
		{
			onPresenceSliderChanged(value);
		}
	}

	void InitializeSlider()
	{
		scarySlider.minValue = SLIDER_MIN_VALUE;
		scarySlider.value = SLIDER_START_VALUE;
		scarySlider.maxValue = SLIDER_MAX_VALUE;

		presenceSlider.minValue = SLIDER_MIN_VALUE;
		presenceSlider.value = SLIDER_START_VALUE;
		presenceSlider.maxValue = SLIDER_MAX_VALUE;

		if (onScarySliderChanged != null)
		{
			onScarySliderChanged(scarySlider.value);
		}

		if (onPresenceSliderChanged != null)
		{
			onPresenceSliderChanged(presenceSlider.value);
		}
	}
}
