using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureSpawner : MonoBehaviour {

	public Sprite[] flowersHealthy;
	public Sprite[] flowersDying;
	private SpriteRenderer renderer;
	private PropManager propManager;
	
	void Awake()
	{
		propManager = FindObjectOfType(typeof(PropManager)) as PropManager;

		renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		int rndIndex = Random.Range(0, flowersHealthy.Length);
		renderer.sprite = flowersHealthy[rndIndex];
	}
}
