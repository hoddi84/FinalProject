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
	private SimpleInterfaceController simpleInterfaceController;

	[Range(0.0f, 1.0f)]
	public float frameSliderValue;
	private float _frameSliderValue;


	void Awake()
	{
		simpleInterfaceController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
	}

	void Start()
	{
		if (simpleInterfaceController != null)
		{
			frameSliderValue = simpleInterfaceController.slider.value;
			_frameSliderValue = frameSliderValue;
		}
		else
		{
			frameSliderValue = 0;
			_frameSliderValue = frameSliderValue;
		}
		UpdateAvailablePhotos(_frameSliderValue);
	}

	void Update()
	{
		if (_frameSliderValue != frameSliderValue)
		{
			_frameSliderValue = frameSliderValue;
			UpdateAvailablePhotos(_frameSliderValue);
		}
	}

	public void UpdateAvailablePhotos(float sliderValue)
	{
		if (sliderValue < .5f)
		{
			UtilityTools.IncreaseVariance(photoAssets.healthyFlowers, photoAssets.dyingFlowers, 0, sliderValue, .5f, ref availablePhotos);
		}
		else if (sliderValue >= .5f && sliderValue <= 1f)
		{
			UtilityTools.IncreaseVariance(photoAssets.dyingFlowers, photoAssets.scary, .5f, sliderValue, .5f, ref availablePhotos);
		}
	}
}
