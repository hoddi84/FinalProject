using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameButtonScript : MonoBehaviour {

	private ComplexInterfaceController complexController;

	private Action<GameObject> onFrameSelected = null;
	private Action<GameObject> onFrameDeselected = null;

	private bool isSelected = false;

	void Awake()
	{
		complexController = FindObjectOfType(typeof(ComplexInterfaceController)) as ComplexInterfaceController;
		onFrameSelected += complexController.AddToActiveFrames;
		onFrameDeselected += complexController.RemoveFromActiveFrames;
	}

	void Start()
	{
		Initialize();
	}

	void Initialize()
	{
		GetComponent<Outline>().enabled = false;
	}

	public void ClickOnFrame()
	{
		isSelected = !isSelected;

		if (isSelected)
		{
			GetComponent<Outline>().enabled = true;
			if (onFrameSelected != null)
			{
				onFrameSelected(gameObject);
			}
		}
		else
		{
			GetComponent<Outline>().enabled = false;
			if (onFrameDeselected != null)
			{
				onFrameDeselected(gameObject);
			}
		}
	}

	public void SelectFrame()
	{
		isSelected = true;
		GetComponent<Outline>().enabled = true;
	}

	public void DeselectFrame()
	{
		isSelected = false;
		GetComponent<Outline>().enabled = false;
	}
}
