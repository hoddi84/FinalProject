using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MazeCore.Lighting;

namespace MazeUI {

	public class ComplexInterfaceController : MonoBehaviour {

		public Slider lightIntensitySlider;
		public Slider lightAmbienceSlider;
		public Slider decalBloodSlider;

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

		private List<Sprite> _activeFrameList = new List<Sprite>();
		public Action<List<Sprite>> onFrameListChanged = null;
		public Action onSelectAllFrames = null;
		public Action onDeselectAllFrames = null;

		// Tab Buttons
		public GameObject btnLighting;
		public GameObject btnSound;
		public GameObject btnFrame;
		public GameObject btnCharacter;
		public GameObject btnOther;

		public Color btnDefaultColor;
		public Color btnSelectedColor;

		private List<GameObject> _listOfControlPanels = null;
		private List<GameObject> _listOfButtonPanels = null;
		
		private void OnEnable()
		{
			lightIntensitySlider.onValueChanged.AddListener(UpdateLightIntensity);
			lightAmbienceSlider.onValueChanged.AddListener(UpdateAmbienceIntensity);
			decalBloodSlider.onValueChanged.AddListener(UpdateDecalBloodAmount);
		}

		private void OnDisable()
		{
			lightIntensitySlider.onValueChanged.RemoveListener(UpdateLightIntensity);
			lightAmbienceSlider.onValueChanged.RemoveListener(UpdateAmbienceIntensity);
			decalBloodSlider.onValueChanged.RemoveListener(UpdateDecalBloodAmount);
		}

		private void Start()
		{
			_listOfControlPanels = new List<GameObject>(){
				cntrlLighting,
				cntrlSound,
				cntrlFrames,
				cntrlCharacters, 
				cntrlOther
			};

			_listOfButtonPanels = new List<GameObject>(){
				btnLighting,
				btnSound,
				btnFrame,
				btnCharacter,
				btnOther
			};

			Initialize();
		}

		private void Initialize()
		{
			ActivatePanel(cntrlLighting, btnLighting);

			PopulateFrameView();
		}

		private void UpdateLightIntensity(float newLightIntensity)
		{
			if (onLightIntensityChanged != null)
			{
				onLightIntensityChanged(newLightIntensity);
			}
		}

		private void UpdateAmbienceIntensity(float newAmbienceIntensity)
		{
			if (onAmbienceIntensityChanged != null)
			{
				onAmbienceIntensityChanged(newAmbienceIntensity);
			}
		}

		private void UpdateDecalBloodAmount(float newValue)
		{
			if (onDecalBloodAmountChanged != null)
			{
				onDecalBloodAmountChanged(newValue);
			}
		}

		/// <summary>
		/// Activate the currently selected panel in the complex interface UI
		/// mode.
		/// </summary>
		/// <param name="activeControl">The active controls.</param>
		/// <param name="activeButton">The selected button for the active control.</param>
		private void ActivatePanel(GameObject activeControl, GameObject activeButton)
		{
			activeControl.SetActive(true);
			activeButton.GetComponent<Image>().color = btnSelectedColor;

			foreach (GameObject control in _listOfControlPanels)
			{
				if (control.GetInstanceID() != activeControl.GetInstanceID())
				{
					control.SetActive(false);
				}
			}

			foreach (GameObject panel in _listOfButtonPanels)
			{
				if (panel.GetInstanceID() != activeButton.GetInstanceID())
				{
					panel.GetComponent<Image>().color = btnDefaultColor;
				}
			}
		}

		public void EnableSoundPanel()
		{
			ActivatePanel(cntrlSound, btnSound);
		}

		public void EnableLightingPanel()
		{
			ActivatePanel(cntrlLighting, btnLighting);
		}

		public void EnableFrameControls()
		{
			ActivatePanel(cntrlFrames, btnFrame);
		}

		public void EnableCharacterControls()
		{
			ActivatePanel(cntrlCharacters, btnCharacter);
		}

		public void EnableOtherControls()
		{
			ActivatePanel(cntrlOther, btnOther);
		}

		/// <summary>
		/// Populate the frame panel with frames.
		/// </summary>
		private void PopulateFrameView()
		{
			foreach (Sprite sprite in frameAssets.healthyFlowers)
			{
				GameObject t = Instantiate(frameContentPrefab);
				SetUpFrame(t, sprite);
			}

			foreach (Sprite sprite in frameAssets.dyingFlowers)
			{
				GameObject t = Instantiate(frameContentPrefab);
				SetUpFrame(t, sprite);
			}

			foreach (Sprite sprite in frameAssets.scary)
			{
				GameObject t = Instantiate(frameContentPrefab);
				SetUpFrame(t, sprite);
			}
		}

		private void SetUpFrame(GameObject frame, Sprite sprite)
		{
			frame.GetComponent<Image>().sprite = sprite;
			frame.transform.SetParent(frameContents.transform);
			onSelectAllFrames += frame.GetComponent<FrameButtonScript>().SelectFrame;
			onDeselectAllFrames += frame.GetComponent<FrameButtonScript>().DeselectFrame;
		}

		public void AddToActiveFrames(GameObject frame)
		{
			_activeFrameList.Add(frame.GetComponent<Image>().sprite);

			if (onFrameListChanged != null)
			{
				onFrameListChanged(_activeFrameList);
			}
		}

		public void RemoveFromActiveFrames(GameObject frame)
		{
			_activeFrameList.Remove(frame.GetComponent<Image>().sprite);

			if (onFrameListChanged != null)
			{
				onFrameListChanged(_activeFrameList);
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
				_activeFrameList.Add(sprite);
			}
			foreach (Sprite sprite in frameAssets.dyingFlowers)
			{
				_activeFrameList.Add(sprite);
			}
			foreach (Sprite sprite in frameAssets.scary)
			{
				_activeFrameList.Add(sprite);
			}

			if (onFrameListChanged != null)
			{
				onFrameListChanged(_activeFrameList);
			}
		}

		public void DeselectAllFrames()
		{
			if (onDeselectAllFrames != null)
			{
				onDeselectAllFrames();
			}

			_activeFrameList.Clear();

			if (onFrameListChanged != null)
			{
				onFrameListChanged(_activeFrameList);
			}
		}
	}
}


