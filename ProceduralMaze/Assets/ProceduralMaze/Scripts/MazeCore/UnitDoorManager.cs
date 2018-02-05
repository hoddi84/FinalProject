using UnityEngine;

namespace MazeCore.Door {

	public class UnitDoorManager : MonoBehaviour {

		public float doorCloseTime;
		public float doorOpenTime;
		public float rotationAngle;

		public AudioClip openDoorClip;
		public AudioClip openHandleClip;
		public AudioClip closeDoorClip;
		public Sprite doorSpriteClosed;
		public Sprite doorSpriteOpen;
		public Sprite doorSpriteLockOpen;
		public Sprite doorSpriteLockClosed;

		[Range(0.0f, 1.0f)]
		public float presenceSliderValue;
		private float _presenceSliderValue;

		[Range(0.0f, 1.0f)]
		public float scarySliderValue;
		private float _scarySliderValue;

		private SimpleInterfaceController _simpleController;

		private void Awake()
		{
			_simpleController = FindObjectOfType<SimpleInterfaceController>();
		}

		private void OnEnable()
		{
			if (_simpleController != null)
			{
				_simpleController.onPresenceSliderChanged += UpdatePresenceValue;
				_simpleController.onScarySliderChanged += UpdateScaryValue;
			}
		}

		private void OnDisable()
		{
			if (_simpleController != null)
			{
				_simpleController.onPresenceSliderChanged -= UpdatePresenceValue;
				_simpleController.onScarySliderChanged -= UpdateScaryValue;
			}
		}

		private void Update()
		{
			if (_scarySliderValue != scarySliderValue)
			{
				UpdateScaryValue(scarySliderValue);
			}

			if (_presenceSliderValue != presenceSliderValue)
			{
				UpdatePresenceValue(presenceSliderValue);
			}
		}

		private void UpdateScaryValue(float newScaryValue)
		{
			scarySliderValue = newScaryValue;
			_scarySliderValue = scarySliderValue;
		}

		private void UpdatePresenceValue(float newPresenceValue)
		{
			presenceSliderValue = newPresenceValue;
			_presenceSliderValue = presenceSliderValue;
		}
	}
}


