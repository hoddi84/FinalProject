using System;
using UnityEngine;
using UnityEngine.UI;

namespace MazeUI {

	public class FrameButtonScript : MonoBehaviour {

		private ComplexInterfaceController _complexController;

		private Action<GameObject> onFrameSelected = null;
		private Action<GameObject> onFrameDeselected = null;
		private bool _isSelected = false;

		private void Awake()
		{
			_complexController = FindObjectOfType<ComplexInterfaceController>();
		}

		private void OnEnable()
		{
			onFrameSelected = _complexController.AddToActiveFrames;
			onFrameDeselected = _complexController.RemoveFromActiveFrames;
		}

		private void OnDisable()
		{
			onFrameSelected = null;
			onFrameDeselected = null;
		}

		private void Start()
		{
			Initialize();
		}

		private void Initialize()
		{
			ShowFrameOutline(false);
		}

		public void ClickOnFrame()
		{
			_isSelected = !_isSelected;

			if (_isSelected)
			{
				ShowFrameOutline(true);
				if (onFrameSelected != null)
				{
					onFrameSelected(gameObject);
				}
			}
			else
			{
				ShowFrameOutline(false);
				if (onFrameDeselected != null)
				{
					onFrameDeselected(gameObject);
				}
			}
		}

		public void SelectFrame()
		{
			_isSelected = true;
			ShowFrameOutline(true);
		}

		public void DeselectFrame()
		{
			_isSelected = false;
			ShowFrameOutline(false);
		}

		private void ShowFrameOutline(bool show)
		{
			GetComponent<Outline>().enabled = show;
		}
	}
}


