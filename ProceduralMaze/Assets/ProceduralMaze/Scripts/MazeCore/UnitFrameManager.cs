﻿using System.Collections;
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
	public float frameVariance;
	private float _frameVariance;

	void Awake()
	{
		simpleInterfaceController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;
		simpleInterfaceController.onScarySliderChanged += UpdateAvailablePhotos;
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
