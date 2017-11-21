using UnityEngine;
using UnityEngine.UI;

public class SimpleInterfaceController : MonoBehaviour {

	public Slider slider;
	private LightManager lightManager;
	private UnitFrameManager frameManager;

	void Awake()
	{
		lightManager = FindObjectOfType(typeof(LightManager)) as LightManager;
		frameManager = FindObjectOfType(typeof(UnitFrameManager)) as UnitFrameManager;

		slider.onValueChanged.AddListener(UpdateScarySlider);
	}

	void Start()
	{
		InitializeSlider();
	}

	void UpdateScarySlider(float value)
	{
		lightManager.lightIntensity = value;
		lightManager.ambienceIntensity = value;

		frameManager.frameSliderValue = value;
	}

	void InitializeSlider()
	{
		slider.minValue = 0;
		slider.value = lightManager.lightIntensity;
		slider.maxValue = lightManager.MAX_LIGHT_INTENSITY;
	}
}
