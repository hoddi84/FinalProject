using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPL_GPUDrawNodeGroup {

	public Mesh mesh = null;
	public Texture texture = null;
	public List<Vector4> worldPositions = new List<Vector4>();
	public List<Vector4> worldRotations = new List<Vector4>();
	public List<string> names = new List<string>();
	public int instanceCount = 0;
	public VRPL_GPUDrawInstanced drawer = new VRPL_GPUDrawInstanced();

	public void AddNode(VRPL_GPUDrawNode node)
	{
		if (mesh == null)
		{
			mesh = node.mesh;
			texture = node.texture;
		}

		worldPositions.Add(node.worldPosition);
		worldRotations.Add(node.worldRotation);
		names.Add(node.name);
		instanceCount++;
	}

	public bool Compare(VRPL_GPUDrawNode node)
	{
		if (this.mesh.name == node.mesh.name)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public Vector4[] GetWorldPoitionsAsVec4()
	{
		Vector4[] vec4 = new Vector4[worldPositions.Count];
		worldPositions.CopyTo(vec4);
		return vec4;
	}

	public Vector4[] GetWorldRotationsAsQuaternions()
	{
		Vector4[] vec4 = new Vector4[worldRotations.Count];
		worldRotations.CopyTo(vec4);
		return vec4;
	}
}
