using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathDrawer : MonoBehaviour {

	private MeshCollider meshCollider;
	private LineRenderer lineRenderer;
	private UnitPathDrawerManager pathDrawerManager;

	public Material lineMaterial;

	private float _lineWidth;
	private float _lineOffset;
	private Color _lineColor;

	private void Awake()
	{
		if (lineRenderer == null)
		{
			gameObject.AddComponent<LineRenderer>();
			lineRenderer = GetComponent<LineRenderer>();
		}

		meshCollider = gameObject.GetComponent<MeshCollider>();
		pathDrawerManager = FindObjectOfType<UnitPathDrawerManager>();
	}

	private void Start()
	{
		_lineWidth = pathDrawerManager.lineWidth;
		_lineOffset = pathDrawerManager.lineOffset;
		_lineColor = pathDrawerManager.lineColor;

		Vector3 leftPoint;
		Vector3 rightPoint;

		SetUpLinePoints(out leftPoint, out rightPoint);
		SetUpLineRenderer(leftPoint, rightPoint);

	}

	private void Update()
	{
		if (_lineWidth != pathDrawerManager.lineWidth)
		{
			lineRenderer.endWidth = pathDrawerManager.lineWidth;
			lineRenderer.startWidth = pathDrawerManager.lineWidth;
		}

		if (_lineColor != pathDrawerManager.lineColor)
		{
			lineMaterial.color = pathDrawerManager.lineColor;
			_lineColor = pathDrawerManager.lineColor;
		}

		if (_lineOffset != pathDrawerManager.lineOffset)
		{
			_lineOffset = pathDrawerManager.lineOffset;
			
			Vector3 leftPoint;
			Vector3 rightPoint;

			SetUpLinePoints(out leftPoint, out rightPoint);
			SetUpLineRenderer(leftPoint, rightPoint);
		}
	}

	private void SetUpLinePoints(out Vector3 leftPoint, out Vector3 rightPoint)
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

		leftPoint = leftVec + center;
		rightPoint = rightVec + center;

		leftPoint += transform.forward/_lineOffset;
		rightPoint += transform.forward/_lineOffset;
	}

	private void SetUpLineRenderer(Vector3 leftPosition, Vector3 rightPosition)
	{
		lineRenderer.positionCount = 2;
		lineRenderer.receiveShadows = false;
		lineRenderer.material = lineMaterial;
		lineRenderer.startWidth = _lineWidth;
		lineRenderer.endWidth = _lineWidth;
		lineRenderer.SetPositions(
			new Vector3[] {
				leftPosition,
				rightPosition
			}
		);
	}
}
