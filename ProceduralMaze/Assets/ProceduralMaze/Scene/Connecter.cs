using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connecter : MonoBehaviour {

	public GameObject prefab1;
	public GameObject prefab2;

	private ConnectLeft prefab1Left;
	private ConnectRight prefab1Right;
	private ConnectLeft prefab2Left;
	private ConnectRight prefab2Right;
	public bool left = true;

	void Awake()
	{
		prefab1Left = prefab1.GetComponentInChildren<ConnectLeft>();
		prefab1Right = prefab1.GetComponentInChildren<ConnectRight>();

		prefab2Left = prefab2.GetComponentInChildren<ConnectLeft>();
		prefab2Right = prefab2.GetComponentInChildren<ConnectRight>();
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			CalculatePosition();
		}
	}

	void CalculatePosition()
	{
		float xDiff;
		float zDiff;

		if (left)
		{
			float newPosX = prefab1Left.xPos + prefab1Left.xExtents + prefab2Right.xExtents;
			float newPosZ = prefab1Left.zPos;

			float oldPosX = prefab2Right.xPos;
			float oldPosZ = prefab2Right.zPos;

			xDiff = newPosX - oldPosX;
			zDiff = newPosZ - oldPosZ;
		}
		else 
		{
			float newPosX = prefab1Right.xPos - prefab1Right.xExtents - prefab2Left.xExtents;
			float newPosZ = prefab1Right.zPos;

			float oldPosX = prefab2Left.xPos;
			float oldPosZ = prefab2Left.zPos;

			xDiff = newPosX - oldPosX;
			zDiff = newPosZ - oldPosZ;
		}

		Vector3 original = prefab2.transform.position;

		prefab2.transform.position = new Vector3(original.x + xDiff, original.y, original.z + zDiff);
		

	}
}
