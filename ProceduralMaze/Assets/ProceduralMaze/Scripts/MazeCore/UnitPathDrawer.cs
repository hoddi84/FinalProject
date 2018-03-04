using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathDrawer : MonoBehaviour {

	private MeshCollider meshCollider;
	private LineRenderer lineRenderer;

	public Material lineMaterial;

	private void Awake()
	{
		if (lineRenderer == null)
		{
			gameObject.AddComponent<LineRenderer>();
			lineRenderer = GetComponent<LineRenderer>();
		}

		meshCollider = gameObject.GetComponent<MeshCollider>();
	}

	private void Start()
	{
		Vector3 center = meshCollider.bounds.center;

		float lengthX = meshCollider.bounds.extents.x;
		float lengthZ = meshCollider.bounds.extents.z;

		float length;

		length = (lengthX > lengthZ) ? lengthX : lengthZ;

		Vector3 leftX = new Vector3(center.x - length, center.y, center.z);
		Vector3 rightX = new Vector3(center.x + length, center.y, center.z);

		Vector3 leftVec = leftX - center;
		Vector3 rightVec = rightX - center;

		leftVec = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * leftVec;
		rightVec = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * rightVec;

		Vector3 leftPoint = leftVec + center;
		Vector3 rightPoint = rightVec + center;

		leftPoint += transform.forward/5;
		rightPoint += transform.forward/5;

		SetUpLineRenderer(leftPoint, rightPoint);

	}

	private void SetUpLineRenderer(Vector3 leftPosition, Vector3 rightPosition)
	{
		lineRenderer.positionCount = 2;
		lineRenderer.receiveShadows = false;
		lineRenderer.material = lineMaterial;
		lineRenderer.startWidth = .05f;
		lineRenderer.endWidth = .05f;
		lineRenderer.SetPositions(
			new Vector3[] {
				leftPosition,
				rightPosition
			}
		);
	}
}
