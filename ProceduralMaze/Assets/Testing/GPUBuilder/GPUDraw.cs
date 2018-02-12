using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUDraw : MonoBehaviour {

	[System.Serializable]
	public struct DrawMesh {
		public Material material;
		public Mesh mesh;
		public float xMin;
		public float xMax;
		public float yMin;
		public float yMax;
		public float zMin;
		public float zMax;
		public float szMin;
		public float szMax;
		public int count;
		private int _count;
		[HideInInspector]
		public Vector4[] positions;
		[HideInInspector]
		public Bounds bounds;
		public GPUDrawer drawer;
		public int GetCount() {return _count; }
		public void SetCount() { _count = count; }
	}

	public DrawMesh[] drawMeshes;

	private void Start()
	{
		SetAllBounds(drawMeshes);
		SetAllPositions(drawMeshes);
		SetAllDrawers(drawMeshes);
	}

	private void OnDisable()
	{
		foreach (DrawMesh drawMesh in drawMeshes)
		{
			drawMesh.drawer.EmptyBuffers();
		}
	}

	private void Update()
	{
		CheckForCountIncrease(drawMeshes);

		foreach (DrawMesh drawMesh in drawMeshes)
		{
			drawMesh.drawer.Draw(drawMesh.mesh, drawMesh.material, drawMesh.count, drawMesh.positions, drawMesh.bounds);
		}
	}

	private void SetAllBounds(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			float xCenter = drawMeshes[i].xMax - drawMeshes[i].xMin;
			float yCenter = drawMeshes[i].yMax - drawMeshes[i].yMin;
			float zCenter = drawMeshes[i].zMax - drawMeshes[i].zMin;
			Vector3 center = new Vector3(xCenter, yCenter, zCenter);

			float xSz = drawMeshes[i].xMax - xCenter;
			float ySz = drawMeshes[i].yMax - yCenter;
			float zSz = drawMeshes[i].zMax - zCenter;
			Vector3 sz = new Vector3(xSz, ySz, zSz);

			drawMeshes[i].bounds = new Bounds(center, sz);
		}
	}

	private void SetAllPositions(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			drawMeshes[i].positions = new Vector4[drawMeshes[i].count];
			for (int k = 0; k < drawMeshes[i].positions.Length; k++)
			{
				float x = Random.Range(drawMeshes[i].xMin, drawMeshes[i].xMax);
				float y = Random.Range(drawMeshes[i].yMin, drawMeshes[i].yMax);
				float z = Random.Range(drawMeshes[i].zMin, drawMeshes[i].zMax);
				float sz = Random.Range(drawMeshes[i].szMin, drawMeshes[i].szMax);
				//drawMeshes[i].positions[k] = new Vector4(x, y, z, sz);
				drawMeshes[i].positions[k] = new Vector4(Mathf.Sin(x) * z, y, Mathf.Cos(x) * z, sz);
			}
			drawMeshes[i].SetCount();
		}
	}

	private void SetAllDrawers(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			drawMeshes[i].drawer = new GPUDrawer(drawMeshes[i].mesh, drawMeshes[i].material);
		}
	}

	private void CheckForCountIncrease(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			if (drawMeshes[i].count != drawMeshes[i].GetCount())
			{
				if (drawMeshes[i].count > drawMeshes[i].GetCount())
				{
					Vector4[] additional = new Vector4[drawMeshes[i].count - drawMeshes[i].GetCount()];
					for (int k = 0; k < additional.Length; k++)
					{
						float x = Random.Range(drawMeshes[i].xMin, drawMeshes[i].xMax);
						float y = Random.Range(drawMeshes[i].yMin, drawMeshes[i].yMax);
						float z = Random.Range(drawMeshes[i].zMin, drawMeshes[i].zMax);
						float sz = Random.Range(drawMeshes[i].szMin, drawMeshes[i].szMax);
						//additional[k] = new Vector4(x, y, z, sz);
						additional[k] = new Vector4(Mathf.Sin(x) * z, y, Mathf.Cos(x) * z, sz);
					}

					List<Vector4> tmp = new List<Vector4>();
					foreach (Vector4 vec4 in drawMeshes[i].positions)
					{
						tmp.Add(vec4);
					}
					foreach (Vector4 vec4 in additional)
					{
						tmp.Add(vec4);
					}
					drawMeshes[i].positions = new Vector4[tmp.Count];
					drawMeshes[i].SetCount();
					tmp.CopyTo(drawMeshes[i].positions);
					tmp.Clear();
					tmp = null;
				}
			}
		}
	}
}
