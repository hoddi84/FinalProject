using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anim_NavManager : MonoBehaviour {

	public GameObject zombie;
	private NavMeshAgent agent;
	private Vector3 currentStop;
	private bool movingOnPath = false;
	private Animator animator;

	// Use this for initialization
	void Start () {
		
		currentStop = zombie.transform.position;

		animator = zombie.GetComponent<Animator>();
		agent = zombie.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				currentStop = hit.point;
				agent.SetDestination(hit.point);
			}
		}

		if (Vector3.Distance(currentStop, zombie.transform.position) < .5f)
		{
			if (animator.GetBool("Stop") == false)
			{
				print("Stopped");
				agent.isStopped = true;
				movingOnPath = false;
				animator.SetBool("Stop", true);
			}
		}
		else
		{
			if (animator.GetBool("Stop") == true)
			{
				print("moving");
				agent.isStopped = false;
				animator.SetBool("Stop", false);
			}
		}
		
	}
}
