using UnityEngine;

public class VRPL_GPUDrawNode {

	public string name;
	public Mesh mesh;
	public Texture texture;
	public Vector4 worldPosition;
	public Quaternion worldQuaternion;

	public VRPL_GPUDrawNode(string name, Mesh mesh, Texture texture, Vector4 worldPosition, Quaternion worldQuaternion)
	{
		this.name = name;
		this.mesh = mesh;
		this.texture = texture;
		this.worldPosition = new Vector4(worldPosition.x, worldPosition.y, worldPosition.z, 1);
		this.worldQuaternion = worldQuaternion;
	}
}
