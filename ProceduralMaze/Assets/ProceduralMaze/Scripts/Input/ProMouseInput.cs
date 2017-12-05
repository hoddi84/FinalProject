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

    // Just added.
    public Action<Vector3, RaycastHit, Camera> onRenderTextureClick = null;
    private testing testing;
    public Vector2 renderTexMousePos;

    private void Awake()
    {
        testing = FindObjectOfType(typeof(testing)) as testing;

        if (testing != null)
        {
            testing.onHit = OnRenderTextureHit;
        }
    }

    private void OnRenderTextureHit(Camera overHeadCam, Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = overHeadCam.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit))
        {
            if (onRenderTextureClick != null)
            {
                onRenderTextureClick(pos, hit, overHeadCam);
                renderTexMousePos = pos;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (onMouseButtonDownLeftRaycast != null)
                {
                    onMouseButtonDownLeftRaycast(hit);
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
}
