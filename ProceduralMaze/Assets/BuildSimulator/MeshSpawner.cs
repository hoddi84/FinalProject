using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSpawner : MonoBehaviour {

	public int instanceCount = 100000;
    public Mesh instanceMesh;
    public Material instanceMaterial;

    public GameObject ball;
    private BallMovement ballMovement;
    private Color ballColor1;
    private Color ballColor2;

    private int cachedInstanceCount = -1;
    private ComputeBuffer positionBuffer;
    private ComputeBuffer argsBuffer;
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    public bool render = false;

    void Start() {

        ballMovement = ball.GetComponent<BallMovement>();
        ballMovement.onCollision += ChangeCubeColor;

        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        UpdateBuffers();
    }

    void Update() {

        // Update starting position buffer
        if (cachedInstanceCount != instanceCount)
            UpdateBuffers();

        // Render
        if (render) 
        {
            Vector3 newPos = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z + 300);
            instanceMaterial.SetVector("_Point", newPos);
            Graphics.DrawMeshInstancedIndirect(instanceMesh, 0, instanceMaterial, new Bounds(Vector3.zero, new Vector3(300.0f, 300.0f, 300.0f)), argsBuffer);
        }
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

			float x = Random.Range(0, 100f);
			float y = Random.Range(0, 100f);
			float z = Random.Range(0, 100f);
			float size = Random.Range(0, 0.2f);

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

    void ChangeCubeColor()
    {
        ballColor1 = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
        ballColor2 = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
        instanceMaterial.SetColor("_BallColor1", ballColor1);
        instanceMaterial.SetColor("_BallColor2", ballColor2);
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
