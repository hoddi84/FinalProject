using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints_Nav : MonoBehaviour {

	public Transform[] waypoints;
	public float speed = .5f;
	public float accuracy = .1f;
	private int currentWaypoint = 0;

	private void LateUpdate()
	{
		Vector3 goalPosition = waypoints[currentWaypoint].position - transform.position;

		if (goalPosition.magnitude < accuracy)
		{
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length)
			{
				currentWaypoint = 0;
			}
		}
		transform.position += goalPosition.normalized * speed * Time.deltaTime;
	}
}
