using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour {

	private enum EntryAxis { X, Y, Z }
	private EntryAxis entryAxis;
	// Use this for initialization
	void Start () {
		
		FindAxisOfEntry();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			print("LOL");
		}

		print("Touched");
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			print("LOL2");
		}
	}
	
	void CheckWayMessage(float entry, float exit)
	{
		float maxDiff = .16f;
		//print("entry: " + entry);
		//print("exit: " + exit);
		print("diff: " + Mathf.Abs(entry - exit));
		if (Mathf.Abs(entry - exit) <= maxDiff)
		{
			print("Came out same way");
		}
		else
		{
			print("Came out other way");
		}
	}

	void CheckWay(Vector3 entryVector, Vector3 outVector, EntryAxis axis)
	{
		switch (axis) 
		{
			case EntryAxis.X:
				CheckWayMessage(entryVector.x, outVector.x);
				break;

			case EntryAxis.Y:
				CheckWayMessage(entryVector.y, outVector.y);
				break;

			case EntryAxis.Z:
				CheckWayMessage(entryVector.z, outVector.z);
				break;
		}
	}

	void FindAxisOfEntry()
	{
		float smallest = 0;

		float x = gameObject.GetComponent<Collider>().bounds.extents.x;
		float y = gameObject.GetComponent<Collider>().bounds.extents.y;
		float z = gameObject.GetComponent<Collider>().bounds.extents.z;

		if (x < y)
		{
			smallest = x;
			entryAxis = EntryAxis.X;
		}
		else 
		{
			smallest = y;
			entryAxis = EntryAxis.Y;
		}

		if (z < smallest)
		{
			smallest = z;
			entryAxis = EntryAxis.Z;
		}
	}
}
