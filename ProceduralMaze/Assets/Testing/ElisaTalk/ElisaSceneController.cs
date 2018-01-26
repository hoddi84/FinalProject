using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElisaSceneController : MonoBehaviour {

	private Animator animator;
	
	private bool isWaving = false;
	private bool isDancing = false;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (isWaving)
			{
				animator.SetBool("Wave", false);
				isWaving = false;
			}
			else
			{
				animator.SetBool("Wave",true);
				isWaving = true;
			}
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			if (isDancing)
			{
				animator.SetBool("Dance", false);
				isDancing = false;
			}
			else
			{
				animator.SetBool("Dance", true);
				isDancing = true;
			}
		}
	}
}
