using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMouseInput : MonoBehaviour {

    public delegate void MouseButtonDownRaycastEvent(RaycastHit hit);
    public event MouseButtonDownRaycastEvent onMouseButtonDownRightRaycast;
    public event MouseButtonDownRaycastEvent onMouseButtonDownLeftRaycast;

    public delegate void MouseButtonUpEvent();
    public event MouseButtonUpEvent onMouseButtonLeftUp;

    public GameObject renderTexture;
    public Camera overHeadCamera;
    public Action<Vector3, RaycastHit, Camera> onRenderTextureClickDown = null;

    [HideInInspector]
    public Vector3 renderTextureCoord;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (onMouseButtonDownLeftRaycast != null)
                {
                    onMouseButtonDownLeftRaycast(hit);
                }
            }

            Vector2 screenCoord;
            
            if (GetMouseScreenPosition(renderTexture, out screenCoord))
            {
                ray = overHeadCamera.ScreenPointToRay(screenCoord);

                if (Physics.Raycast(ray, out hit))
                {
                    if (onMouseButtonDownLeftRaycast != null)
                    {
                        onMouseButtonDownLeftRaycast(hit);
                    }

                    renderTextureCoord = screenCoord;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 screenCoord;

            if (GetMouseScreenPosition(renderTexture, out screenCoord))
            {
                RaycastHit hit;
                Ray ray = overHeadCamera.ScreenPointToRay(screenCoord);

                if (Physics.Raycast(ray, out hit))
                {
                    if (onRenderTextureClickDown != null)
                    {
                        onRenderTextureClickDown(screenCoord, hit, overHeadCamera);
                    }

                    renderTextureCoord = screenCoord;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (onMouseButtonLeftUp != null)
            {
                onMouseButtonLeftUp();
            }
        }
    }

    /// <summary>
    /// Convert the mouse position in a RectTransform to the
    /// position in screen pixels. Used with render textures.
    /// Returns true if the mouse position is within the rect transform.
    /// </summary>
    /// <param name="obj">The gameobject containing the render texture.</param>
    /// <param name="screen">The mouse position in screen pixels.</param>
    /// <returns></returns>
    private bool GetMouseScreenPosition(GameObject obj, out Vector2 screen) 
    {	
        RectTransform rect = obj.GetComponent<RectTransform>();

        float minX;
        float maxX;
        float minY;
        float maxY;
        float rectWidth;
        float rectHeight;

		minX = rect.transform.GetComponent<RectTransform>().rect.min.x;
		maxX = rect.transform.GetComponent<RectTransform>().rect.max.x;
		minY = rect.transform.GetComponent<RectTransform>().rect.min.y;
		maxY = rect.transform.GetComponent<RectTransform>().rect.max.y;

		rectWidth = Mathf.Abs(minX) + Mathf.Abs(maxX);
		rectHeight = Mathf.Abs(minY) + Mathf.Abs(maxY);	

        Vector3 rectCoord = rect.InverseTransformPoint( Input.mousePosition );

		Vector3 result;
		Vector3 clampedResult;

        result.x = Mathf.Clamp(rectCoord.x, rect.rect.min.x, rect.rect.max.x);
        result.y = Mathf.Clamp(rectCoord.y, rect.rect.min.y, rect.rect.max.y);

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


        return rect.rect.Contains(rectCoord);
    }
}
