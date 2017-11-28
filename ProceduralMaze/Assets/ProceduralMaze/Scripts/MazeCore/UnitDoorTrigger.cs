using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorTrigger : MonoBehaviour {

	private const string PLAYER = "Player";
	public Action onTriggerEntered = null;
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == PLAYER)
		{
			if (onTriggerEntered != null)
			{
				onTriggerEntered();
			}
		}
	}
}
