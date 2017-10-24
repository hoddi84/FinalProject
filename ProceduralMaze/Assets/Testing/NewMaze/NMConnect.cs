using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMConnect : MonoBehaviour {

	public GameObject startUnit;
	public GameObject connectUnit;

	private GameObject currentUnit;
	private Vector3 currentUnitPos;

	void Start()
	{
		GameObject t = Instantiate(startUnit, new Vector3(0,0,0), Quaternion.identity);
		currentUnit = startUnit;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			connectUnit.GetComponent<NMUnit>().MakeConnecterParent();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			connectUnit.GetComponent<NMUnit>().RemoveParent();
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			print(currentUnit.GetComponent<NMUnit>().connecters[0].transform.position);
			print(connectUnit.GetComponent<NMUnit>().connecters[0].transform.position);
		}

		if (Input.GetKeyDown(KeyCode.Y))
		{
			GetCurrentUnitPosition();
			GetNewUnitPosition();
		}
	}

	void GetCurrentUnitPosition()
	{
		Vector3 currentPos = currentUnit.GetComponent<NMUnit>().connecters[0].transform.localPosition;
		currentUnitPos = currentPos;
	}

	void GetNewUnitPosition()
	{
		connectUnit.GetComponent<NMUnit>().MakeConnecterParent();
		connectUnit.GetComponent<NMUnit>().connecters[0].transform.position = currentUnitPos;
		connectUnit.GetComponent<NMUnit>().RemoveParent();
	}
}
