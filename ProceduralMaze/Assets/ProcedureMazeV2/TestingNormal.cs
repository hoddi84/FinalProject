using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingNormal : MonoBehaviour {

	public Vector3 normal;
	// Use this for initialization
	void Start () {
		
		normal = gameObject.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		
		normal = gameObject.transform.forward;
	}
}
