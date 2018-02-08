using System;
using UnityEngine;
using MazeUI;

namespace MazeCore.Decals {

	public class DecalManager : MonoBehaviour {

		private SimpleInterfaceController _simpleInterface;
		private ComplexInterfaceController _complexInterface;

		[Range(0.0f, 1.0f)]
		public float bloodDecalAmount = 0;
		private float _bloodDecalAmount = 0;

		public Action<float> onBloodDecalChanged = null;

		void Awake()
		{
			_simpleInterface = FindObjectOfType<SimpleInterfaceController>();
			_complexInterface = FindObjectOfType<ComplexInterfaceController>();
		}

		void OnEnable()
		{
			if (_simpleInterface != null)
			{	
				_simpleInterface.onScarySliderChanged += UpdateBloodDecals;
			}

			if (_complexInterface != null)
			{
				_complexInterface.onDecalBloodAmountChanged += UpdateBloodDecals;
			}
		}

		void OnDisable()
		{
			if (_simpleInterface != null)
			{	
				_simpleInterface.onScarySliderChanged -= UpdateBloodDecals;
			}

			if (_complexInterface != null)
			{
				_complexInterface.onDecalBloodAmountChanged -= UpdateBloodDecals;
			}
		}

		void Update()
		{
			if (_bloodDecalAmount != bloodDecalAmount)
			{
				UpdateBloodDecals(bloodDecalAmount);
			}
		}

		/// <summary>
		/// Callback executed when scary meter slider changes in simple mode UI, or when the 
		/// decal slider changes in complex mode UI.
		/// </summary>
		/// <param name="newValue">New decal amount value.</param>
		void UpdateBloodDecals(float newBloodDecalAmount)
		{
			bloodDecalAmount = newBloodDecalAmount;
			_bloodDecalAmount = bloodDecalAmount;

			if (onBloodDecalChanged != null)
			{
				onBloodDecalChanged(bloodDecalAmount);
			}
		}
	}
}
