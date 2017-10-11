using System;
using UnityEngine;

public enum TestUnit
{
    NULL,
    TypeA,
    TypeB,
    TypeC,
    TypeD,
    TypeD1,
    TypeD2,
    TypeD3,
    TypeD4,
    TypeD5,
    TypeD6,
    TypeD7,
    TypeD8,
    TypeD9,
    TypeD10,
    TypeE,
    TypeE1,
    TypeE2,
    TypeE3,
    TypeE4,
    TypeE5,
    TypeE6,
    TypeF,
    TypeF1,
    TypeF2,
    TypeF3,
    TypeF4,
    TypeG,
    TypeG1,
    TypeH,
    TypeH1,
    TypeH2,
    TypeH3,
    TypeH4,
    TypeH5,
    TypeH6,
    TypeH7,
    TypeK,
    TypeK1,
}

public enum EntryAxis 
{
	X,
	Y,
	Z,
}

public class UnitTrigger : MonoBehaviour {

    private const string PLAYER = "Player";

    public Action<UnitTrigger> onTriggerEntered = null;

    public TestUnit fromType;
    public TestUnit isType;
    public TestUnit toType;


    //new
    private EntryAxis entryAxis;
	private Vector3 entryVector;
	private Vector3 outVector;

/* 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == PLAYER)
        {
            if (onTriggerEntered != null)
            {
                onTriggerEntered(this);
            }
        }
    }
*/
    void Start()
    {
        FindAxisOfEntry();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			print("enter");
			//print(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));

			entryVector = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);
			
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			print("exit");
			//print(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));

			outVector = other.gameObject.GetComponent<Collider>().ClosestPoint(transform.position);

			CheckWay(entryVector, outVector, entryAxis);
		}
	}

	void CheckWayMessage(float entry, float exit)
	{
		float maxDiff = .1f;
        print("entry: " + entry);
        print("exit: " + exit);
        print("diff: " + Math.Abs(entry - exit));
		if (Mathf.Abs(entry - exit) <= maxDiff)
		{
			print("Came out same way");
            print(entryAxis);
		}
		else
		{
			print("Came out other way");
            print(entryAxis);
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
