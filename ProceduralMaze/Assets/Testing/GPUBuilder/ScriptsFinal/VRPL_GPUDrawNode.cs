using UnityEngine;

public class VRPL_GPUDrawNode {

	public string name;
	public Mesh mesh;
	public Texture texture;
	public Vector4 worldPosition;
	public Vector4 worldRotation;

	public VRPL_GPUDrawNode(string name, Mesh mesh, Texture texture, Vector4 worldPosition, Vector4 worldRotation)
	{
		this.name = name;
		this.mesh = mesh;
		this.texture = texture;
		this.worldPosition = new Vector4(worldPosition.x, worldPosition.y, worldPosition.z, 1);
		this.worldRotation = worldRotation;
	}
}
