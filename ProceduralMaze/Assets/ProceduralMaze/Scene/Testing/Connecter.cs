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

	Vector3 CalculateRotatedPoint(float rotation, bool left)
	{
		float length = prefab1Left.xExtents + prefab2Right.xExtents;
		print(prefab1Left.xExtents);

		Vector3 origin;
		Vector3 end;

		if (left)
		{
			origin = prefab1Left.gameObject.transform.position;
			end = new Vector3(origin.x + length, origin.y, origin.z);
		}
		else
		{
			origin = prefab1Right.gameObject.transform.position;
			end = new Vector3(origin.x - length, origin.y, origin.z);
		}
		
		end = Quaternion.Euler(0, rotation, 0) * end;

		Vector3 endPoint = length * Vector3.Normalize(end - origin) + origin;

		return endPoint;
	}

	void CalculatePosition()
	{
		float xDiff;
		float zDiff;

		float rotation = prefab1.transform.eulerAngles.y;
		prefab2.transform.eulerAngles = new Vector3(0, rotation, 0);

		if (left)
		{
			float newPosX = CalculateRotatedPoint(rotation, true).x;
			float newPosZ = CalculateRotatedPoint(rotation, true).z;

			float oldPosX = prefab2Right.gameObject.transform.position.x;
			float oldPosZ = prefab2Right.gameObject.transform.position.z;

			xDiff = newPosX - oldPosX;
			zDiff = newPosZ - oldPosZ;
		}
		else 
		{
			float newPosX = CalculateRotatedPoint(rotation, false).x;
			float newPosZ = CalculateRotatedPoint(rotation, false).z;

			float oldPosX = prefab2Left.gameObject.transform.position.x;
			float oldPosZ = prefab2Left.gameObject.transform.position.z;

			xDiff = newPosX - oldPosX;
			zDiff = newPosZ - oldPosZ;
		}

		Vector3 original = prefab2.transform.position;
		prefab2.transform.position = new Vector3(original.x + xDiff, original.y, original.z + zDiff);
	}
}
