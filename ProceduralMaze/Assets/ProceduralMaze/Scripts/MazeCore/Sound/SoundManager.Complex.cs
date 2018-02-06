using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MazeCore.Sound {

	public partial class SoundManager : MonoBehaviour {

		/// <summary>
		/// Connects callbacks for sounds section in the complex mode UI.
		/// AmbienceSounds, SpecialSounds, EffectSounds.
		/// </summary>
		/// <param name="audioSourceAttached">The parent gameObject of audio sources.</param>
		private void InitializeSounds(GameObject audioSourceParent)
		{
			for (int i = 0; i < ambienceSounds.Length; i++)
			{
				AudioSource audioSource;
				SetupAudioSource(audioSourceParent, out audioSource, i, ambienceSoundAsset);

				ambienceSounds[i].activeToggle.onValueChanged.AddListener(delegate(bool active) {
					OnToggleChanged(active, audioSource);
				});
				ambienceSounds[i].volumeSlider.onValueChanged.AddListener(delegate(float value) {
					OnSliderChanged(value, audioSource);
				});
				ambienceSounds[i].audioSource = audioSource;
			}

			for (int i = 0; i < specialSounds.Length; i++)
			{
				AudioSource audioSource;
				SetupAudioSource(audioSourceParent, out audioSource, i, specialSoundsAsset);

				specialSounds[i].activeToggle.onValueChanged.AddListener(delegate(bool active) {
					OnToggleChanged(active, audioSource);
				});
				specialSounds[i].volumeSlider.onValueChanged.AddListener(delegate(float value) {
					OnSliderChanged(value, audioSource);
				});
				specialSounds[i].audioSource = audioSource;
			}

			for (int i = 0; i < effectSounds.Length; i++)
			{
				int index = i;
				effectSounds[i].button.onClick.AddListener(delegate() {
					StartCoroutine(PlayOneShotSound(audioSourceParent, effectSoundsAsset, index));
				});
			}
		}

		/// <summary>
		/// Callback executed when the different sound slider's changes in complex UI mode
		/// for changing volume.
		/// </summary>
		/// <param name="volume">The new volume.</param>
		/// <param name="audioSource">The AuioSource whose volume is being changed.</param>
		private void OnSliderChanged(float volume, AudioSource audioSource)
		{
			audioSource.volume = volume;
		}

		/// <summary>
		/// Callback executed when the different toggle boxes change in complex UI mode,
		/// for stopping / playing an AudioSource.
		/// </summary>
		/// <param name="active">Toggle AudioSource playing or not playing.</param>
		/// <param name="audioSource">The AudioSource being toggled.</param>
		private void OnToggleChanged(bool active, AudioSource audioSource)
		{
			if (active) { audioSource.Play(); }
			else { audioSource.Stop(); }
		}

		/// <summary>
		/// Callback executed to play a AudioClip from an AudioSource.
		/// </summary>
		/// <param name="audioSourceParent">Parent of AudioSource.</param>
		/// <param name="asset">Asset container containing the sounds.</param>
		/// <param name="index">Index of sound in asset container.</param>
		/// <returns></returns>
		private IEnumerator PlayOneShotSound(GameObject audioSourceParent, SoundScriptableObject asset, int index)
		{
			GameObject origin = new GameObject("Origin");
			origin.transform.parent = audioSourceParent.transform;
			AudioSource audioSource = origin.AddComponent<AudioSource>();
			audioSource.spatialBlend = 1.0f;
			audioSource.clip = asset.soundsAsset[index];
			audioSource.loop = false;
			audioSource.playOnAwake = false;
			audioSource.volume = 1;
			audioSource.minDistance = .3f;
			audioSource.maxDistance = 1;

			origin.transform.position = new Vector3(Random.Range(-effectSpawnRange, effectSpawnRange), Random.Range(-effectSpawnRange, effectSpawnRange), Random.Range(-effectSpawnRange, effectSpawnRange));

			audioSource.Play();

			yield return new WaitUntil(() => !audioSource.isPlaying);

			Destroy(origin);
		}

		private void SetupAudioSource(GameObject audioSourceParent, out AudioSource audioSource, int index, SoundScriptableObject asset)
		{
			audioSource = audioSourceParent.AddComponent<AudioSource>();
			audioSource.spatialBlend = 1.0f;
			audioSource.clip = asset.soundsAsset[index];
			audioSource.loop = true;
			audioSource.playOnAwake = false;
			audioSource.volume = 0;
			audioSource.minDistance = 495;
			audioSource.Play();
		}

		private void SetUpSoundInterface()
		{
			ambienceSounds = new InteractiveSound[ambienceSoundList.Length];

			for (int i = 0; i < ambienceSoundList.Length; i++)
			{
				ambienceSounds[i].text = ambienceSoundList[i].GetComponentInChildren<Text>();
				ambienceSounds[i].volumeSlider = ambienceSoundList[i].GetComponentInChildren<Slider>();
				ambienceSounds[i].activeToggle = ambienceSoundList[i].GetComponentInChildren<Toggle>();
			}

			specialSounds = new InteractiveSound[specialSoundList.Length];

			for (int i = 0; i < specialSoundList.Length; i++)
			{
				specialSounds[i].text = specialSoundList[i].GetComponentInChildren<Text>();
				specialSounds[i].volumeSlider = specialSoundList[i].GetComponentInChildren<Slider>();
				specialSounds[i].activeToggle = specialSoundList[i].GetComponentInChildren<Toggle>();
			}

			effectSounds = new OneShotSound[effectSoundList.Length];

			for (int i = 0; i < effectSoundList.Length; i++)
			{
				effectSounds[i].text = effectSoundList[i].GetComponentInChildren<Text>();
				effectSounds[i].button = effectSoundList[i].GetComponentInChildren<Button>();
			}

		}
	}
}


