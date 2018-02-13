using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatedDraw : MonoBehaviour {

	public GameObject prefab;
	public int count = 0;
	private int _count;
	public bool enableInstancing;

	[MinMaxRange(0, 40)]
	public RangedFloat xRange;
	[MinMaxRange(0, 40)]
	public RangedFloat yRange;
	[MinMaxRange(0, 40)]
	public RangedFloat zRange;
	[MinMaxRange(0, 1)]
	public RangedFloat szRange;

	private Vector4[] _positions;
	private Material _material;

	void Start()
	{
		_material = prefab.GetComponent<MeshRenderer>().sharedMaterial;
		_count = count;

		EnableInstancing(true);

		Setup(prefab, count);

	}

	void Setup(GameObject prefab, int count)
	{
		{
			_positions = GetPositions(_positions, count);

			foreach (Vector3 vec3 in _positions)
			{
				GameObject tmp = Instantiate(prefab, vec3, Quaternion.identity);
				tmp.transform.localScale = new Vector3(
					Random.Range(szRange.minValue, szRange.maxValue),
					Random.Range(szRange.minValue, szRange.maxValue),
					Random.Range(szRange.minValue, szRange.maxValue)
				);
			}
		}
	}

	Vector4[] GetPositions(Vector4[] prevPositions, int count)
	{
		Vector4[] positions = new Vector4[count];

		for (int i = 0; i < positions.Length; i++)
		{
			float x = Random.Range(xRange.minValue, xRange.maxValue);
			float y = Random.Range(yRange.minValue, yRange.maxValue);
			float z = Random.Range(zRange.minValue, zRange.maxValue);
			float sz = Random.Range(szRange.minValue, szRange.maxValue);
			positions[i] = new Vector4(x, y, z, sz);
		}
		return positions;
	}

	void EnableInstancing(bool enable)
	{
		_material.enableInstancing = enable;
	}
}

