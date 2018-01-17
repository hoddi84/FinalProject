using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour {

	private GameObject[] agentBlue;
	private GameObject[] agentRed;

	// Use this for initialization
	void Start () {

		agentRed = GameObject.FindGameObjectsWithTag("AI1");
		agentBlue = GameObject.FindGameObjectsWithTag("AI2");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				foreach (GameObject agent in agentRed)
				{
					agent.GetComponent<NavMeshAgent>().SetDestination(hit.point);
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				foreach (GameObject agent in agentBlue)
				{
					agent.GetComponent<NavMeshAgent>().SetDestination(hit.point);
				}
			}
		}
	}
}
