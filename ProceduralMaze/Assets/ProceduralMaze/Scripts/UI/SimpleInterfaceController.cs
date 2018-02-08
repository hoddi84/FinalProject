using System;
using UnityEngine;
using UnityEngine.UI;

namespace MazeUI {

	public class SimpleInterfaceController : MonoBehaviour {

		public Slider scarySlider;
		public Slider presenceSlider;
		public Action<float> onScarySliderChanged = null;
		public Action<float> onPresenceSliderChanged = null;

		private const float SLIDER_MIN_VALUE = 0;
		private const float SLIDER_START_VALUE = 0;
		private const float SLIDER_MAX_VALUE = 1;

		private void OnEnable()
		{
			scarySlider.onValueChanged.AddListener(UpdateScarySlider);
			presenceSlider.onValueChanged.AddListener(UpdatePresenceSlider);
		}

		private void OnDisable()
		{
			scarySlider.onValueChanged.RemoveAllListeners();
		}

		private void Start()
		{
			InitializeSlider();
		}

		/// <summary>
		/// Callback executed when the slider of scary meter is changed.
		/// </summary>
		/// <param name="value">The new scary meter value.</param>
		private void UpdateScarySlider(float value)
		{
			if (onScarySliderChanged != null)
			{
				onScarySliderChanged(value);
			}
		}

		/// <summary>
		/// Callback executed when the slider of presence meter is changed.
		/// </summary>
		/// <param name="value">The new presence meter value.</param>
		private void UpdatePresenceSlider(float value)
		{
			if (onPresenceSliderChanged != null)
			{
				onPresenceSliderChanged(value);
			}
		}

		/// <summary>
		/// Initialize the values of the scary meter and presence meter
		/// slider's, and sends the values to listeners.
		/// </summary>
		private void InitializeSlider()
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
}


