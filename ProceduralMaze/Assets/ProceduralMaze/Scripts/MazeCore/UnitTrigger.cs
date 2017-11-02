using System;
using UnityEngine;

public enum EntryAxis 
{
	X,
	Y,
	Z,
}

public class UnitTrigger : MonoBehaviour {

    private const string PLAYER = "Player";
    public Action<UnitTrigger> onTriggerEntered = null;

	public string fromType;
	public string isType;
	public string toType;

    //new
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
			entryVector = other.transform.position;	
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			outVector = other.transform.position;
			CheckWay(entryVector, outVector, entryAxis);
		}
	}

	void CheckWayMessage(float entry, float exit)
	{
		float maxDiff = .15f;
		if (Mathf.Abs(entry - exit) <= maxDiff)
		{
			//print("Came out same way");
		}
		else
		{
			//print("Came out other way");
            if (onTriggerEntered != null)
            {
                onTriggerEntered(this);
            }
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
