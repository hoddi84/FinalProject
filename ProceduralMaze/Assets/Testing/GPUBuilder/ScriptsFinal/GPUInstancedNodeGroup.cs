namespace GPUInstanced {

	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using MazeUtiliy;

	public class GPUInstancedNodeGroup {

		public Mesh mesh = null;
		public Texture texture = null;
		public Material material = null;
		public Vector4[] worldPositions = new Vector4[0];
		public Quaternion[] worldQuaternions = new Quaternion[0];
		public string[] names = new string[0];
		public int count = 0;
		public Bounds bounds = new Bounds(Vector3.zero, new Vector3(100f, 100f, 100f));
		public ComputeBuffer bufferArgs = null;

		private ComputeBuffer _bufferPositions = null;
		private ComputeBuffer _bufferQuaternions = null;
		private uint[] _args = new uint[5] { 0, 0, 0, 0, 0 };
		private const string POSITION_BUFFER = "positions";
		private const string QUATERNION_BUFFER = "quaternions";
		private const string GPU_SURFACE_INSTANCED = "GPUBuilder/GPUSurfaceInstanced";
		private const string TEX_ALBEDO = "_MainTex";
		private const string TEX_NORMAL = "_NormalMap";

		public GPUInstancedNodeGroup()
		{
			bufferArgs = new ComputeBuffer(1, _args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
		}
		
		public void AddNode(GPUInstancedNode instancedNode)
		{
			if (mesh == null)
			{
				mesh = instancedNode.mesh;
				texture = instancedNode.texture;
				CreateMaterial(texture);
			}

			worldPositions = UtilityTools.IncrementArray(instancedNode.worldPosition, worldPositions);
			worldQuaternions = UtilityTools.IncrementArray(instancedNode.worldQuaternion, worldQuaternions);
			names = UtilityTools.IncrementArray(instancedNode.name, names);

			count++;

			UpdateBuffers(worldPositions, worldQuaternions, count);
		}

		public bool Compare(GPUInstancedNode instancedNode)
		{
			if (this.mesh.name == instancedNode.mesh.name)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void CreateMaterial(Texture texture)
		{
			material = new Material(Shader.Find(GPU_SURFACE_INSTANCED));
			material.SetTexture(TEX_ALBEDO, texture);
		}

		private void UpdateBuffers(Vector4[] positions, Quaternion[] quaternions, int count)
		{
			ReleaseBuffer(_bufferPositions);
			ReleaseBuffer(_bufferQuaternions);

			_bufferPositions = new ComputeBuffer(count, 16);
			_bufferPositions.SetData(positions);

			_bufferQuaternions = new ComputeBuffer(count, 16);
			_bufferQuaternions.SetData(quaternions);

			material.SetBuffer(POSITION_BUFFER, _bufferPositions);
			material.SetBuffer(QUATERNION_BUFFER, _bufferQuaternions);

			uint numIndices = (mesh != null) ? (uint)mesh.GetIndexCount(0) : 0;
			_args[0] = numIndices;
			_args[1] = (uint)count;
			bufferArgs.SetData(_args);
		}

		private void ReleaseBuffer(ComputeBuffer buffer)
		{
			if (buffer != null) { buffer.Release(); buffer = null; }
		}
	}
}
