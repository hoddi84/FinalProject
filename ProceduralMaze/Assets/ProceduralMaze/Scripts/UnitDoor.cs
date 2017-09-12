using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject frame;
	public GameObject door;
	private Action onDoorClosed = null;
	private Action onDoorOpen = null;

	private void Update() 
	{
		if (Input.GetKeyDown(KeyCode.E)) {
			StartCoroutine(RotateDoor(45));
		}
	}

	private bool IsDoorClosed() 
	{
		if (frame.transform.forward == door.transform.forward) 
		{
			return true;
		}
		else 
		{
			return false;
		}
	}

	private IEnumerator RotateDoor(float angle) {

		Vector3 startPos = door.transform.eulerAngles;
		Vector3 endPos = door.transform.eulerAngles;
		endPos.y += angle;

		float elapsedTime = 0f;
		float seconds = 5.0f;

		while (elapsedTime < seconds)
		{
			door.transform.eulerAngles = Vector3.Lerp(startPos, endPos, (elapsedTime/seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		door.transform.eulerAngles = endPos;
	}
}
