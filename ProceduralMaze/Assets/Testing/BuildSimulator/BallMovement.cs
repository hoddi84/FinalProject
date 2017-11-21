using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
	
	private Rigidbody rigidbody;
	public Action onCollision = null;
	private Action<Color> onCubeColorChanged = null;
	private Action<float> onBallSizeChanged = null;
	// Update is called once per frame
	public float force = 0;
	public Material instanceMaterial;
	public Color cubeColor;
	private Color cubeColordefault;
	[Range(0.0f, 150.0f)]
	public float ballSize;
	private float ballSizeDefaultSize;

	void Start() 
	{
		rigidbody = GetComponent<Rigidbody>();

		cubeColordefault = instanceMaterial.GetColor("_CubeColor");
		ballSizeDefaultSize = instanceMaterial.GetFloat("_BallSize");

		onCubeColorChanged += ChangeCubeColor;
		onBallSizeChanged += ChangeBallSize;
	}

	void Update()
	{
		if (cubeColordefault != cubeColor) 
		{
			onCubeColorChanged(cubeColor);
			cubeColordefault = cubeColor;
		}

		if (ballSizeDefaultSize != ballSize) 
		{
			onBallSizeChanged(ballSize);
			ballSizeDefaultSize = ballSize;
		}
	}

	void ChangeCubeColor(Color newColor)
	{
		instanceMaterial.SetColor("_CubeColor", newColor);
	}

	void ChangeBallSize(float newSize)
	{
		instanceMaterial.SetFloat("_BallSize", newSize);
	}

	void FixedUpdate () 
	{
		if (Input.GetKeyDown(KeyCode.E)) 
		{
			Vector3 rndForce = new Vector3(UnityEngine.Random.Range(-1.0f,1.0f)*force, UnityEngine.Random.Range(-1.0f,1.0f)*force, UnityEngine.Random.Range(-1.0f,1.0f)*force);
			rigidbody.AddForce(rndForce, ForceMode.Acceleration);
		}	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (onCollision != null) 
		{
			onCollision();
		}
	}
}
