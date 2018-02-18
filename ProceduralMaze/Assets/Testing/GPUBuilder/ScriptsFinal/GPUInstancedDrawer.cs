namespace GPUInstanced {

	using UnityEngine;

	public class GPUInstancedDrawer {

		private Mesh _mesh = null;
		private Material _material = null;
		private ComputeBuffer _bufferPositions = null;
		private ComputeBuffer _bufferQuaternions = null;
		private ComputeBuffer _bufferArgs = null;
		private Bounds _bounds = new Bounds (Vector3.zero, new Vector3(100f, 100f, 100f));
		private int _count = -1;
		private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
		private const string POSITION_BUFFER = "positions";
		private const string QUATERNION_BUFFER = "quaternions";
		private const string GPU_SURFACE_INSTANCED = "GPUBuilder/GPUSurfaceInstanced";
		private const string TEX_ALBEDO = "_MainTex";
		private const string TEX_NORMAL = "_NormalMap";

		public void Draw(GPUInstancedNodeGroup instancedNodeGoup)
		{
			if (_mesh == null)
			{
				_mesh = instancedNodeGoup.mesh;
				_bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
				CreateMaterial(instancedNodeGoup.texture);
			}

			if (instancedNodeGoup.count > 0)
			{
				if (_count != instancedNodeGoup.count)
				{	_count = instancedNodeGoup.count;
					UpdateBuffers(instancedNodeGoup.worldPositions, instancedNodeGoup.worldQuaternions, _count);
				}

				Graphics.DrawMeshInstancedIndirect(_mesh, 0, _material, _bounds, _bufferArgs);
			}
		}

		public void EmptyBuffers()
		{
			ReleaseBuffer(_bufferPositions);
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

		private void UpdateBuffers(Vector4[] positions, Quaternion[] quaternions, int instanceCount) {

			ReleaseBuffer(_bufferPositions);
			ReleaseBuffer(_bufferQuaternions);

			_bufferPositions = new ComputeBuffer(instanceCount, 16);
			_bufferPositions.SetData(positions);

			_bufferQuaternions = new ComputeBuffer(instanceCount, 16);
			_bufferQuaternions.SetData(quaternions);

			_material.SetBuffer(POSITION_BUFFER, _bufferPositions);
			_material.SetBuffer(QUATERNION_BUFFER, _bufferQuaternions);

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
}


