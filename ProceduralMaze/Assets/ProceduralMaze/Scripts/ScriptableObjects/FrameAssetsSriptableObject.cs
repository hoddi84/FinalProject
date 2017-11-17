using UnityEngine;

[CreateAssetMenu(fileName="FrameAssets", menuName="FrameAssets", order=1)]
public class FrameAssetsSriptableObject : ScriptableObject {

	public Sprite[] healthyFlowers;
	public Sprite[] dyingFlowers;
	public Sprite[] familyCreepy;
	public Sprite[] scary;
}
