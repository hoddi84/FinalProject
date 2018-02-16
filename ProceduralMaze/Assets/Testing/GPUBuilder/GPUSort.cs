using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUSort : MonoBehaviour {

	private List<VRPL_GPUDrawNode> nodeList = new List<VRPL_GPUDrawNode>();
	private List<VRPL_GPUDrawNodeGroup> nodeGroupList = new List<VRPL_GPUDrawNodeGroup>();
	private bool nodeAdded = false;

	public GameObject obj;

	public Mesh mesh;
	public Texture texture;
	public int count;
	public Vector4[] position;
	public Bounds bounds;

	private VRPL_GPUDrawInstanced draw = new VRPL_GPUDrawInstanced();

	void Start()
	{
		PopulateNodeList();
		GroupNodes();
	}

	void GroupNodes()
	{
		foreach (VRPL_GPUDrawNode node in nodeList)
		{
			nodeAdded = false;
			if (nodeGroupList.Count == 0)
			{
				VRPL_GPUDrawNodeGroup nodeGroup = new VRPL_GPUDrawNodeGroup();
				nodeGroup.AddNode(node);
				nodeGroupList.Add(nodeGroup);
			}

			foreach (VRPL_GPUDrawNodeGroup nodeGroup in nodeGroupList)
			{
				if (nodeGroup.Compare(node))
				{
					nodeGroup.AddNode(node);
					nodeAdded = true;
				}
			}

			if (!nodeAdded)
			{
				VRPL_GPUDrawNodeGroup nodeGroup = new VRPL_GPUDrawNodeGroup();
				nodeGroup.AddNode(node);
				nodeGroupList.Add(nodeGroup);
			}
		}
	}

	void PopulateNodeList()
	{
		Transform[] transforms = obj.GetComponentsInChildren<Transform>();

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
				Quaternion rotQ = transform.localRotation;
				Vector4 worldRotation = new Vector4(rotQ.w, rotQ.x, rotQ.y, rotQ.z);

				VRPL_GPUDrawNode node = new VRPL_GPUDrawNode(name, mesh, texture, worldPosition, worldRotation);
				nodeList.Add(node);
			}
		}
	}

	void Update()
	{
		foreach (VRPL_GPUDrawNodeGroup group in nodeGroupList)
		{
			group.drawer.Draw(group);
		}
	}
}
