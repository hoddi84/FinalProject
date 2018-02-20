using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GPUInstanced;

public class GPUSort : MonoBehaviour {

	private List<GPUInstancedNode> instancedNodeList = new List<GPUInstancedNode>();
	private List<GPUInstancedNodeGroup> instancedNodeGroupList = new List<GPUInstancedNodeGroup>();
	private bool nodeAdded = false;

	public GameObject[] obj;

	void Start()
	{
		PopulateNodeList();
		GroupNodes();
	}

	void GroupNodes()
	{
		foreach (GPUInstancedNode node in instancedNodeList)
		{
			nodeAdded = false;
			if (instancedNodeGroupList.Count == 0)
			{
				GPUInstancedNodeGroup nodeGroup = new GPUInstancedNodeGroup();
				nodeGroup.AddNode(node);
				instancedNodeGroupList.Add(nodeGroup);
			}

			foreach (GPUInstancedNodeGroup nodeGroup in instancedNodeGroupList)
			{
				if (nodeGroup.Compare(node))
				{
					nodeGroup.AddNode(node);
					nodeAdded = true;
				}
			}

			if (!nodeAdded)
			{
				GPUInstancedNodeGroup nodeGroup = new GPUInstancedNodeGroup();
				nodeGroup.AddNode(node);
				instancedNodeGroupList.Add(nodeGroup);
			}
		}

		instancedNodeList.Clear();
	}

	void PopulateNodeList()
	{
		foreach (GameObject g in obj)
		{
			Transform[] transforms = g.GetComponentsInChildren<Transform>();

			foreach (Transform transform in transforms)
			{
				MeshFilter filter = transform.GetComponent<MeshFilter>();
				MeshRenderer renderer = transform.GetComponent<MeshRenderer>();

				if (filter != null && renderer != null)
				{
					string name = transform.name;
					Mesh mesh = filter.sharedMesh;
					Texture texture = renderer.sharedMaterial.mainTexture;
					Vector3 worldPosition = transform.position;
					Quaternion worldQuaternion = transform.rotation;

					GPUInstancedNode node = new GPUInstancedNode(name, mesh, texture, worldPosition, worldQuaternion);
					instancedNodeList.Add(node);
				}
			}
		}
	}

	void Update()
	{
		foreach (GPUInstancedNodeGroup instancedNodeGroup in instancedNodeGroupList)
		{
			GPUInstancedDrawer.DrawInstancedIndirect(instancedNodeGroup);
		}
	}
}
