using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectRight : MonoBehaviour {

    public float xExtents;
    public float xPos;
    public float zPos;
    private MeshCollider collider;

    void Awake()
    {
        collider = GetComponent<MeshCollider>();
    }

    void Start()
    {
        xExtents = collider.bounds.extents.x;
        xPos = transform.position.x;
        zPos = transform.position.z;
    }
}
