using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints_Nav : MonoBehaviour {

	public Transform[] waypoints;
	public float speed = .5f;
	public float rotSpeed = .1f;
	public float accuracy = .1f;
	private int currentWaypoint = 0;

	private void LateUpdate()
	{
		Debug.DrawRay(transform.position, transform.forward*5, Color.blue);

		//Vector3 goalPosition = waypoints[currentWaypoint].position - transform.position;
		Vector3 goalPosition = new Vector3(waypoints[currentWaypoint].position.x, transform.position.y, waypoints[currentWaypoint].position.z);

		Vector3 dir = goalPosition - transform.position;

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);

		if (dir.magnitude < accuracy)
		{
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length)
			{
				currentWaypoint = 0;
			}
		}
		//transform.position += dir.normalized * speed * Time.deltaTime;
		transform.Translate(0, 0, speed * Time.deltaTime);
	}
}
