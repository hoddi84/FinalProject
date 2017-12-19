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

[System.Serializable]
public struct OneShotSound {
	public Text text;
	public Button button;
}

public class SoundManager : MonoBehaviour {

	[Header("Complex Mode")]
	public GameObject soundPanel;

	private bool showSoundPanel = false;

	public SoundScriptableObject ambienceSoundAsset;
	public SoundScriptableObject specialSoundsAsset;
	public SoundScriptableObject effectSoundsAsset;

	public InteractiveSound[] ambienceSound;
	public InteractiveSound[] specialSound;
	public OneShotSound[] effectSound;

	[Range(1.0f, 5.0f)]
	public float effectSpawnRange;

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

		for (int i = 0; i < effectSound.Length; i++)
		{
			int index = i;
			effectSound[i].button.onClick.AddListener(delegate() {
				StartCoroutine(PlayOneShotSound(audioSourceAttached, effectSoundsAsset, index));
			});
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

	IEnumerator PlayOneShotSound(GameObject audioSourceAttached, SoundScriptableObject asset, int index)
	{
		GameObject origin = new GameObject("Origin");
		origin.transform.parent = audioSourceAttached.transform;
		AudioSource source = origin.AddComponent<AudioSource>();
		source.spatialBlend = 1.0f;
		source.clip = asset.soundsAsset[index];
		source.loop = false;
		source.playOnAwake = false;
		source.volume = 1;
		source.minDistance = .3f;
		source.maxDistance = 1;

		origin.transform.position = new Vector3(Random.Range(-effectSpawnRange, effectSpawnRange), Random.Range(-effectSpawnRange, effectSpawnRange), Random.Range(-effectSpawnRange, effectSpawnRange));

		source.Play();

		yield return new WaitUntil(() => !source.isPlaying);

		Destroy(source);
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
