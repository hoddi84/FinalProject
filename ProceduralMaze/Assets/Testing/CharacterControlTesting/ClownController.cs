using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClownController : MonoBehaviour {

	public GameObject clown;
	private NavMeshAgent agentClown;
	private Animator animatorClown;

	private Vector3 currentPosition;

	// Lookat info
	private Vector3 lookAtPosition;
	public float lookAtHeight;
	private float _lookAtHeight;
	private HeadLookController headLookController;

	public bool followPlayer;
	public GameObject player;

	void Awake()
	{
		agentClown = clown.GetComponent<NavMeshAgent>();
		animatorClown = clown.GetComponent<Animator>();
		headLookController = clown.GetComponent<HeadLookController>();
		currentPosition = clown.transform.position;
	}

	void Start () {
		
		_lookAtHeight = lookAtHeight;
	}
	
	void Update () {

		clown.transform.position = new Vector3(clown.transform.position.x, 0, clown.transform.position.z);
		headLookController.target = lookAtPosition;

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				agentClown.SetDestination(hit.point);
				currentPosition = hit.point;
			}
		}	

		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				hit.point = new Vector3(hit.point.x, lookAtHeight, hit.point.z);
				lookAtPosition = hit.point;
			}
		}

		if (followPlayer)
		{
			FollowPlayer(player);
		}

		CheckLookAtHeightChanged();


		if (Vector3.Distance(currentPosition, clown.transform.position) >= .2f)
		{
			if (!animatorClown.GetBool("Walk"))
			{
				animatorClown.SetBool("Walk", true);
			}
		}
		else
		{
			if (animatorClown.GetBool("Walk"))
			{
				animatorClown.SetBool("Walk", false);
			}
		}	

		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(LookYes());
		}
	}

	IEnumerator LookYes()
	{
		float timeElapsed = 0;
		float duration = .5f;

		float lookMax = 5f;
		float lookMin = -5f;

		for (int i = 0; i < 6; i++)
		{
			while (timeElapsed <= duration)
			{
				lookAtHeight = Mathf.Lerp(lookMax, lookMin, (timeElapsed/duration));
				timeElapsed += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			float tmp = lookMax;
			lookMax = lookMin;
			lookMin = tmp;
			timeElapsed = 0;
		}
		lookAtHeight = 1.9f;
	}

	void CheckLookAtHeightChanged()
	{
		if (_lookAtHeight != lookAtHeight)
		{
			lookAtPosition = new Vector3(lookAtPosition.x, lookAtHeight, lookAtPosition.z);
			_lookAtHeight = lookAtHeight;
		}
	}

	void FollowPlayer(GameObject player)
	{
		Vector3 groundPlayer = new Vector3(player.transform.position.x, 0, player.transform.position.z);
		agentClown.SetDestination(groundPlayer);
		currentPosition = groundPlayer;
	}
}
