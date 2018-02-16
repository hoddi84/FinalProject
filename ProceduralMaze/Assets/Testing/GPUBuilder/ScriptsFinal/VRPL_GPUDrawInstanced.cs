using UnityEngine;

public class VRPL_GPUDrawInstanced {

	private Mesh _mesh;
	private Material _material;
    private ComputeBuffer _bufferPosition;
	private ComputeBuffer _bufferQuaternion;
    private ComputeBuffer _bufferArgs;
    private int _instanceCount = -1;
    private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
	private const string POSITION_BUFFER = "positions";
	private const string QUATERNION_BUFFER = "quaternions";
	private const string GPU_SURFACE_INSTANCED = "GPUBuilder/GPUSurfaceInstanced";
	private const string TEX_ALBEDO = "_MainTex";
	private const string TEX_NORMAL = "_NormalMap";
	private Bounds _bounds = new Bounds (
		Vector3.zero,
		new Vector3(100f, 100f, 100f)
	);

	public void Draw(VRPL_GPUDrawNodeGroup drawNodeList)
	{
		if (_mesh == null)
		{
			_mesh = drawNodeList.mesh;
			_bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
			CreateMaterial(drawNodeList.texture);
		}

		if (drawNodeList.instanceCount > 0)
		{
			if (_instanceCount != drawNodeList.instanceCount)
			{	_instanceCount = drawNodeList.instanceCount;
				UpdateBuffers(drawNodeList.GetWorldPoitionsAsVec4(), drawNodeList.GetWorldRotationsAsQuaternions(), _instanceCount);
			}

			Graphics.DrawMeshInstancedIndirect(_mesh, 0, _material, _bounds, _bufferArgs);
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
	}

    private void UpdateBuffers(Vector4[] positions, Vector4[] quaternions, int instanceCount) {

        ReleaseBuffer(_bufferPosition);
		ReleaseBuffer(_bufferQuaternion);

        _bufferPosition = new ComputeBuffer(instanceCount, 16);
        _bufferPosition.SetData(positions);

		_bufferQuaternion = new ComputeBuffer(instanceCount, 16);
		_bufferQuaternion.SetData(quaternions);

        _material.SetBuffer(POSITION_BUFFER, _bufferPosition);
		_material.SetBuffer(QUATERNION_BUFFER, _bufferQuaternion);

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
