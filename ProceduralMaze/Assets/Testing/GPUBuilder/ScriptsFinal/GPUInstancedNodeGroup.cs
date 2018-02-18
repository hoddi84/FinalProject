namespace GPUInstanced {

	using System.Collections.Generic;
	using UnityEngine;
	using MazeUtiliy;

	public class GPUInstancedNodeGroup {

		public Mesh mesh = null;
		public Texture texture = null;
		public Vector4[] worldPositions = new Vector4[0];
		public Quaternion[] worldQuaternions = new Quaternion[0];
		public GPUInstancedDrawer drawer = new GPUInstancedDrawer();
		public string[] names = new string[0];
		public int count = 0;
		
		public void AddNode(GPUInstancedNode instancedNode)
		{
			if (mesh == null)
			{
				mesh = instancedNode.mesh;
				texture = instancedNode.texture;
			}

			worldPositions = UtilityTools.IncrementArray(instancedNode.worldPosition, worldPositions);
			worldQuaternions = UtilityTools.IncrementArray(instancedNode.worldQuaternion, worldQuaternions);
			names = UtilityTools.IncrementArray(instancedNode.name, names);

			count++;
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
	}
}
