using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

	private EntryAxis entryAxis;
	private Vector3 entryVector;
	private Vector3 outVector;

	void Start()
	{
		FindAxisOfEntry();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			print("enter");
			//print(other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position));

			entryVector = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			print("exit");
			//print(other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position));

			outVector = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);

			CheckWay(entryVector, outVector, entryAxis);
		}
	}

	void CheckWayMessage(float entry, float exit)
	{
		float maxDiff = .1f;
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
