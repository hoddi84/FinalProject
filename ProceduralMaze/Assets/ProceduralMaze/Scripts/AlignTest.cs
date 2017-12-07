using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignTest : MonoBehaviour {

	BoxCollider coll;
	void Start () {
		
		coll = GetComponentInChildren<BoxCollider>();

		float xExtents = coll.bounds.extents.x;
		float yExtents = coll.bounds.extents.z;

		print("X: " + xExtents);
		print("Z: " + yExtents);
	}
}
