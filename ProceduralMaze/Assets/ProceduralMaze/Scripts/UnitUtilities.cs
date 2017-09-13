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

	public static IEnumerator RotateRoundAxis(float rotationTime, float rotationAngle, Axis rotationAxis, GameObject objToRotate, Action onDone = null) {

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
