using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathDrawer : MonoBehaviour {

	private Collider meshCollider;
	private LineRenderer line;
	private Vector3 leftPosition;
	private Vector3 rightPosition;

	private float extentsX;

	private void Start()
	{
		line = gameObject.GetComponent<LineRenderer>();
		meshCollider = GetComponent<MeshCollider>();

		extentsX = meshCollider.bounds.extents.x;

		leftPosition = new Vector3(transform.position.x + extentsX, transform.position.y, transform.position.z);
		rightPosition = new Vector3(transform.position.x - extentsX, transform.position.y, transform.position.z);

		leftPosition += transform.forward;
		rightPosition += transform.forward;
	}

	private void Update()
	{

		line.SetPositions(
			new Vector3[] {
				leftPosition, 
				rightPosition
			}
		);
	}
}
