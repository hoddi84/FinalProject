using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPathDrawerManager : MonoBehaviour {

	[Range(0.0f, 60.0f)]
	public float lineOffset;
	[Range(0.0f, 1.0f)]
	public float lineWidth;
	public Color lineColor;
	public bool showPath = true;
}
