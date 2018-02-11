using UnityEngine;
using System.Collections;

public class CombineMesh : MonoBehaviour {

	private GPUBuilderExample gPUBuilder;
	public Material material;

	void Awake()
	{
		gPUBuilder = FindObjectOfType<GPUBuilderExample>();
	}

    void Start() {
        
		CombineMeshes();
    }

	void CombineMeshes()
	{
		MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];

		for (int i = 0; i < meshFilters.Length; i++)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.SetActive(false);
		}
		gameObject.AddComponent<MeshFilter>();
		GetComponent<MeshFilter>().mesh = new Mesh();
		GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
		gameObject.SetActive(true);

		gPUBuilder.mesh = GetComponent<MeshFilter>().mesh;
		gPUBuilder.material = material;
	}
}