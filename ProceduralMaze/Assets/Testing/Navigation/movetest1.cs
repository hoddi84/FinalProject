using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetest1 : MonoBehaviour {

	public Transform goal;
	public float speed = 0.5f;
	public bool useTranslate = false;

	void Start()
	{
		//transform.LookAt(goal.position);
	}

	private void LateUpdate()
	{
		WithTranslate(useTranslate);

		Debug.DrawRay(transform.position, transform.forward, Color.blue);
	}

	private void WithTranslate(bool useTranslate)
	{
		Vector3 goalposition = goal.transform.position - transform.position;
		Debug.DrawLine(transform.position, goal.position, Color.red);
		transform.LookAt(goal.position);

		if (useTranslate)
		{
			transform.Translate(goalposition.normalized * speed * Time.deltaTime, Space.World);
		}
		else
		{

			transform.position += goalposition * speed * Time.deltaTime;
		}
	}
}
