using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour {

	public GameObject wall;

	private MeshCollider collider;

	void Start()
	{
		collider = wall.GetComponent<MeshCollider>();

		print(collider.bounds.extents.y);
	}
}
