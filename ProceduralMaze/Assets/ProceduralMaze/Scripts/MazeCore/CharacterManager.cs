using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using MazeUtiliy;
using MazeUI;

public class CharacterManager : MonoBehaviour {

	private ProMouseInput _mouseInput;
	private UIController _uiController;

	private GameObject _currentControlled = null;
	private Animator _currentAnimator = null;
	private NavMeshAgent _currentAgent = null;
	private HeadLookController _currentHeadLook = null;
	private Vector3 _currentAgentPosition;
	private Vector3 _currentHeadLookDirection;
	private float _currentHeadLookHeight = 1.5f;

	private bool _isCharacterActive = false;
	private bool _isCharacterBeingSpawned = false;

	private enum Animation { WALK }
	private const string WALK = "Walk";
	private const string AGENT_GIRL = "Girl";
	private const string AGENT_CLOWN = "Clown";
	private const string AGENT_ZOMBIE = "Zombie";

	public GameObject[] characters;
	public GameObject[] characterBtns;
	private int _spawnIndex = 0;
	private bool _frameSelected = false;

	public GameObject eyePlayer;
	private bool _lookAtPlayer = false;

	private void Awake()
	{
		_mouseInput = FindObjectOfType<ProMouseInput>();
		_uiController = FindObjectOfType<UIController>();

		if (_uiController != null)
		{
			_mouseInput.onRenderTextureClickDown += SetAgentDestination;
			_mouseInput.onRenderTextureClickDownLeft += SetAgentLookDirection;
		}

		foreach (GameObject obj in characterBtns)
		{
			obj.GetComponent<CharacterButtonSelect>().onFrameSelected += OnFrameSelected;
		}
	}

	private void OnFrameSelected(GameObject frame, bool selected)
	{
		if (selected && !_frameSelected)
		{
			switch (frame.name)
			{
				case AGENT_GIRL:
					_spawnIndex = 0;
				break;

				case AGENT_CLOWN:
					_spawnIndex = 1;
				break;

				case AGENT_ZOMBIE:
					_spawnIndex = 2;
				break;
			}
			_frameSelected = true;
			_isCharacterBeingSpawned = true;
		}

		if (!selected)
		{
			_frameSelected = false;
			_isCharacterBeingSpawned = false;

		}
	}

	private void Update()
	{
		if (_uiController == null)
		{
			EnableDefaultControls();
		}

		if (_currentControlled != null && _currentHeadLook != null)
		{
			CheckAnimationState();

			if (_lookAtPlayer) { _currentHeadLook.target = eyePlayer.transform.position; }
			else { _currentHeadLook.target = _currentHeadLookDirection;	}
			
		}
	}

	private void SetAgentDestination(Vector3 newDestination, RaycastHit hit, Camera camera)
	{
		if (_isCharacterBeingSpawned && _currentControlled == null)
		{
			_currentAgentPosition = camera.ScreenToWorldPoint(newDestination);
			SpawnCharacter(_spawnIndex, _currentAgentPosition);
		}
		if (_currentControlled != null)
		{
			_currentAgentPosition = camera.ScreenToWorldPoint(newDestination);
			_currentAgentPosition.y = 0;
			_currentAgent.SetDestination(_currentAgentPosition);
		}
	}

	private void SetAgentLookDirection(Vector3 lookPosition, RaycastHit hit, Camera camera)
	{
		_lookAtPlayer = false;
		if (_currentControlled != null)
		{
			_currentHeadLookDirection = camera.ScreenToWorldPoint(lookPosition);
			_currentHeadLookDirection.y = _currentHeadLookHeight;
		}
	}

	public void LookAtPlayer()
	{
		_lookAtPlayer = true;
	}

	private void EnableDefaultControls()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{	
				if (_isCharacterBeingSpawned)
				{
					SpawnCharacter(0, hit.point);
				}

				if (_currentControlled != null)
				{
					MoveControlledCharacter(hit.point);
				}
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			if (_currentControlled != null)
			{
				RaycastHit hit;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
				{
					hit.point = new Vector3(hit.point.x, _currentHeadLookHeight, hit.point.z);
					_currentHeadLookDirection = hit.point;
				}
			}
		}
	}

	private void SpawnCharacter(int index, Vector3 spawnPosition)
	{
		_currentControlled = characters[index];
		_currentControlled.SetActive(true);
		_currentControlled.transform.position = spawnPosition;

		SetupControlledCharacter(_currentControlled);

		_isCharacterActive = true;
		_isCharacterBeingSpawned = false;
	}

	public void DespawnCharacter()
	{
		if (_currentControlled != null)
		{
			Light[] activeLights = FindObjectsOfType<Light>();

			StartCoroutine(UtilityTools.FlickerLights(activeLights, delegate() {
				_currentControlled.SetActive(false);
				_currentControlled = null;

				SetupControlledCharacter();

				_isCharacterActive = false;
				_isCharacterBeingSpawned = true;
			}));
		}
	}

	private void SetupControlledCharacter(GameObject currentControlled = null)
	{
		if (currentControlled != null)
		{
			_currentAgent = currentControlled.GetComponent<NavMeshAgent>();
			_currentAgentPosition = currentControlled.transform.position;
			_currentAnimator = currentControlled.GetComponent<Animator>();
			_currentHeadLook = currentControlled.GetComponent<HeadLookController>();
			_currentHeadLookDirection = _currentControlled.transform.forward;
		}
		else
		{
			_currentAgent = null;
			_currentAgentPosition = Vector3.zero;
			_currentAnimator = null;
			_currentHeadLook = null;
			_currentHeadLookDirection = Vector3.zero;
		}
	}

	private void MoveControlledCharacter(Vector3 newPosition)
	{
		newPosition.y = 0;
		_currentAgent.SetDestination(newPosition);
		_currentAgentPosition = newPosition;
	}

	private void CheckAnimationState()
	{
		if (_currentControlled != null)
		{
			if (Vector3.Distance(_currentAgentPosition, _currentControlled.transform.position) >= .1f)
			{
				if (_currentAgent != null)
				{
					_currentAgent.isStopped = false;
					if (!GetAnimationState(Animation.WALK, _currentAnimator))
					{
						SetAnimationState(Animation.WALK, _currentAnimator, true);
					}
				}
			}
			else
			{
				if (_currentAgent != null)
				{
					_currentAgent.isStopped = true;
					if (GetAnimationState(Animation.WALK, _currentAnimator))
					{
						SetAnimationState(Animation.WALK, _currentAnimator, false);
					}
				}
			}	
		}
	}

	private bool GetAnimationState(Animation animation, Animator currentAnimator)
	{
		if (animation == Animation.WALK)
		{
			if (currentAnimator.GetBool(WALK))
			{
				return true;
			}
		}
		return false;
	}

	private void SetAnimationState(Animation animation, Animator currentAnimator, bool state)
	{
		switch (animation) {

			case Animation.WALK:
				currentAnimator.SetBool(WALK, state);
				break;	
		}
	}
}
