using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anim_NavManager : MonoBehaviour {

	public GameObject zombie;
	public Avatar zombieWalk;
	public Avatar zombieAgony;
	private Vector3 currentStop;
	private Animator animator;

	// Use this for initialization
	void Start () {
		
		currentStop = zombie.transform.position;

		animator = zombie.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				currentStop = hit.point;
				zombie.GetComponent<NavMeshAgent>().SetDestination(hit.point);
			}
		}

		if (Vector3.Distance(currentStop, zombie.transform.position) < .5f)
		{
			print("yes");
			if (animator.GetBool("Stop") == false)
			{
				print("double yes");
				animator.avatar = zombieAgony;
				animator.SetBool("Stop", true);
			}
		}
		else
		{
			if (animator.GetBool("Stop") == true)
			{
				animator.avatar = zombieWalk;
				animator.SetBool("Stop", false);
			}
		}
		
	}
}
