using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing : MonoBehaviour {

	public GameObject rect;

	private static bool GetLocalMouse( GameObject go, out Vector2 result ) 
    {
        var rt = ( RectTransform )go.transform;
        var mp = rt.InverseTransformPoint( Input.mousePosition );

        result.x = Mathf.Clamp( mp.x, rt.rect.min.x, rt.rect.max.x );
        result.y = Mathf.Clamp( mp.y, rt.rect.min.y, rt.rect.max.y );

        return rt.rect.Contains( mp );
    }

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 vector;

			if (GetLocalMouse(rect, out vector)) {
				print(vector);
			}
		}
	}
}
