using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour {

	public int instanceCount = 100000;
    public Mesh instanceMesh;
    public Material instanceMaterial;

    private int cachedInstanceCount = -1;
    private ComputeBuffer positionBuffer;
    private ComputeBuffer argsBuffer;
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    void Start() {

        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        UpdateBuffers();
    }

    void Update() {

        // Update starting position buffer
        if (cachedInstanceCount != instanceCount)
            UpdateBuffers();

        // Render
        Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, new Bounds(Vector3.zero, new Vector3(300.0f, 300.0f, 300.0f)), argsBuffer);
    }

    void UpdateBuffers() {

        // positions
        if (positionBuffer != null) 
		{
			positionBuffer.Release();
		}
            
        positionBuffer = new ComputeBuffer(instanceCount, 16);
        Vector4[] positions = new Vector4[instanceCount];

        for (int i=0; i < instanceCount; i++) 
		{
            //float angle = Random.Range(0.0f, Mathf.PI * 2.0f);
            //float distance = Random.Range(20.0f, 100.0f);
            //float height = Random.Range(-2.0f, 2.0f);
            //float size = Random.Range(0.05f, 0.25f);
            //positions[i] = new Vector4(Mathf.Sin(angle) * distance, height, Mathf.Cos(angle) * distance, size);

			float x = Random.Range(0, 100);
			float y = Random.Range(0, 50);
			float z = Random.Range(0, 100);
			float size = .1f;

			positions[i] = new Vector4(x, y, z, size);
        }
        positionBuffer.SetData(positions);
        instanceMaterial.SetBuffer("positionBuffer", positionBuffer);

        // indirect args
        uint numIndices = (instanceMesh != null) ? (uint)instanceMesh.GetIndexCount(0) : 0;
        args[0] = numIndices;
        args[1] = (uint)instanceCount;
        argsBuffer.SetData(args);

        cachedInstanceCount = instanceCount;
    }

    void OnDisable() {

        if (positionBuffer != null)
            positionBuffer.Release();
        positionBuffer = null;

        if (argsBuffer != null)
            argsBuffer.Release();
        argsBuffer = null;
    }
}
