using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazeUI {

	public class UIController : MonoBehaviour {

		public enum ControlMode { SimpleControlMode, ComplexControlMode }

		public GameObject simplePanel;
		public GameObject focusedPanel;

		public Toggle simpleCheck;
		public Toggle focusedCheck;

		public bool isSimpleCheckOn;
		public bool isFocusedCheckOn;

		public Text mainTitleText;
		public string titleSimple;
		public string titleComplex;

		public Action<ControlMode> onControlModeSwitch = null;

		void Start()
		{
			isSimpleCheckOn = simpleCheck.isOn;
			isFocusedCheckOn = focusedCheck.isOn;

			simplePanel.SetActive(simpleCheck.isOn);
			focusedPanel.SetActive(focusedCheck.isOn);

			if (onControlModeSwitch != null)
			{
				onControlModeSwitch(ControlMode.SimpleControlMode);
			}
		}

		void UpdateControlMode()
		{
			if (isSimpleCheckOn)
			{
				mainTitleText.text = titleSimple;
			}
			else
			{
				mainTitleText.text = titleComplex;
			}
		}

		void Update()
		{
			simplePanel.SetActive(simpleCheck.isOn);
			focusedPanel.SetActive(focusedCheck.isOn);

			UpdateControlMode();
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
				
				if (onControlModeSwitch != null)
				{
					onControlModeSwitch(ControlMode.ComplexControlMode);
				}
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

				if (onControlModeSwitch != null)
				{
					onControlModeSwitch(ControlMode.SimpleControlMode);
				}
			}
		}
	}
}

