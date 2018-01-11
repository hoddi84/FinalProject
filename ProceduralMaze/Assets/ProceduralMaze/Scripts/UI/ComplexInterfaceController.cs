using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComplexInterfaceController : MonoBehaviour {

	public Slider lightIntensitySlider;
	public Slider lightAmbienceSlider;

	private LightManager lightManager;

	public Action<float> onLightIntensityChanged = null;
	public Action<float> onAmbienceIntensityChanged = null;

	public GameObject lightingControls;
	public GameObject soundControls;
	public GameObject frameControls;
	public GameObject frameContents;
	public GameObject frameContentPrefab;
	public FrameAssetsSriptableObject frameAssets;

	private List<Sprite> activeFrames = new List<Sprite>();

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;

		lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
		lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbienceIntensity);
	}

	void Start()
	{
		Initialize();
	}

	void Initialize()
	{
		lightingControls.SetActive(true);
		soundControls.SetActive(false);

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

	public void EnableSoundPanel()
	{
		soundControls.SetActive(true);
		lightingControls.SetActive(false);
		frameControls.SetActive(false);
	}

	public void EnableLightingPanel()
	{
		lightingControls.SetActive(true);
		soundControls.SetActive(false);
		frameControls.SetActive(false);
	}

	public void EnableFrameControls()
	{
		frameControls.SetActive(true);
		lightingControls.SetActive(false);
		soundControls.SetActive(false);
	}

	private void PopulateFrameView()
	{
		foreach (Sprite sprite in frameAssets.healthyFlowers)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			t.transform.SetParent(frameContents.transform);
		}

		foreach (Sprite sprite in frameAssets.dyingFlowers)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			t.transform.SetParent(frameContents.transform);
		}

		foreach (Sprite sprite in frameAssets.scary)
		{
			GameObject t = Instantiate(frameContentPrefab);
			t.GetComponent<Image>().sprite = sprite;
			t.transform.SetParent(frameContents.transform);
		}
	}

	public void AddToActiveFrames(GameObject obj)
	{
		activeFrames.Add(obj.GetComponent<Image>().sprite);

		foreach (Sprite sprite in activeFrames)
		{
			print(sprite.name);
		}
	}

	public void RemoveFromActiveFrames(GameObject obj)
	{
		activeFrames.Remove(obj.GetComponent<Image>().sprite);

		foreach (Sprite sprite in activeFrames)
		{
			print(sprite.name);
		}
	}
}
