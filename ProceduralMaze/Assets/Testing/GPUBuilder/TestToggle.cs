using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestToggle : MonoBehaviour {

	private GPUSort sort;
	public GameObject obj;
	private bool showInstanced = true;

	void Awake()
	{
		sort = FindObjectOfType<GPUSort>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			showInstanced = !showInstanced;

			if (showInstanced)
			{
				sort.enabled = true;
				obj.SetActive(false);
			}
			else
			{
				sort.enabled = false;
				obj.SetActive(true);
			}
		}
	}
}
