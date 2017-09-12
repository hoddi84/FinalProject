using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoor : MonoBehaviour {

	public GameObject frame;
	public GameObject door;
	private Action onDoorRotChanged = null;

	private void Update() 
	{
		if(Input.GetKeyDown(KeyCode.E)) {
			test();
			// TODO 
			// Fix bandwidth recurrency failure.
		}
	}
	private void test() 
	{
		print("Frame" + frame.transform.forward);
		print("Door" + door.transform.forward);
	}




	private bool IsDoorClosed() 
	{
		return false;
	}


}
