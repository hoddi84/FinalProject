using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPUDraw : MonoBehaviour {

	[System.Serializable]
	public class DrawMesh {
		public Material material;
		public Mesh mesh;
		public Texture albedo;
		public Texture normal;
		[MinMaxRange(0, 100)]
		public RangedFloat xRange;
		[MinMaxRange(0, 100)]
		public RangedFloat yRange;
		[MinMaxRange(0, 100)]
		public RangedFloat zRange;
		[MinMaxRange(0, 5)]
		public RangedFloat szRange;
		public int count;
		private int _count;
		[HideInInspector]
		public Vector4[] positions;
		[HideInInspector]
		public Bounds bounds;
		public GPUDrawer drawer;
		public int GetCount() {return _count; }
		public void SetCount() { _count = count; }
		public int boundsMultiplier;
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
			drawMesh.drawer.Draw(drawMesh.count, drawMesh.positions, drawMesh.bounds);
		}
	}

	private Vector4 SpawnShape(float x, float y, float z, float sz)
	{
		Vector4 tmp = new Vector4(x, y, z, sz);
		//Vector4 tmp = new Vector4(Mathf.Sin(x) * y, z, Mathf.Cos(x) * y, sz);
	
		return tmp;
	}

	private void SetAllBounds(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			float xCenter = drawMeshes[i].xRange.maxValue - drawMeshes[i].xRange.minValue;
			float yCenter = drawMeshes[i].yRange.maxValue - drawMeshes[i].yRange.minValue;
			float zCenter = drawMeshes[i].zRange.maxValue - drawMeshes[i].zRange.minValue;
			Vector3 center = new Vector3(xCenter, yCenter, zCenter);

			float xSz = drawMeshes[i].xRange.maxValue - xCenter + drawMeshes[i].boundsMultiplier;
			float ySz = drawMeshes[i].yRange.maxValue - yCenter + drawMeshes[i].boundsMultiplier;;
			float zSz = drawMeshes[i].zRange.maxValue - zCenter + drawMeshes[i].boundsMultiplier;;
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
				float x = Random.Range(drawMeshes[i].xRange.minValue, drawMeshes[i].xRange.maxValue);
				float y = Random.Range(drawMeshes[i].yRange.minValue, drawMeshes[i].yRange.maxValue);
				float z = Random.Range(drawMeshes[i].zRange.minValue, drawMeshes[i].zRange.maxValue);
				float sz = Random.Range(drawMeshes[i].szRange.minValue, drawMeshes[i].szRange.maxValue);
				
				drawMeshes[i].positions[k] = SpawnShape(x, y, z, sz);
			}
			drawMeshes[i].SetCount();
		}
	}

	private void SetAllDrawers(DrawMesh[] drawMeshes)
	{
		for (int i = 0; i < drawMeshes.Length; i++)
		{
			drawMeshes[i].drawer = new GPUDrawer(drawMeshes[i].mesh, drawMeshes[i].albedo, drawMeshes[i].normal, drawMeshes[i].material);
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
						float x = Random.Range(drawMeshes[i].xRange.minValue, drawMeshes[i].xRange.maxValue);
						float y = Random.Range(drawMeshes[i].yRange.minValue, drawMeshes[i].yRange.maxValue);
						float z = Random.Range(drawMeshes[i].zRange.minValue, drawMeshes[i].zRange.maxValue);
						float sz = Random.Range(drawMeshes[i].szRange.minValue, drawMeshes[i].szRange.maxValue);
						additional[k] = SpawnShape(x, y ,z, sz);
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
