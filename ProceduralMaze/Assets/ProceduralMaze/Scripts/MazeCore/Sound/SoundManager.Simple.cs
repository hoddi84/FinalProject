using UnityEngine;

namespace MazeCore.Sound {

	public partial class SoundManager : MonoBehaviour {

		/// <summary>
		/// Callback executed when scary meter changes, logic for 
		/// AudioUnit container struct.
		/// </summary>
		/// <param name="value">The current scary meter value.</param>
		private void OnScarySliderChanged(float value)
		{
			if (audioUnits != null)
			{
				foreach (AudioUnit unit in audioUnits)
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
		}

		private void SetupAudioUnit()
		{
			if (audioUnits != null)
			{
				foreach (AudioUnit unit in audioUnits)
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
	}
}

