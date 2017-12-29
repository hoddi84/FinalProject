using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SoundManager : MonoBehaviour {

	[System.Serializable]
	public struct AudioUnit {
		
		public AudioSource audioSource;
		[Range(0.0f, 1.0f)]
		public float audioStart;
		[Range(0.0f, 1.0f)]
		public float audioEnd;
		public bool increasingAudio;
		[Range(0.0f, 1.0f)]
		public float maxVolume;
	}

	[Header("Simple Mode")]
	public AudioUnit[] audioUnit;

	private SimpleInterfaceController simpleController;

	private void Awake()
	{
		simpleController = FindObjectOfType(typeof(SimpleInterfaceController)) as SimpleInterfaceController;

		if (simpleController != null)
		{
			simpleController.onScarySliderChanged += OnScarySliderChanged;
		}
	}

	private void OnScarySliderChanged(float value)
	{
		foreach (AudioUnit unit in audioUnit)
		{
			float interval = unit.audioEnd - unit.audioStart;

			float zeroVolumeTime = .2f;

			if (value >= unit.audioStart && value < unit.audioEnd)
			{
				unit.audioSource.volume = Mathf.Lerp(0, unit.maxVolume, (value - unit.audioStart)/interval);
			}
			else if (value >= unit.audioEnd)
			{
				unit.audioSource.volume = Mathf.Lerp(unit.maxVolume, 0, (value - unit.audioEnd)/zeroVolumeTime);
			}
			else if (value < unit.audioStart && unit.audioSource.volume > 0)
			{
				unit.audioSource.volume = Mathf.Lerp(unit.audioSource.volume, 0, (Mathf.Abs(value - unit.audioStart))/zeroVolumeTime);
			}
		}
	}

	private void Setup()
	{
		foreach (AudioUnit unit in audioUnit)
		{
			unit.audioSource.loop = true;
			unit.audioSource.playOnAwake = true;
			unit.audioSource.minDistance = 495;
			
			if (unit.increasingAudio)
			{
				unit.audioSource.volume = 0;
			}
		}
	}
}
