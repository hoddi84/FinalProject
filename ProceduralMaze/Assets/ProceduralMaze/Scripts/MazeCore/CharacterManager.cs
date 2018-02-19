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
	public NavMeshAgent _currentAgent = null;
	private HeadLookController _currentHeadLook = null;
	private Vector3 _currentAgentPosition;
	private Vector3 _currentHeadLookDirection;
	private float _currentHeadLookHeight = 1.5f;

	private enum Animation { WALK, RUN }
	private const string WALK = "Walk";
	private const string RUN = "Run";
	private const string AGENT_GIRLSCOUT = "Agent_GirlScout";
	private const string AGENT_WHITECLOWN = "Agent_WhiteClown";
	private const string AGENT_ZOMBIEGIRL = "Agent_ZombieGirl";
	private const string AGENT_MALCOLM = "Agent_Malcolm";
	private const string AGENT_PARASITE = "Agent_Parasite";
	private const string AGENT_JASPER = "Agent_Jasper";

	private bool _isCharacterActive = false;
	private bool _isCharacterBeingSpawned = false;

	private bool _isWalking = true;
	private bool _isRunning = false;
	private bool _agentIsMoving = false;

	private int _spawnIndex = 0;
	private bool _frameSelected = false;
	private bool _lookAtPlayer = false;

	public GameObject[] characters;
	public GameObject[] characterBtns;
	public GameObject eyePlayer;
	
	private void Awake()
	{
		_mouseInput = FindObjectOfType<ProMouseInput>();
		_uiController = FindObjectOfType<UIController>();
	}

	private void OnEnable()
	{
		if (_uiController != null)
		{
			_mouseInput.onRenderTextureClickDown += SetAgentDestination;
			_mouseInput.onRenderTextureClickDownLeft += SetAgentLookDirection;
		}

		foreach (GameObject btn in characterBtns)
		{
			btn.GetComponent<CharacterButtonSelect>().onFrameSelected += OnFrameSelected;
		}
	}

	private void OnDisable()
	{
		if (_uiController != null)
		{
			_mouseInput.onRenderTextureClickDown -= SetAgentDestination;
			_mouseInput.onRenderTextureClickDownLeft -= SetAgentLookDirection;
		}

		foreach (GameObject btn in characterBtns)
		{
			if (btn != null)
			{
				btn.GetComponent<CharacterButtonSelect>().onFrameSelected -= OnFrameSelected;
			}
		}
	}

	private void OnFrameSelected(GameObject frame, bool selected)
	{
		if (selected && !_frameSelected)
		{
			switch (frame.name)
			{
				case AGENT_GIRLSCOUT:
					_spawnIndex = 0;
				break;

				case AGENT_WHITECLOWN:
					_spawnIndex = 1;
				break;

				case AGENT_ZOMBIEGIRL:
					_spawnIndex = 2;
				break;

				case AGENT_MALCOLM:
					_spawnIndex = 3;
				break;

				case AGENT_JASPER:
					_spawnIndex = 4;
				break;

				case AGENT_PARASITE:
					_spawnIndex = 5;
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

	private void MoveControlledCharacter(Vector3 newPosition)
	{
		newPosition.y = 0;
		_currentAgent.SetDestination(newPosition);
		_currentAgentPosition = newPosition;
	}

	public void SetWalkAnimationState()
	{
		_isWalking = true;
		_isRunning = false;
	}

	public void SetRunAnimationState()
	{
		_isRunning = true;
		_isWalking = false;
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
					if (_isWalking)
					{
						if (!GetAnimationState(Animation.WALK, _currentAnimator))
						{
							SetAnimationState(Animation.WALK, _currentAnimator, true);
						}
					}
					if (_isRunning)
					{
						if (!GetAnimationState(Animation.RUN, _currentAnimator))
						{
							SetAnimationState(Animation.RUN, _currentAnimator, true);
						}
					}	
				}
			}
			else
			{
				if (_currentAgent != null)
				{
					_currentAgent.isStopped = true;
					if (_isWalking)
					{
						if (GetAnimationState(Animation.WALK, _currentAnimator))
						{
							SetAnimationState(Animation.WALK, _currentAnimator, false);
						}
					}
					if (_isRunning)
					{
						if (GetAnimationState(Animation.RUN, _currentAnimator))
						{
							SetAnimationState(Animation.RUN, _currentAnimator, false);
						}
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
		if (animation == Animation.RUN)
		{
			if (currentAnimator.GetBool(RUN))
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
				if (state)
				{
					if (_currentControlled.name == AGENT_GIRLSCOUT)
					{
						_currentAgent.speed = 0.22f;
						_currentAgent.angularSpeed = 120.0f;
					}
					if (_currentControlled.name == AGENT_WHITECLOWN)
					{
						_currentAgent.speed = 0.35f;
						_currentAgent.angularSpeed = 115.0f;
					}
					if (_currentControlled.name == AGENT_ZOMBIEGIRL)
					{
						_currentAgent.speed = .2f;
						_currentAgent.angularSpeed = 120.0f;
					}
					if (_currentControlled.name == AGENT_MALCOLM)
					{
						_currentAgent.speed = .47f;
						_currentAgent.angularSpeed = 160.0f;
					}
					if (_currentControlled.name == AGENT_JASPER)
					{
						_currentAgent.speed = 0.57f;
						_currentAgent.angularSpeed = 170.0f;
					}
					if (_currentControlled.name == AGENT_PARASITE)
					{
						_currentAgent.speed = 0.45f;
						_currentAgent.angularSpeed = 140.0f;
					}
				}
			break;	

			case Animation.RUN:
				currentAnimator.SetBool(RUN, state);
				if (state)
				{
					if (_currentControlled.name == AGENT_GIRLSCOUT)
					{
						_currentAgent.speed = 1.5f;
						_currentAgent.angularSpeed = 170.0f;
					}
					if (_currentControlled.name == AGENT_WHITECLOWN)
					{
						_currentAgent.speed = 0.4f;
						_currentAgent.angularSpeed = 115.0f;
					}
					if (_currentControlled.name == AGENT_ZOMBIEGIRL)
					{
						_currentAgent.speed = .8f;
						_currentAgent.angularSpeed = 120.0f;
					}
					if (_currentControlled.name == AGENT_MALCOLM)
					{
						_currentAgent.speed = 1.4f;
						_currentAgent.angularSpeed = 160.0f;
					}
					if (_currentControlled.name == AGENT_JASPER)
					{
						_currentAgent.speed = 1.35f;
						_currentAgent.angularSpeed = 180.0f;
					}
					if (_currentControlled.name == AGENT_PARASITE)
					{
						_currentAgent.speed = 2.0f;
						_currentAgent.angularSpeed = 160.0f;
					}
				}
			break;
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

	public void SpawnCharacterSimple(int index, Vector3 spawnPosition)
	{
		_currentControlled = characters[index];
		_currentControlled.SetActive(true);
		_currentControlled.transform.position = spawnPosition;

		SetupControlledCharacter(_currentControlled);
	}

	public void MoveCharacterSimple(Vector3 newPosition)
	{
		newPosition.y = 0;
		_currentAgent.SetDestination(newPosition);
		_currentAgentPosition = newPosition;
	}

	public void DespawnCharacterSimple()
	{
		if (_currentControlled != null)
		{
			_currentControlled.SetActive(false);
			_currentControlled = null;

			SetupControlledCharacter();

			_isCharacterActive = false;
			_isCharacterBeingSpawned = true;
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

	public void LookAtPlayer()
	{
		_lookAtPlayer = true;
	}
}
