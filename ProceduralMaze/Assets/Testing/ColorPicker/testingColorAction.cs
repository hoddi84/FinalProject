using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeCore.Lighting;

public class testingColorAction : MonoBehaviour {

	public CUIColorPicker colorPicker;
	private LightManager lightManager;

	private Action<Color> onColorChanged = null;

	void Awake()
	{
		onColorChanged = OnColorChanged;
		colorPicker.SetOnValueChangeCallback(onColorChanged);

		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;
	}

	void OnColorChanged(Color col)
	{
		print("Color changes");
		print("Color: " + col.ToString());
		lightManager.ambienceColor = col;
		lightManager.lightColor = col;
	}
}
