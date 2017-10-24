using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMUnit : MonoBehaviour {

	public GameObject[] connecters;
	public GameObject content;

	public void MakeConnecterParent()
	{
		content.transform.parent = connecters[0].transform;
		connecters[1].transform.parent = connecters[0].transform;
	}

	public void RemoveParent()
	{
		content.transform.parent = gameObject.transform;
		connecters[1].transform.parent = gameObject.transform;
	}
}
