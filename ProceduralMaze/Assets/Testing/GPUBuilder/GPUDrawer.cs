using UnityEngine;

public class GPUDrawer {

	private Mesh _mesh;
	private Material _material;
    private ComputeBuffer _bufferPosition;
    private ComputeBuffer _bufferArgs;
    private int _instanceCount = -1;
    private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
	private const string POSITION_BUFFER = "positions";
	private const string GPU_SURFACE_INSTANCED = "GPUBuilder/GPUSurfaceInstanced";
	private const string TEX_ALBEDO = "_MainTex";
	private const string TEX_NORMAL = "_NormalMap";

	public GPUDrawer(Mesh mesh, Texture albedo, Texture normal = null, Material material = null)
	{
		_mesh = mesh;
		_bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);

		if (material != null)
		{
			_material = material;
			return;
		}

		CreateMaterial(albedo, normal);
	}

	public void Draw(int instanceCount, Vector4[] positions, Bounds bounds)
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

	private void CreateMaterial(params Texture[] textures)
	{
		_material = new Material(Shader.Find(GPU_SURFACE_INSTANCED));
		if (textures[0] != null)
		{
			_material.SetTexture(TEX_ALBEDO, textures[0]);
		}
		if (textures[1] != null)
		{
			_material.SetTexture(TEX_NORMAL, textures[1]);
		}
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
