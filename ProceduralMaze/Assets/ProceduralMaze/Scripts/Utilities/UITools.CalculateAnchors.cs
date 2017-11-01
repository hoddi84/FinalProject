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

	[MenuItem("Helper/CalculateAll")]
	static void AnchorObject3()
	{
		GameObject child = null;

		foreach (GameObject obj in Selection.gameObjects)
		{
			RecalculateAnchors(obj);

			RecalculateAnchorsChildren(obj);
		}
	}

	static void RecalculateAllAnchors(GameObject parent)
	{
		RecalculateAnchors(parent);

		if (parent.transform.childCount == 0)
		{
			return;
		}
		else
		{
			
		}
	}

	static void RecalculateAnchorsChildren(GameObject parent)
	{
		int childCount = parent.transform.childCount;

		for (int i = 0; i < childCount; i++)
		{
			RecalculateAnchors(parent.transform.GetChild(i).gameObject);
		}
	}

	static void RecalculateAnchors(GameObject obj)
	{
		RectTransform rectTransform = obj.GetComponent<RectTransform>();
		RectTransform parentRect = obj.transform.parent.GetComponent<RectTransform>();
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


