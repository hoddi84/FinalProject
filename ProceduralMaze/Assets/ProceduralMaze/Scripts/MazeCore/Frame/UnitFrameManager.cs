using System.Collections.Generic;
using UnityEngine;
using MazeUtiliy;
using MazeUI;

namespace MazeCore.Frame {

	public class UnitFrameManager : MonoBehaviour {

		[Range(0.0f, 1.0f)]
		public float frameSpawnChance = 0f;
		public FrameAssetsSriptableObject photoAssets;

		[HideInInspector]
		public List<Sprite> availablePhotos;

		private UIController _uIController;
		private SimpleInterfaceController _simpleInterfaceController;
		private ComplexInterfaceController _complexInterfaceController;

		public float GetScaryMeterValue() 
		{
			if (_simpleInterfaceController != null) { return _simpleInterfaceController.scarySlider.value; }
			else { return 0; }
		}

		[Range(0.0f, 1.0f)]
		public float frameVariance;
		private float _frameVariance;

		[Range(0.0f, 4.0f)]
		public float maxFrameRotation;

		private void Awake()
		{
			_uIController = FindObjectOfType<UIController>();

			if (_uIController != null)
			{
				_simpleInterfaceController = _uIController.GetComponent<SimpleInterfaceController>();
				_complexInterfaceController = _uIController.GetComponent<ComplexInterfaceController>();

				_uIController.onControlModeSwitch += ChangeControlModes;
				_simpleInterfaceController.onScarySliderChanged += UpdateAvailablePhotos;
				_complexInterfaceController.onFrameListChanged += UpdateAvailablePhotos;
			}
			else
			{
				UpdateAvailablePhotos(frameVariance);
			}
		}

		private void ChangeControlModes(UIController.ControlMode mode)
		{
			switch (mode) {
				case UIController.ControlMode.SimpleControlMode:
					UpdateAvailablePhotos(frameVariance);
				break;
				case UIController.ControlMode.ComplexControlMode:
					availablePhotos.Clear();
				break;
			}
		}

		private void Start()
		{
			_frameVariance = frameVariance;
		}

		private void Update()
		{
			if (_frameVariance != frameVariance)
			{
				UpdateAvailablePhotos(frameVariance);
				_frameVariance = frameVariance;
			}
		}

		/// <summary>
		/// Callback for when the list of selected photos in the complex
		/// interface mode changes.
		/// </summary>
		/// <param name="newList">List of selected photos.</param>
		public void UpdateAvailablePhotos(List<Sprite> newList)
		{
			availablePhotos = newList;
		}

		/// <summary>
		/// Callback for when the scary meter in the simple interface
		/// mode changes.
		/// </summary>
		/// <param name="newFrameVariance">The current scary meter value.</param>
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
}


