using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonSelect : MonoBehaviour {

	public Action<GameObject, bool> onFrameSelected = null;

	private Outline _frameOutline = null;
	private bool _frameSelected = false;

	private void Awake()
	{
		_frameOutline = GetComponent<Outline>();
	}

	private void Start()
	{
		_frameOutline.enabled = false;
	}

	private void OnFrameSelected(GameObject frame, bool selected)
	{
		GetComponent<Outline>().enabled = selected;

		if (onFrameSelected != null)
		{
			onFrameSelected(gameObject, selected);
		}
	}

	public void DeselectFrame()
	{
		_frameSelected = false;
		GetComponent<Outline>().enabled = _frameSelected;
	}

	public void ClickOnFrame()
	{
		_frameSelected = !_frameSelected;

		if (_frameSelected)
		{
			OnFrameSelected(gameObject, true);
		}
		else
		{
			OnFrameSelected(gameObject, false);
		}
	}
}



