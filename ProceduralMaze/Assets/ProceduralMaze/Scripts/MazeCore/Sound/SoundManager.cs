using UnityEngine;
using UnityEngine.UI;
using MazeUI;

namespace MazeCore.Sound {

	/// <summary>
	/// Container for ambience sounds used in complex mode.
	/// </summary>
	[System.Serializable]
	public struct InteractiveSound {
		public Text text;
		public Toggle activeToggle;
		public Slider volumeSlider;

		[HideInInspector]
		public AudioSource audioSource;
	}

	/// <summary>
	/// Container for one shot sounds used in complex mode.
	/// </summary>
	[System.Serializable]
	public struct OneShotSound {
		public Text text;
		public Button button;
	}

	/// <summary>
	/// Container for mixing sounds in simple mode.
	/// </summary>
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

	public partial class SoundManager : MonoBehaviour {

		private SimpleInterfaceController _simpleController;
		private const string AMBIENCESOUNDS = "AMBIENCESOUNDS";

		[Header("Sound Assets")]
		public SoundScriptableObject ambienceSoundAsset;
		public SoundScriptableObject specialSoundsAsset;
		public SoundScriptableObject effectSoundsAsset;

		[Header("Simple Mode")]
		public AudioUnit[] audioUnits;

		[Header("Complex Mode")]
		public InteractiveSound[] ambienceSounds;
		public InteractiveSound[] specialSounds;
		public OneShotSound[] effectSounds;

		[Range(1.0f, 5.0f)]
		public float effectSpawnRange;

		[Header("Sound Interface")]
		public GameObject[] ambienceSoundList;
		public GameObject[] specialSoundList;
		public GameObject[] effectSoundList;

		private void Awake()
		{
			_simpleController = FindObjectOfType<SimpleInterfaceController>();
		}

		private void OnEnable()
		{
			if (_simpleController != null)
			{
				_simpleController.onScarySliderChanged += OnScarySliderChanged;
			}
		}

		private void OnDisable()
		{
			if (_simpleController != null)
			{
				_simpleController.onScarySliderChanged -= OnScarySliderChanged;
			}
		}

		private void Start()
		{
			GameObject t = GameObject.Find(AMBIENCESOUNDS);
			if (t == null)
			{
				t = new GameObject(AMBIENCESOUNDS);
			}

			SetUpSoundInterface();

			InitializeSounds(t);

			SetupAudioUnit();
		}
	}
}


