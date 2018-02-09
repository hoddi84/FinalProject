using System;
using UnityEngine;
using UnityEngine.UI;

namespace MazeUI {

	public class CharacterButtonSelect : MonoBehaviour {

		public Action<GameObject, bool> onFrameSelected = null;
		private bool _isSelected = false;

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
					onFrameSelected(gameObject, true);
				}
			}
			else
			{
				ShowFrameOutline(false);
				if (onFrameSelected != null)
				{
					onFrameSelected(gameObject, false);
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


