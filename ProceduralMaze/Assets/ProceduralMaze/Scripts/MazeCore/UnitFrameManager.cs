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

	/*
	 * We have three photo groups.
	 * Need to add more variety to the changes in slider values.
	 * At 0: Only healthy flower paintings.
	 * At 0 < x < 50: Gradually increasing mix of dying flower paintings.
	 * At 50: Only dying flower paintings.
	 * At 50 < x < 100: Gradually increasing mix of creepy paintings. 
	 * At 100: Only creepy paintings.
	 */
	public void UpdateAvailablePhotos(float sliderValue)
	{
		if (sliderValue < .5f)
		{
			IncreaseVariance(photoAssets.healthyFlowers, photoAssets.dyingFlowers, 0, sliderValue, .5f, ref availablePhotos);
		}
		else if (sliderValue >= .5f && sliderValue <= 1f)
		{
			IncreaseVariance(photoAssets.dyingFlowers, photoAssets.scary, .5f, sliderValue, .5f, ref availablePhotos);
		}
	}

	private void IncreaseVariance(Sprite[] from, Sprite[] to, float startRange, float value, float range, ref List<Sprite> listToFill)
	{
		listToFill = new List<Sprite>();
		float ratio = (value - startRange) / range;

		// Fill first list.
		foreach (Sprite x in from)
		{
			float rnd = Random.Range(0.0f, 1.0f);

			if (ratio == 0)
			{
				listToFill.Add(x);
			}
			if (rnd > ratio)
			{
				listToFill.Add(x);
			}
		}

		// Fill second list.
		foreach (Sprite x in to)
		{
			float rnd = Random.Range(0.0f, 1.0f);

			if (ratio == 1)
			{
				listToFill.Add(x);
			}
			if (rnd < ratio)
			{
				listToFill.Add(x);
			}
		}
	}
}
