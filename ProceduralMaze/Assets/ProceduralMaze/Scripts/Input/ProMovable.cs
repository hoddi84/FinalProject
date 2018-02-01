using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMovable : MonoBehaviour {

    private ProMouseInput _mouseInput;
    private bool _canMove = false;
    private float _height;
    private Vector3 _mousePosition;
    public float _moveSpeed = .1f;
    private Camera _overHeadCamera;

    private void Awake()
    {
        _mouseInput = FindObjectOfType(typeof(ProMouseInput)) as ProMouseInput;
        _mouseInput.onMouseButtonDownLeftRaycast += EnableObject;
        _mouseInput.onMouseButtonLeftUp += ReleaseObject;
        _mouseInput.onRenderTextureClickDown += EnableObjectOverTexture;

        _height = transform.position.y;
    }

    private void Update()
    {
        if (_canMove)
        {
            if (_overHeadCamera == null)
            {
                MoveObject(Input.mousePosition, Camera.main);
            }
            else
            {
                MoveObject(_mouseInput.renderTextureCoord, _overHeadCamera);
            }         
        }
    }

    void MoveObject(Vector3 position, Camera renderCamera)
    {
        _mousePosition = renderCamera.ScreenToWorldPoint(position);
        _mousePosition.y = _height;
        transform.position = Vector3.Lerp(transform.position, _mousePosition, _moveSpeed);
    }

    void EnableObject(RaycastHit hit)
    {
        if (gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
        {
            _canMove = true;
        }
    }

    void EnableObjectOverTexture(Vector3 pos, RaycastHit hit, Camera overHeadCamera)
    {
        if (gameObject.GetInstanceID() == hit.transform.gameObject.GetInstanceID())
        {
            _overHeadCamera = overHeadCamera;
            _canMove = true;
        }
    }

    void ReleaseObject()
    {
        _canMove = false;
        _overHeadCamera = null;
    }
}
