using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorManager : MonoBehaviour {

	public float doorCloseTime;
	public float doorOpenTime;
	public float rotationAngle;

	public AudioClip openDoorClip;
	public AudioClip openHandleClip;
	public AudioClip closeDoorClip;
	public Sprite doorSpriteClosed;
	public Sprite doorSpriteOpen;
	public Sprite doorSpriteLockOpen;
	public Sprite doorSpriteLockClosed;

	[Range(0.0f, 1.0f)]
	public float presenceSliderValue;
	private float _presenceSliderValue;

	[Range(0.0f, 1.0f)]
	public float scarySliderValue;
	private float _scarySliderValue;

	private SimpleInterfaceController simepleController;

	void Awake()
	{
		simepleController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
		
		if (simepleController != null)
		{
			simepleController.onPresenceSliderChanged += UpdatePresenceValue;
		}
	}

	void Update()
	{
		if (_scarySliderValue != scarySliderValue)
		{
			UpdateScaryValue(scarySliderValue);
		}

		if (_presenceSliderValue != presenceSliderValue)
		{
			UpdatePresenceValue(presenceSliderValue);
		}
	}

	void UpdateScaryValue(float newScaryValue)
	{
		scarySliderValue = newScaryValue;
		_scarySliderValue = scarySliderValue;
	}

	void UpdatePresenceValue(float newPresenceValue)
	{
		presenceSliderValue = newPresenceValue;
		_presenceSliderValue = presenceSliderValue;
	}
}
