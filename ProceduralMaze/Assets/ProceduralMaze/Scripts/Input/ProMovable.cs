using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMovable : MonoBehaviour {

    private ProMouseInput mouseInput;

    private bool canMove = false;
    private float height;
    private Vector3 mousePosition;
    public float moveSpeed = .1f;

    private void Awake()
    {
        mouseInput = FindObjectOfType(typeof(ProMouseInput)) as ProMouseInput;

        mouseInput.onMouseButtonDownLeftRaycast += MoveObject;
        mouseInput.onMouseButtonLeftUp += ReleaseObject;

        height = transform.position.y;
    }

    private void Update()
    {
        if (canMove)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.y = height;
            transform.position = Vector3.Lerp(transform.position, mousePosition, moveSpeed);
        }
    }

    void MoveObject(RaycastHit hit)
    {
        if (gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
        {
            canMove = true;
        }
    }

    void ReleaseObject()
    {
        canMove = false;
    }
}
