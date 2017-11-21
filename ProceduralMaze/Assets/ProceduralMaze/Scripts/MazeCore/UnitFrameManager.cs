using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFrameManager : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float frameSpawnChance = 0f;
	public FrameAssetsSriptableObject photoAssets;
	[HideInInspector]
	public List<Sprite> availablePhotos;
	private SimpleInterfaceController simpleInterfaceController;


	//TODO Connect scary slider to frame.
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
		UpdateAvailablePhotos(_frameSliderValue);
	}

	void Update()
	{
		if (simpleInterfaceController != null)
		{
			if (_frameSliderValue != frameSliderValue)
			{
				_frameSliderValue = frameSliderValue;
				UpdateAvailablePhotos(_frameSliderValue);
			}
		}
	}

	public void UpdateAvailablePhotos(float sliderValue)
	{
		if (sliderValue < .3f)
		{
			availablePhotos = new List<Sprite>();
			foreach (Sprite x in photoAssets.healthyFlowers)
			{
				availablePhotos.Add(x);
			}
		}
		else if (sliderValue >= .3f && sliderValue < .7f)
		{
			availablePhotos = new List<Sprite>();
			foreach (Sprite x in photoAssets.dyingFlowers)
			{
				availablePhotos.Add(x);
			}
		}
		else 
		{
			availablePhotos = new List<Sprite>();
			foreach (Sprite x in photoAssets.scary)
			{
				availablePhotos.Add(x);
			}
		}
	}
}
