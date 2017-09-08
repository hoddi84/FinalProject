using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMouseInput : MonoBehaviour {

    public delegate void MouseButtonDownRaycastEvent(RaycastHit hit);
    public event MouseButtonDownRaycastEvent onMouseButtonDownRightRaycast;
    public event MouseButtonDownRaycastEvent onMouseButtonDownLeftRaycast;

    public delegate void MouseButtonUpEvent();
    public event MouseButtonUpEvent onMouseButtonLeftUp;

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
