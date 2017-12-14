using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis {
	X,
	Y,
	Z
}

public class UnitUtilities : MonoBehaviour {

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
	public static IEnumerator RotateRoundAxis(float rotationTime, float rotationAngle, Axis rotationAxis, GameObject objToRotate, Action onBegin = null, Action onDone = null, float delay = 0) {

		Vector3 startPos = objToRotate.transform.localEulerAngles;
		Vector3 endPos = objToRotate.transform.localEulerAngles;

		switch(rotationAxis)
		{
			case Axis.X:
				endPos.x += rotationAngle;
				break;
			case Axis.Y:
				endPos.y += rotationAngle;
				break;
			case Axis.Z:
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

	/*
     * Trigger the VIVE controller haptic pulse over a period of time.
     */
    public static IEnumerator TriggerVibration(SteamVR_TrackedController controller, float vibrationStrength, float vibrationLength)
    {
        for (float i = 0; i < vibrationLength; i += Time.deltaTime)
        {
            SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, vibrationStrength));
            yield return null;
        }    
    }

    /*
     * Trigger a continuous haptic pulse. Strength equal to 1 is the strongest haptic pulse.
     */
    public static void TriggerContinuousVibration(SteamVR_TrackedController controller, float vibrationStrength)
    {
        SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, vibrationStrength));
    }

}


