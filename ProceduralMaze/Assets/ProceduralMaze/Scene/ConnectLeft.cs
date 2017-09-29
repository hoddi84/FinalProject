using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectLeft : MonoBehaviour {

    public float xExtents;
    public float xPos;
    public float zPos;
    private MeshCollider meshCollider;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        xExtents = meshCollider.bounds.extents.x;
        xPos = transform.position.x;
        zPos = transform.position.z;
    }
}
