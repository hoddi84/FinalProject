using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour {

	public GameObject rect;
	public Camera overViewCamera;

	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	private float rectWidth;
	private float rectHeight;

	public Action<Camera, Vector3> onHit = null;

	void Start()
	{
		minX = rect.transform.GetComponent<RectTransform>().rect.min.x;
		maxX = rect.transform.GetComponent<RectTransform>().rect.max.x;
		minY = rect.transform.GetComponent<RectTransform>().rect.min.y;
		maxY = rect.transform.GetComponent<RectTransform>().rect.max.y;

		rectWidth = Mathf.Abs(minX) + Mathf.Abs(maxX);
		rectHeight = Mathf.Abs(minY) + Mathf.Abs(maxY);	

		/*
		 * Annoying magic number. Needs fixing.
		 * Could be something related to the render texture
		  * and the camera, such that the render texture is not
		  * rendering the complete width of the camera.
		 */
		rectWidth += 100;
	}

	// Want this to return pixel coords.
	// Need to convert rect coords into pixel coords.
	// Rect coords (0,0) is the rects center.
	public bool GetLocalMouse( GameObject go, out Vector2 screen ) 
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        Vector3 rectCoord = rect.InverseTransformPoint( Input.mousePosition );

		Vector3 result;
		Vector3 clampedResult;

        result.x = Mathf.Clamp( rectCoord.x, rect.rect.min.x, rect.rect.max.x );
        result.y = Mathf.Clamp( rectCoord.y, rect.rect.min.y, rect.rect.max.y );

		result.x = rectCoord.x;
		result.y = rectCoord.y;
		result.z = rectCoord.z;

		if (result.x >= 0)
		{
			clampedResult.x = (rectWidth/2) + result.x;
		}
		else
		{
			clampedResult.x = (rectWidth/2) + result.x;
		}

		if (result.y >= 0)
		{
			clampedResult.y = (rectHeight/2) + result.y;
		}
		else
		{
			clampedResult.y = (rectHeight/2) + result.y;
		}

		float xFactor = clampedResult.x / rectWidth;
		float yFactor = clampedResult.y / rectHeight;

		screen.x = Mathf.Lerp(0, Screen.width, xFactor);
		screen.y = Mathf.Lerp(0, Screen.height, yFactor);


        return rect.rect.Contains( rectCoord );
    }

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 screen;

			if (GetLocalMouse(rect, out screen)) 
			{
				if (onHit != null)
				{
					onHit(overViewCamera, screen);
					print("Projected Screen: " + screen);
				}
			}
		}
	}
}
