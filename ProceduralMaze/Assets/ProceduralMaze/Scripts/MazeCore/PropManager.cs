using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour {

	public GameObject[] props;
	private GameObject propParent;
	private UnitManager unitManager;
	private const string PROP_GAMEOBJECT = "PROPS";
	public Action onPropsReady = null; 

	void Awake()
	{
		unitManager = GetComponent<UnitManager>();

		propParent = new GameObject();
		propParent.name = PROP_GAMEOBJECT;
	}

	void Start()
	{
		StartCoroutine(InitializeProps());
	}

	IEnumerator InitializeProps()
	{
		foreach (GameObject prop in props)
		{
			GameObject t = Instantiate(prop);
			t.transform.parent = propParent.transform;
			bool tr = t.GetComponent<PropSpawner>().done;
			yield return new WaitUntil(() => tr == true);
			unitManager.onPathDictUpdate += t.GetComponent<PropSpawner>().CheckForActivePath;
		}

		if (onPropsReady != null) 
		{
			onPropsReady();
		}
	}
}
