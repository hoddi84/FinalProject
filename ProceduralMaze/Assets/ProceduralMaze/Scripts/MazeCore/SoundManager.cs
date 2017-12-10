using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct AmbienceSound {
	public Text text;
	public Toggle activeToggle;
	public Slider volumeSlider;
}

public class SoundManager : MonoBehaviour {

	public GameObject soundPanel;

	public AmbienceSoundsScriptableObject ambienceSoundsAsset;

	private bool showSoundPanel = false;

	public AmbienceSound ambienceSound;

	private AudioSource ambienceSource1;

	void Start()
	{
		ActivateAmbience();
		ambienceSound.volumeSlider.onValueChanged.AddListener(OnSliderChanged);
		ambienceSound.activeToggle.onValueChanged.AddListener(OnToggleChanged);
	}

	private void OnSliderChanged(float volume)
	{
		ambienceSource1.volume = volume;
	}

	private void OnToggleChanged(bool active)
	{
		if (active)
		{
			ambienceSource1.Play();
		}
		else
		{
			ambienceSource1.Stop();
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

	void ActivateAmbience()
	{
		GameObject t = GameObject.Find("AmbienceSOunds");
		if (t == null)
		{
			t = new GameObject("AmbienceSounds");
		}

		ambienceSource1 = t.AddComponent<AudioSource>();
		ambienceSource1.spatialBlend = 1.0f;
		ambienceSource1.clip = ambienceSoundsAsset.ambienceSounds[0];
		ambienceSource1.loop = true;
		ambienceSource1.playOnAwake = false;
		ambienceSource1.volume = 0;
		ambienceSource1.Play();
	}
}
