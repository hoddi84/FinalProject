using UnityEngine;
using System.Collections;

public class GPUBuilderExample : MonoBehaviour {

	public Mesh mesh;
    public Material material;
    public int instanceCount = 100000;

    private int _instanceCount = -1;
    private ComputeBuffer _bufferPosition;
    private ComputeBuffer _bufferArgs;
    private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
	private const string POSITION_BUFFER = "positions";

    void Start() {

        _bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        UpdateBuffers();
    }

    void Update() {

        if (_instanceCount != instanceCount)
            UpdateBuffers();

        Graphics.DrawMeshInstancedIndirect(mesh, 0, material, new Bounds(Vector3.zero, new Vector3(100.0f, 100.0f, 100.0f)), _bufferArgs);
    }

    void UpdateBuffers() {

        ReleaseBuffer(_bufferPosition);
        _bufferPosition = new ComputeBuffer(instanceCount, 16);

        Vector4[] positions = new Vector4[instanceCount];
        for (int i=0; i < instanceCount; i++) {
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(-10.0f, 10.0f);
            float z = Random.Range(-10.0f, 10.0f);
            float size = Random.Range(.5f, 1.0f);
            positions[i] = new Vector4(x, y, z, size);
        }
        _bufferPosition.SetData(positions);
        material.SetBuffer(POSITION_BUFFER, _bufferPosition);

		SetupIndirectArgs(_bufferArgs);   
    }

    void OnDisable() {

        ReleaseBuffer(_bufferPosition);
        ReleaseBuffer(_bufferArgs);
    }

	void SetupIndirectArgs(ComputeBuffer argsBuffer)
	{
		uint numIndices = (mesh != null) ? (uint)mesh.GetIndexCount(0) : 0;
        _args[0] = numIndices;
        _args[1] = (uint)instanceCount;
        argsBuffer.SetData(_args);

		_instanceCount = instanceCount;
	}

	void ReleaseBuffer(ComputeBuffer buffer)
	{
		if (buffer != null) { buffer.Release(); buffer = null; }
	}
}