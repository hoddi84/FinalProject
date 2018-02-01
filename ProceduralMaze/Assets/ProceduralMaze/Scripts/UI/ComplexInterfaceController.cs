using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexInterfaceController : MonoBehaviour {

	public Slider lightIntensitySlider;
	public Slider lightAmbienceSlider;
	public Slider decalBloodSlider;

	private LightManager lightManager;

	public Action<float> onLightIntensityChanged = null;
	public Action<float> onAmbienceIntensityChanged = null;
	public Action<float> onDecalBloodAmountChanged = null;

	public GameObject cntrlLighting;
	public GameObject cntrlSound;
	public GameObject cntrlFrames;
	public GameObject cntrlCharacters;
	public GameObject cntrlOther;
	public GameObject frameContents;
	public GameObject frameContentPrefab;
	public FrameAssetsSriptableObject frameAssets;

	private List<Sprite> activeFrames = new List<Sprite>();
	public Action<List<Sprite>> onListChanged = null;
	public Action onSelectAllFrames = null;
	public Action onDeselectAllFrames = null;

	// Tab Buttons
	public GameObject btnLighting;
	public GameObject btnSound;
	public GameObject btnFrame;
	public GameObject btnCharacter;
	public GameObject btnOther;

	private bool btnLightingHover;
	private bool btnSoundHover;
	private bool btnFrameHover;
	private bool btnOtherHover;
	public Color btnDefaultColor;
	public Color btnSelectedColor;
	public Color btnHoverColor;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
		lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbienceIntensity);
		decalBloodSlider.onValueChanged.AddListener(UpdateDecalBloodAmount);
	}

	void Start()
	{
		Initialize();
	}

	void Initialize()
	{
		cntrlLighting.SetActive(true);
		cntrlSound.SetActive(false);
		cntrlFrames.SetActive(false);
		cntrlCharacters.SetActive(false);
		cntrlOther.SetActive(false);

		btnLighting.GetComponent<Image>().color = btnSelectedColor;
		btnSound.GetComponent<Image>().color = btnDefaultColor;
		btnFrame.GetComponent<Image>().color = btnDefaultColor;
		btnCharacter.GetComponent<Image>().color = btnDefaultColor;
		btnOther.GetComponent<Image>().color = btnDefaultColor;

		PopulateFrameView();
	}

	void UpdateLightIntensity(float newLightIntensity)
	{
		if (onLightIntensityChanged != null)
		{
			onLightIntensityChanged(newLightIntensity);
		}
	}

	void UpdateAmbienceIntensity(float newAmbienceIntensity)
	{
		if (onAmbienceIntensityChanged != null)
		{
			onAmbienceIntensityChanged(newAmbienceIntensity);
		}
	}

	void UpdateDecalBloodAmount(float newValue)
	{
		if (onDecalBloodAmountChanged != null)
		{
			onDecalBloodAmountChanged(newValue);
		}
	}

	public void EnableSoundPanel()
	{
		cntrlLighting.SetActive(false);
		cntrlSound.SetActive(true);
		cntrlFrames.SetActive(false);
		cntrlCharacters.SetActive(false);
		cntrlOther.SetActive(false);

		btnLighting.GetComponent<Image>().color = btnDefaultColor;
		btnSound.GetComponent<Image>().color = btnSelectedColor;
		btnFrame.GetComponent<Image>().color = btnDefaultColor;
		btnCharacter.GetComponent<Image>().color = btnDefaultColor;
		btnOther.GetComponent<Image>().color = btnDefaultColor;
	}

	public void EnableLightingPanel()
	{
		cntrlLighting.SetActive(true);
		cntrlSound.SetActive(false);
		cntrlFrames.SetActive(false);
		cntrlCharacters.SetActive(false);
		cntrlOther.SetActive(false);

		btnLighting.GetComponent<Image>().color = btnSelectedColor;
		btnSound.GetComponent<Image>().color = btnDefaultColor;
		btnFrame.GetComponent<Image>().color = btnDefaultColor;
		btnCharacter.GetComponent<Image>().color = btnDefaultColor;
		btnOther.GetComponent<Image>().color = btnDefaultColor;
	}

	public void EnableFrameControls()
	{
		cntrlLighting.SetActive(false);
		cntrlSound.SetActive(false);
		cntrlFrames.SetActive(true);
		cntrlCharacters.SetActive(false);
		cntrlOther.SetActive(false);

		btnLighting.GetComponent<Image>().color = btnDefaultColor;
		btnSound.GetComponent<Image>().color = btnDefaultColor;
		btnFrame.GetComponent<Image>().color = btnSelectedColor;
		btnCharacter.GetComponent<Image>().color = btnDefaultColor;
		btnOther.GetComponent<Image>().color = btnDefaultColor;
	}

	public void EnableCharacterControls()
	{
		cntrlLighting.SetActive(false);
		cntrlSound.SetActive(false);
		cntrlFrames.SetActive(false);
		cntrlCharacters.SetActive(true);
		cntrlOther.SetActive(false);

		btnLighting.GetComponent<Image>().color = btnDefaultColor;
		btnSound.GetComponent<Image>().color = btnDefaultColor;
		btnFrame.GetComponent<Image>().color = btnDefaultColor;
		btnCharacter.GetComponent<Image>().color = btnSelectedColor;
		btnOther.GetComponent<Image>().color = btnDefaultColor;
	}

	public void EnableOtherControls()
	{
		cntrlLighting.SetActive(false);
		cntrlSound.SetActive(false);
		cntrlFrames.SetActive(false);
		cntrlCharacters.SetActive(false);
		cntrlOther.SetActive(true);

		btnLighting.GetComponent<Image>().color = btnDefaultColor;
		btnSound.GetComponent<Image>().color = btnDefaultColor;
		btnFrame.GetComponent<Image>().color = btnDefaultColor;
		btnCharacter.GetComponent<Image>().color = btnDefaultColor;
		btnOther.GetComponent<Image>().color = btnSelectedColor;
	}

	private void PopulateFrameView()
	{
		foreach (Sprite sprite in frameAssets.healthyFlowers)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			onSelectAllFrames += t.GetComponent<FrameButtonScript>().SelectFrame;
			onDeselectAllFrames += t.GetComponent<FrameButtonScript>().DeselectFrame;
			t.transform.SetParent(frameContents.transform);
		}

		foreach (Sprite sprite in frameAssets.dyingFlowers)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			onSelectAllFrames += t.GetComponent<FrameButtonScript>().SelectFrame;
			onDeselectAllFrames += t.GetComponent<FrameButtonScript>().DeselectFrame;
			t.transform.SetParent(frameContents.transform);
		}

		foreach (Sprite sprite in frameAssets.scary)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			onSelectAllFrames += t.GetComponent<FrameButtonScript>().SelectFrame;
			onDeselectAllFrames += t.GetComponent<FrameButtonScript>().DeselectFrame;
			t.transform.SetParent(frameContents.transform);
		}
	}

	public void AddToActiveFrames(GameObject obj)
	{
		activeFrames.Add(obj.GetComponent<Image>().sprite);

		if (onListChanged != null)
		{
			onListChanged(activeFrames);
		}
	}

	public void RemoveFromActiveFrames(GameObject obj)
	{
		activeFrames.Remove(obj.GetComponent<Image>().sprite);

		if (onListChanged != null)
		{
			onListChanged(activeFrames);
		}
	}

	public void SelectAllFrames()
	{
		if (onSelectAllFrames != null)
		{
			onSelectAllFrames();
		}

		foreach (Sprite sprite in frameAssets.healthyFlowers)
		{
			activeFrames.Add(sprite);
		}
		foreach (Sprite sprite in frameAssets.dyingFlowers)
		{
			activeFrames.Add(sprite);
		}
		foreach (Sprite sprite in frameAssets.scary)
		{
			activeFrames.Add(sprite);
		}

		if (onListChanged != null)
		{
			onListChanged(activeFrames);
		}
	}

	public void DeselectAllFrames()
	{
		if (onDeselectAllFrames != null)
		{
			onDeselectAllFrames();
		}

		activeFrames.Clear();

		if (onListChanged != null)
		{
			onListChanged(activeFrames);
		}
	}
}
