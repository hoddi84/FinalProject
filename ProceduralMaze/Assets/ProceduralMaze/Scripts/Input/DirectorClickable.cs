using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Place this class on objects that should be interactable
/// for the director, i.e. the door open / close sprites for the
/// doors.
/// </summary>
public class DirectorClickable : MonoBehaviour {

	private ProMouseInput _mouseInput;
	public Action onHit = null;

	void Awake()
	{
		_mouseInput = FindObjectOfType(typeof(ProMouseInput)) as ProMouseInput;
	}

	void OnEnable()
	{
		_mouseInput.onMouseButtonDownLeftRaycast += ClickedObject;
	}

	void OnDisable()
	{
		_mouseInput.onMouseButtonDownLeftRaycast -= ClickedObject;
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
