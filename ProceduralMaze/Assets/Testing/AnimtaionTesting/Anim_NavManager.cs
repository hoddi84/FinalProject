using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Anim_NavManager : MonoBehaviour {

	public GameObject[] characters;
	private Vector3 currentStop;

	// Use this for initialization
	void Start () {
		
		currentStop = characters[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		ForceYPosition(characters, 0.0f);

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				currentStop = hit.point;
				foreach (GameObject agent in characters)
				{
					agent.GetComponent<NavMeshAgent>().SetDestination(currentStop);
				}
			}
		}

		foreach (GameObject agent in characters)
		{
		if (Vector3.Distance(currentStop, agent.transform.position) < .5f)
		{
			if (agent.GetComponent<Animator>().GetBool("Stop") == false)
			{
				print("Stopped");
				agent.GetComponent<NavMeshAgent>().isStopped = true;
				agent.GetComponent<Animator>().SetBool("Stop", true);
			}
		}
		else
		{
			if (agent.GetComponent<Animator>().GetBool("Stop") == true)
			{
				print("moving");
				agent.GetComponent<NavMeshAgent>().isStopped = false;
				agent.GetComponent<Animator>().SetBool("Stop", false);
			}
		}	
		}	
	}

	/*
	 * Annoying hack, playing animations causes model to hover slightly.
	 * TODO: FIX
	 */
	private void ForceYPosition(GameObject[] objToForce, float yPos)
	{
		foreach (GameObject obj in objToForce)
		{
			obj.transform.position = new Vector3(obj.transform.position.x, yPos, obj.transform.position.z);
		}
	}
}
