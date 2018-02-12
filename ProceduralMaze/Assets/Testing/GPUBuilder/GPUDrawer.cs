using UnityEngine;

public class GPUDrawer {

	private Mesh _mesh;
	private Material _material;
    private ComputeBuffer _bufferPosition;
    private ComputeBuffer _bufferArgs;
    private int _instanceCount = -1;
    private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
	private const string POSITION_BUFFER = "positions";

	public GPUDrawer(Mesh mesh, Material material)
	{
		_mesh = mesh;
		_material = material;
		_bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
	}

	public void Draw(Mesh mesh, Material material, int instanceCount, Vector4[] positions, Bounds bounds)
	{
		if (instanceCount > 0)
		{
			if (_instanceCount != instanceCount)
			{	_instanceCount = instanceCount;
				UpdateBuffers(positions, _instanceCount);
			}

			Graphics.DrawMeshInstancedIndirect(_mesh, 0, _material, bounds, _bufferArgs);
		}
	}

	public void EmptyBuffers()
	{
		ReleaseBuffer(_bufferPosition);
        ReleaseBuffer(_bufferArgs);
	}

    private void UpdateBuffers(Vector4[] positions, int instanceCount) {

        ReleaseBuffer(_bufferPosition);

        _bufferPosition = new ComputeBuffer(instanceCount, 16);
        _bufferPosition.SetData(positions);

        _material.SetBuffer(POSITION_BUFFER, _bufferPosition);

		uint numIndices = (_mesh != null) ? (uint)_mesh.GetIndexCount(0) : 0;
        _args[0] = numIndices;
        _args[1] = (uint)instanceCount;
        _bufferArgs.SetData(_args);
    }

	private void ReleaseBuffer(ComputeBuffer buffer)
	{
		if (buffer != null) { buffer.Release(); buffer = null; }
	}
}
