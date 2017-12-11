using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct InteractiveSound {
	public Text text;
	public Toggle activeToggle;
	public Slider volumeSlider;

	[HideInInspector]
	public AudioSource audioSource;
}

public class SoundManager : MonoBehaviour {

	public GameObject soundPanel;

	private bool showSoundPanel = false;

	public SoundScriptableObject ambienceSoundAsset;
	public SoundScriptableObject specialSoundsAsset;

	public InteractiveSound[] ambienceSound;
	public InteractiveSound[] specialSound;

	void Start()
	{
		GameObject t = GameObject.Find("AMBIENCESOUNDS");
		if (t == null)
		{
			t = new GameObject("AMBIENCESOUNDS");
		}

		InitializeSounds(t);
	}

	void InitializeSounds(GameObject audioSourceAttached)
	{
		for (int i = 0; i < ambienceSound.Length; i++)
		{
			AudioSource source = audioSourceAttached.AddComponent<AudioSource>();
			SetupAudioSource(source, i, ambienceSoundAsset);

			ambienceSound[i].activeToggle.onValueChanged.AddListener(delegate(bool active) {
				OnToggleChanged(active, source);
			});
			ambienceSound[i].volumeSlider.onValueChanged.AddListener(delegate(float value) {
				OnSliderChanged(value, source);
			});
			ambienceSound[i].audioSource = source;
		}

		for (int i = 0; i < specialSound.Length; i++)
		{
			AudioSource source = audioSourceAttached.AddComponent<AudioSource>();
			SetupAudioSource(source, i, specialSoundsAsset);

			specialSound[i].activeToggle.onValueChanged.AddListener(delegate(bool active) {
				OnToggleChanged(active, source);
			});
			specialSound[i].volumeSlider.onValueChanged.AddListener(delegate(float value) {
				OnSliderChanged(value, source);
			});
			specialSound[i].audioSource = source;
		}
	}

	private void OnSliderChanged(float volume, AudioSource source)
	{
		source.volume = volume;
	}

	private void OnToggleChanged(bool active, AudioSource source)
	{
		if (active)
		{
			source.Play();
		}
		else
		{
			source.Stop();
		}
	}

	public void ToggleSoundPanel()
	{
		showSoundPanel = !showSoundPanel;

		if (showSoundPanel)
		{
			soundPanel.SetActive(true);
		}
		else
		{
			soundPanel.SetActive(false);
		}
	}

	void SetupAudioSource(AudioSource source, int index, SoundScriptableObject asset)
	{
		source.spatialBlend = 1.0f;
		source.clip = asset.soundsAsset[index];
		source.loop = true;
		source.playOnAwake = false;
		source.volume = 0;
		source.minDistance = 495;
		source.Play();
	}
}
