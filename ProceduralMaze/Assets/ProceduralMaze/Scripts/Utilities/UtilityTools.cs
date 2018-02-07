using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazeUtiliy {

	public enum RotationAxis { X, Y, Z }

	public static class UtilityTools  {

		/// <summary>
		/// Fill a list with increasing and decreasing occurrences of two groups of Sprites.
		/// </summary>
		/// <param name="spriteGroupFrom">Decreasing Sprite Family.</param>
		/// <param name="spriteGroupTo">Increasing Sprite Family.</param>
		/// <param name="startRange">Start Range.</param>
		/// <param name="value">Current Value.</param>
		/// <param name="range">Length of Range.</param>
		/// <param name="listToFill">The List to Fill</param>
		public static void IncreaseVariance(Sprite[] spriteGroupFrom, Sprite[] spriteGroupTo, float startRange, float value, float range, ref List<Sprite> listToFill)
		{
			listToFill = new List<Sprite>();
			float ratio = (value - startRange) / range;

			foreach (Sprite x in spriteGroupFrom)
			{
				float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

				if (ratio == 0)
				{
					listToFill.Add(x);
				}
				if (rnd > ratio)
				{
					listToFill.Add(x);
				}
			}

			foreach (Sprite x in spriteGroupTo)
			{
				float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

				if (ratio == 1)
				{
					listToFill.Add(x);
				}
				if (rnd < ratio)
				{
					listToFill.Add(x);
				}
			}
		}

		/// <summary>
		/// A helper method to rotate objects.
		/// </summary>
		/// <param name="rotationTime">The time in seconds for the rotation to complete.</param>
		/// <param name="rotationAngle">The angle of which to rotate the object.</param>
		/// <param name="rotationAxis">The axis of rotation.</param>
		/// <param name="objToRotate">The gameObject to rotate.</param>
		/// <param name="onDone">Event for when the rotation has finished.</param>
		/// <param name="delay">Delay before rotation starts.</param>
		/// <returns></returns>
		public static IEnumerator RotateRoundAxis(float rotationTime, float rotationAngle, RotationAxis rotationAxis, GameObject objToRotate, Action onBegin = null, Action onDone = null, float delay = 0) {

			Vector3 startPos = objToRotate.transform.localEulerAngles;
			Vector3 endPos = objToRotate.transform.localEulerAngles;

			switch(rotationAxis)
			{
				case RotationAxis.X:
					endPos.x += rotationAngle;
					break;
				case RotationAxis.Y:
					endPos.y += rotationAngle;
					break;
				case RotationAxis.Z:
					endPos.z += rotationAngle;
					break;
			}

			float elapsedTime = 0f;

			yield return new WaitForSeconds(delay);

			if (onBegin != null)
			{
				onBegin();
			}

			while (elapsedTime < rotationTime)
			{
				objToRotate.transform.localEulerAngles = Vector3.Lerp(startPos, endPos, (elapsedTime/rotationTime));
				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			objToRotate.transform.localEulerAngles = endPos;

			if (onDone != null) 
			{
				onDone();
			}
		}

		/// <summary>
		/// Trigger VIVE controller haptic feedback.
		/// </summary>
		/// <param name="controller">SteamVR_TrackedController.</param>
		/// <param name="vibrationStrength">Strength of haptic feedback. Max 1.</param>
		/// <param name="vibrationLength">Length of haptic feedback.</param>
		/// <returns></returns>
		public static IEnumerator TriggerVibration(SteamVR_TrackedController controller, float vibrationStrength, float vibrationLength)
		{
			for (float i = 0; i < vibrationLength; i += Time.deltaTime)
			{
				SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, vibrationStrength));
				yield return null;
			}    
		}

		/// <summary>
		/// Trigger continuous VIVE controller haptic feedback.
		/// </summary>
		/// <param name="controller">SteamVR_TrackedController.</param>
		/// <param name="vibrationStrength">Strength of haptic feedback. Max 1.</param>
		public static void TriggerContinuousVibration(SteamVR_TrackedController controller, float vibrationStrength)
		{
			SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, vibrationStrength));
		}

		/// <summary>
		/// Flicker active Lights.
		/// </summary>
		/// <param name="action">Actions to perform during blackout.</param>
		/// <param name="activeLights">Active Lights.</param>
		/// <returns></returns>
		public static IEnumerator FlickerLights(Light[] activeLights, Action action)
		{
			Color currentAmbienceColor = RenderSettings.ambientLight;

			List<float> intensities = new List<float>();

			foreach (Light light in activeLights)
			{
				intensities.Add(light.intensity);
			}

			TurnOffAllLights(activeLights);
			RenderSettings.ambientLight = Color.black;

			if (action != null)
			{
				action();
			}

			yield return new WaitForSeconds(.5f);

			TurnOnAllLights(activeLights, intensities);
			RenderSettings.ambientLight = currentAmbienceColor;

			yield return null;
		}

		/// <summary>
		/// Turn off active Lights.
		/// </summary>
		/// <param name="lights">Active Lights.</param>
		private static void TurnOffAllLights(Light[] lights)
		{
			foreach (Light light in lights)
			{
				light.intensity = 0;
			}
		}

		/// <summary>
		/// Turn on active Lights.
		/// </summary>
		/// <param name="lights">Active Lights.</param>
		/// <param name="intensities">Active Lights intensities.</param>
		private static void TurnOnAllLights(Light[] lights, List<float> intensities)
		{
			for (int i = 0; i < lights.Length; i++)
			{
				lights[i].intensity = intensities[i];
			}
		}

		/// <summary>
		/// Rearranges the elements of a list in random order.
		/// </summary>
		/// <param name="someList">List to be randomized.</param>
		/// <returns>Randomized list.</returns>
		public static List<T> RandomizeList<T>(List<T> someList)
		{
			List<T> randomized = new List<T>();
			List<T> original = new List<T>(someList);

			while (original.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, original.Count);
				randomized.Add(original[index]);
				original.RemoveAt(index);
			}
			return randomized;
		}
	}
}

