using System.Collections;
using UnityEngine;

namespace MazeCore.Lighting {

	public class LightSettings : MonoBehaviour {

		private LightManager _lightManager;
		private Light _light;

		private float _lightInDuration = 1.0f;
		private float _lightOutDuration = .5f;

		private void Awake()
		{
			_lightManager = FindObjectOfType<LightManager>();
			_light = GetComponentInChildren<Light>();	
		}

		private void OnEnable()
		{
			if (_lightManager != null)
			{
				_lightManager.onLightColorChanged += SetLightColor;		
				_lightManager.onLightIntensityChanged += SetLightIntensity;
			}

			StartCoroutine(LightingIntro(true));
		}

		private void OnDisable()
		{
			if (_lightManager != null)
			{
				_lightManager.onLightColorChanged -= SetLightColor;		
				_lightManager.onLightIntensityChanged -= SetLightIntensity;
			}
		}

		private void Start()
		{
			_light.intensity = _lightManager.lightIntensity;
		}

		/// <summary>
		/// Callback executed when light intensity has been changed.
		/// </summary>
		/// <param name="newIntensity">New light intensity.</param>
		private void SetLightIntensity(float newIntensity)
		{
			if (_light != null)
			{
				_light.intensity = newIntensity;
			}
		}

		/// <summary>
		/// Callback executed when light color has been changed.
		/// </summary>
		/// <param name="newColor">New light color.</param>
		private void SetLightColor(Color newColor)
		{
			if (_light != null)
			{
				_light.color = newColor;
			}
		}

		private IEnumerator LightingIntro(bool intro)
		{
			float duration = 0;
			float elapsedTime = 0;

			_light.color = _lightManager.lightColor;

			if (intro)
			{
				duration = _lightInDuration;
			}
			else
			{
				duration = _lightOutDuration;
			}


			while (elapsedTime < duration)
			{
				if (intro)
				{
					_light.intensity = Mathf.Lerp(0, _lightManager.lightIntensity, (elapsedTime/duration));
				}
				else 
				{
					_light.intensity = Mathf.Lerp(_lightManager.lightIntensity, 0, (elapsedTime/duration));
				}
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			if (!intro)
			{
				Destroy(gameObject);
			}
		}

		public void DisableLight()
		{
			StartCoroutine(LightingIntro(false));
		}	
	}
}


