using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeUtiliy;

public class UnitFrameManager : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float frameSpawnChance = 0f;
	public FrameAssetsSriptableObject photoAssets;

	[HideInInspector]
	public List<Sprite> availablePhotos;

	private UIController uIController;
	private SimpleInterfaceController simpleInterfaceController;
	private ComplexInterfaceController complexInterfaceController;

	public float GetScaryMeterValue() 
	{
		if (simpleInterfaceController != null)
		{
			return simpleInterfaceController.scarySlider.value;
		}
		else 
		{
			return 0;
		}
	}

	[Range(0.0f, 1.0f)]
	public float frameVariance;
	private float _frameVariance;

	[Range(0.0f, 4.0f)]
	public float maxFrameRotation;

	void Awake()
	{
		uIController = FindObjectOfType(typeof(UIController)) as UIController;

		if (uIController != null)
		{
			simpleInterfaceController = uIController.GetComponent<SimpleInterfaceController>();
			complexInterfaceController = uIController.GetComponent<ComplexInterfaceController>();

			uIController.onControlModeSwitch += ChangeControlModes;
			simpleInterfaceController.onScarySliderChanged += UpdateAvailablePhotos;
			complexInterfaceController.onListChanged += UpdateAvailablePhotos;
		}
	}

	void ChangeControlModes(ControlMode mode)
	{
		switch (mode) {
			case ControlMode.SimpleControlMode:
				UpdateAvailablePhotos(frameVariance);
			break;
			case ControlMode.ComplexControlMode:
				availablePhotos.Clear();
			break;
		}
	}

	void Start()
	{
		_frameVariance = frameVariance;
	}

	void Update()
	{
		if (_frameVariance != frameVariance)
		{
			UpdateAvailablePhotos(frameVariance);
			_frameVariance = frameVariance;
		}
	}

	public void UpdateAvailablePhotos(List<Sprite> newList)
	{
		availablePhotos = newList;
	}

	public void UpdateAvailablePhotos(float newFrameVariance)
	{
		if (newFrameVariance < .5f)
		{
			UtilityTools.IncreaseVariance(photoAssets.healthyFlowers, photoAssets.dyingFlowers, 0, newFrameVariance, .5f, ref availablePhotos);
		}
		else if (newFrameVariance >= .5f && newFrameVariance <= 1f)
		{
			UtilityTools.IncreaseVariance(photoAssets.dyingFlowers, photoAssets.scary, .5f, newFrameVariance, .5f, ref availablePhotos);
		}

		frameVariance = newFrameVariance;
	}
}
