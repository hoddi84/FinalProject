using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalManager : MonoBehaviour {

	private SimpleInterfaceController simpleInterface;
	private ComplexInterfaceController complexInterface;

	[Range(0.0f, 1.0f)]
	public float bloodDecal;
	private float _bloodDecal;

	public Action<float> onBloodDecalChanged = null;

	void Awake()
	{
		simpleInterface = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
		complexInterface = FindObjectOfType(typeof(ComplexInterfaceController)) as ComplexInterfaceController;

		if (simpleInterface != null)
		{	
			simpleInterface.onScarySliderChanged += UpdateBloodDecals;
		}

		if (complexInterface != null)
		{
			complexInterface.onDecalBloodAmountChanged += UpdateBloodDecals;
		}

	}

	void Start()
	{
		bloodDecal = 0;
		_bloodDecal = bloodDecal;
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
