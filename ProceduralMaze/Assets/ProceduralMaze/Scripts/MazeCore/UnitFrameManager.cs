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

	private float simpleSliderValue;

	void Awake()
	{
		simpleInterfaceController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
	}

	void Start()
	{
		if (simpleInterfaceController != null)
		{
			simpleSliderValue = simpleInterfaceController.slider.value;
		}
		UpdateAvailablePhotos();
	}

	void Update()
	{
		if (simpleInterfaceController != null)
		{
			if (simpleSliderValue != simpleInterfaceController.slider.value)
			{
				UpdateAvailablePhotos();
				simpleSliderValue = simpleInterfaceController.slider.value;
			}
		}
	}

	void UpdateAvailablePhotos()
	{
		if (simpleSliderValue < .3f)
		{
			availablePhotos = new List<Sprite>();
			foreach (Sprite x in photoAssets.healthyFlowers)
			{
				availablePhotos.Add(x);
			}
		}
		else if (simpleSliderValue >= .3f && simpleSliderValue < .7f)
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
