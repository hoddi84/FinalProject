using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MazeUI;

public class ClownController : MonoBehaviour {

	private enum Animation { WALK }
	private const string WALK = "Walk";
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

	private ProMouseInput _mouseInput;
	private UIController _uIController;

	bool GetAnimationState(Animation animation)
	{
		if (animation == Animation.WALK)
		{
			if (animatorClown.GetBool(WALK))
			{
				return true;
			}
		}
		return false;
	}

	void SetAnimationState(Animation animation, bool state)
	{
		switch (animation) {

			case Animation.WALK:
				animatorClown.SetBool(WALK, state);
				break;	
		}
	}

	void Awake()
	{
		_mouseInput = FindObjectOfType<ProMouseInput>();
		_uIController = FindObjectOfType<UIController>();
		agentClown = clown.GetComponent<NavMeshAgent>();
		animatorClown = clown.GetComponent<Animator>();
		headLookController = clown.GetComponent<HeadLookController>();
		currentPosition = clown.transform.position;

		if (_uIController != null)
		{
			_mouseInput.onRenderTextureClickDown += SetAgentDestination;
			_mouseInput.onRenderTextureClickDownLeft += SetAgentLookDirection;
		}
	}

	void Start () {
		
		_lookAtHeight = lookAtHeight;
	}
	
	void Update () {

		clown.transform.position = new Vector3(clown.transform.position.x, 0, clown.transform.position.z);
		headLookController.target = lookAtPosition;

		if (followPlayer)
		{
			FollowPlayer(player);
		}

		if (_uIController == null)
		{
			EnableDefaultControls();
		}

		CheckLookAtHeightChanged();

		if (Vector3.Distance(currentPosition, clown.transform.position) >= .1f)
		{
			agentClown.isStopped = false;
			if (!GetAnimationState(Animation.WALK))
			{
				SetAnimationState(Animation.WALK, true);
			}
		}
		else
		{
			agentClown.isStopped = true;
			if (GetAnimationState(Animation.WALK))
			{
				SetAnimationState(Animation.WALK, false);
			}
		}	
	}

	void SetAgentDestination(Vector3 pos, RaycastHit hit, Camera camera)
	{
		if (agentClown != null)
		{
			currentPosition = camera.ScreenToWorldPoint(pos);
			currentPosition.y = 0;
			agentClown.SetDestination(currentPosition);
		}
	}

	void SetAgentLookDirection(Vector3 pos, RaycastHit hit, Camera camera)
	{
		if (agentClown != null)
		{
			lookAtPosition = camera.ScreenToWorldPoint(pos);
			lookAtPosition.y = lookAtHeight;
		}
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

	void EnableDefaultControls()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				agentClown.SetDestination(hit.point);
				currentPosition = hit.point;
				currentPosition .y = 0;
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
	}
}
