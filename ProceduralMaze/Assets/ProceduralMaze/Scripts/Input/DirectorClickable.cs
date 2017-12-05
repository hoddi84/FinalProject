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
	}

	void OnEnable()
	{
		mouseInput.onMouseButtonDownLeftRaycast += ClickedObject;
		mouseInput.onRenderTextureClick += ClickedObjectOnRenderTexture;
	}

	void OnDisable()
	{
		mouseInput.onMouseButtonDownLeftRaycast -= ClickedObject;
		mouseInput.onRenderTextureClick += ClickedObjectOnRenderTexture;
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

	void ClickedObjectOnRenderTexture(Vector3 pos, RaycastHit hit, Camera camera)
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
