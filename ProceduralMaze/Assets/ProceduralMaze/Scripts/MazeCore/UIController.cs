using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameObject simplePanel;
	public GameObject focusedPanel;

	public Toggle simpleCheck;
	public Toggle focusedCheck;

	private bool isSimpleCheckOn;
	private bool isFocusedCheckOn;

	void Start()
	{
		isSimpleCheckOn = simpleCheck.isOn;
		isFocusedCheckOn = focusedCheck.isOn;
	}

	void SimpleMode()
	{

	}

	void FocusedMode()
	{
		
	}
}
