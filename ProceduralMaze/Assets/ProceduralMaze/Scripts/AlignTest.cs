using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTest : MonoBehaviour {

	BoxCollider coll;

	Vector3 colliderCenter;
	Vector3 colliderEnd;
	float xExtents;
	float yExtents;
	float zExtents;
	Vector3 perpendicular;
	float parentRotation;

	Transform parent;
	void Start () {
		
		coll = GetComponentInChildren<BoxCollider>();

		colliderCenter = coll.bounds.center;
		xExtents = coll.bounds.extents.x;
		yExtents = coll.bounds.extents.y;

		parent = gameObject.transform;
		parentRotation = parent.rotation.eulerAngles.y;
	}

	void Update()
	{
		Vector3 pointEndX = new Vector3(colliderCenter.x + xExtents, colliderCenter.y, colliderCenter.z);
		Vector3 pointEndY = new Vector3(colliderCenter.x, colliderCenter.y + yExtents, colliderCenter.z);

		Vector3 widthLine = pointEndX - colliderCenter;

		Debug.DrawLine(colliderCenter, widthLine*5, Color.blue, 100);
	}

	void OnDrawGizmos()
	{
		Vector3 pointEndX = new Vector3(colliderCenter.x + xExtents, colliderCenter.y, colliderCenter.z);
		Vector3 pointEndY = new Vector3(colliderCenter.x, colliderCenter.y + yExtents, colliderCenter.z);

		print(parentRotation);

		// Before we rotate.
		Gizmos.color = Color.red;
		Gizmos.DrawLine(pointEndX, colliderCenter);
		Gizmos.DrawLine(pointEndY, colliderCenter);

		// need to rotate pointEndX to match parent.
		Vector3 dir = pointEndX - colliderCenter;
		dir = Quaternion.Euler(0,parentRotation+90, 0) * dir;
		pointEndX = dir + colliderCenter;

		Gizmos.color = Color.green;
		Gizmos.DrawLine(pointEndX, colliderCenter);
		Gizmos.DrawLine(pointEndY, colliderCenter);

		
	}
}
