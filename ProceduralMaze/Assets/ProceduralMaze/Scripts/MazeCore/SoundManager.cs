using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public GameObject soundPanel;

	private bool showSoundPanel = false;

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
}
