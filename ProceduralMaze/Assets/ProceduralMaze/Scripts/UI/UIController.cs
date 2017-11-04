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

		simplePanel.SetActive(simpleCheck.isOn);
		focusedPanel.SetActive(focusedCheck.isOn);
	}

	void Update()
	{
		simplePanel.SetActive(simpleCheck.isOn);
		focusedPanel.SetActive(focusedCheck.isOn);
	}

	public void SimpleMode()
	{
		// Allready checked.
		if (isSimpleCheckOn && !simpleCheck.isOn)
		{
			simpleCheck.isOn = true;
			return;
		}

		// Not checked.
		if (!isSimpleCheckOn && isFocusedCheckOn)
		{
			// Now Checked.

			// Uncheck other.
			isFocusedCheckOn = false;
			focusedCheck.isOn = false;
		}

		if (!isSimpleCheckOn && !isFocusedCheckOn)
		{
			isFocusedCheckOn = true;
		}
	}

	public void FocusedMode()
	{
		// Allready checked.
		if (isFocusedCheckOn && !focusedCheck.isOn)
		{
			focusedCheck.isOn = true;
			return;
		}
		
		// Not checked.
		if (!isFocusedCheckOn && isSimpleCheckOn)
		{
			// Now checked.

			// Uncheck other.
			isSimpleCheckOn = false;
			simpleCheck.isOn = false;
		}

		if (!isFocusedCheckOn && !isSimpleCheckOn)
		{
			isSimpleCheckOn = true;
		}
	}
}
