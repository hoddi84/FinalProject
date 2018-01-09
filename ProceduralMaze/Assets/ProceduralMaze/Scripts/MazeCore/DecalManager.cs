using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalManager : MonoBehaviour {

	private SimpleInterfaceController simpleInterface;

	[Range(0.0f, 1.0f)]
	public float bloodDecal;
	private float _bloodDecal;

	public Action<float> onBloodDecalChanged = null;

	void Awake()
	{
		simpleInterface = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;

		if (simpleInterface != null)
		{
			bloodDecal = simpleInterface.scarySlider.value;
			_bloodDecal = bloodDecal;
			
			simpleInterface.onScarySliderChanged += UpdateBloodDecals;
		}
		else
		{
			bloodDecal = 0;
			_bloodDecal = bloodDecal;
		}
	}

	void Update()
	{
		if (_bloodDecal != bloodDecal)
		{
			UpdateBloodDecals(bloodDecal);
		}
	}

	void UpdateBloodDecals(float newValue)
	{
		bloodDecal = newValue;
		_bloodDecal = bloodDecal;

		if (onBloodDecalChanged != null)
		{
			onBloodDecalChanged(bloodDecal);
		}
	}
}
