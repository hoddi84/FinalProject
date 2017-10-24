using UnityEngine;
using UnityEditor;

public class CalculateUIAnchors : MonoBehaviour
{

	static void AnchorObject(GameObject gameObject)
	{
		RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
		RectTransform parentRect = gameObject.transform.parent.GetComponent<RectTransform>();
		// computing anchors
		Vector2 anchorMin = rectTransform.anchorMin;
		anchorMin.x = anchorMin.x + (rectTransform.offsetMin.x / parentRect.rect.width);
		anchorMin.y = anchorMin.y + (rectTransform.offsetMin.y / parentRect.rect.height);
		rectTransform.anchorMin = anchorMin;
		Vector2 anchorMax = rectTransform.anchorMax;
		anchorMax.x = anchorMax.x + (rectTransform.offsetMax.x / parentRect.rect.width);
		anchorMax.y = anchorMax.y + (rectTransform.offsetMax.y / parentRect.rect.height);
		rectTransform.anchorMax = anchorMax;
		// resetting offset and pivot
		rectTransform.offsetMin = new Vector2(0, 0);
		rectTransform.offsetMax = new Vector2(0, 0);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
	}

	[MenuItem("Helper/CalculateAnchors")]
	static void AnchorObject2()
	{
		RectTransform rectTransform = Selection.activeGameObject.GetComponent<RectTransform>();
		RectTransform parentRect = Selection.activeGameObject.transform.parent.GetComponent<RectTransform>();
		// computing anchors
		Vector2 anchorMin = rectTransform.anchorMin;
		anchorMin.x = anchorMin.x + (rectTransform.offsetMin.x / parentRect.rect.width);
		anchorMin.y = anchorMin.y + (rectTransform.offsetMin.y / parentRect.rect.height);
		rectTransform.anchorMin = anchorMin;
		Vector2 anchorMax = rectTransform.anchorMax;
		anchorMax.x = anchorMax.x + (rectTransform.offsetMax.x / parentRect.rect.width);
		anchorMax.y = anchorMax.y + (rectTransform.offsetMax.y / parentRect.rect.height);
		rectTransform.anchorMax = anchorMax;
		// resetting offset and pivot
		rectTransform.offsetMin = new Vector2(0, 0);
		rectTransform.offsetMax = new Vector2(0, 0);
		rectTransform.pivot = new Vector2(0.5f, 0.5f);
	}
}


