using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorClickable : MonoBehaviour {

	private ProMouseInput mouseInput;
	public Action onHit = null;

	void Awake()
	{
		mouseInput = FindObjectOfType(typeof(ProMouseInput)) as ProMouseInput;
        mouseInput.onMouseButtonDownLeftRaycast += ClickedObject;
	}

	void ClickedObject(RaycastHit hit)
	{
		if (hit.transform.gameObject.GetInstanceID() == gameObject.GetInstanceID())
		{
			if (onHit != null)
			{
				onHit();
			}
		}
	}
}
