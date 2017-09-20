using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureManager : MonoBehaviour {

	public Sprite[] flowersHealthy;
	public Sprite[] flowersDying;

	private SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		
		renderer = GetComponent<SpriteRenderer>();

		int rndIndex = Random.Range(0, flowersHealthy.Length);
		renderer.sprite = flowersHealthy[rndIndex];
	}
}
