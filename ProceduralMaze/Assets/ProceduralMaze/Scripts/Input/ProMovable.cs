using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMovable : MonoBehaviour {

    private ProMouseInput mouseInput;

    private bool canMove = false;
    private float height;
    private Vector3 mousePosition;
    public float moveSpeed = .1f;

    private Camera overHeadCam;

    private void Awake()
    {
        mouseInput = FindObjectOfType(typeof(ProMouseInput)) as ProMouseInput;

        mouseInput.onMouseButtonDownLeftRaycast += MoveObject;
        mouseInput.onMouseButtonLeftUp += ReleaseObject;

        mouseInput.onRenderTextureClick = MoveObjectOverTexture;

        height = transform.position.y;
    }

    private void Update()
    {
        if (canMove)
        {
            if (overHeadCam == null)
            {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.y = height;
                transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
            }
            else
            {
                mousePosition = mouseInput.renderTexMousePos;
                mousePosition = overHeadCam.ScreenToWorldPoint(mousePosition);
                mousePosition.y = height;
                transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
                transform.position = mousePosition;
                print("Object: " + overHeadCam.WorldToScreenPoint(transform.position));
            }         
        }
    }

    void MoveObject(RaycastHit hit)
    {
        if (gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
        {
            canMove = true;
        }
    }

    void MoveObjectOverTexture(Vector3 pos, RaycastHit hit, Camera overHeadCamera)
    {
        if (gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
        {
            overHeadCam = overHeadCamera;
            canMove = true;
        }
    }

    void ReleaseObject()
    {
        canMove = false;
    }
}
