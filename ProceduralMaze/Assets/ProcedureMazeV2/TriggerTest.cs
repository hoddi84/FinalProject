using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

	public GameObject[] connecters;

	private ProcedureMazeManager mazeManager;
	
	void Awake()
	{
		mazeManager = FindObjectOfType(typeof(ProcedureMazeManager)) as ProcedureMazeManager;
	}

	void OnTriggerEnter()
	{
		mazeManager.InstantiateUnit(connecters);
	}
}
