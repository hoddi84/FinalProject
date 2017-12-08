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

		Vector3 startPos = objToRotate.transform.eulerAngles;
		Vector3 endPos = objToRotate.transform.eulerAngles;

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
			objToRotate.transform.eulerAngles = Vector3.Lerp(startPos, endPos, (elapsedTime/rotationTime));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		objToRotate.transform.eulerAngles = endPos;

		if (onDone != null) 
		{
			onDone();
		}
	}
}


