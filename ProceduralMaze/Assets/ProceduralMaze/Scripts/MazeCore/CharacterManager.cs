using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

	public GameObject[] characters;

	private ProMouseInput _mouseInput;
	private UIController _uiController;

	// Settings for selected character.
	private GameObject _currentControlled = null;
	private Animator _currentAnimator;
	private NavMeshAgent _currentAgent;
	private Vector3 _currentAgentPosition;
	private HeadLookController _currentHeadLook;
	private Vector3 _currentHeadLookDirection;
	private float _currentHeadLookHeight = 1.5f;

	private bool _isCharacterActive = false;
	private bool _isCharacterBeingSpawned = false;

	private enum Animation { WALK }
	private const string WALK = "Walk";

	//Settings for UI
	public Text txtToggleSpawn;
	public Text txtToggleChar;

	public int spawnIndex = 0;

	private LightFlicker _lightFlicker;

	void Awake()
	{
		_mouseInput = FindObjectOfType<ProMouseInput>();
		_uiController = FindObjectOfType<UIController>();
		_lightFlicker = FindObjectOfType<LightFlicker>();

		if (_uiController != null)
		{
			_mouseInput.onRenderTextureClickDown += SetAgentDestination;
			_mouseInput.onRenderTextureClickDownLeft += SetAgentLookDirection;
		}
	}

	public void ToggleChar()
	{
		spawnIndex++;
		if (spawnIndex > 1)
		{
			spawnIndex = 0;
		}
		if (spawnIndex == 0)
		{
			txtToggleChar.text = "Girl";
		}
		if (spawnIndex == 1)
		{
			txtToggleChar.text = "Clown";
		}
	}

	public void ToggleSpawn()
	{
		if (!_isCharacterActive)
		{
			_isCharacterBeingSpawned = !_isCharacterBeingSpawned;
			print("Spawning Enabled: " + _isCharacterBeingSpawned);
			if (_isCharacterBeingSpawned)
			{
				txtToggleSpawn.text = "Disable Spawn";
			}
			else
			{
				txtToggleSpawn.text = "Enable Spawn";
			}
		}
	}

	void Update()
	{
		// Enable spawning of character.
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (!_isCharacterActive)
			{
				_isCharacterBeingSpawned = !_isCharacterBeingSpawned;
				print("Spawning Enabled: " + _isCharacterBeingSpawned);
			}
		}

		// Despawn character.
		if (Input.GetKeyDown(KeyCode.O))
		{
			if (_isCharacterActive)
			{
				DespawnCharacter();
			}
		}

		if (_uiController == null)
		{
			EnableDefaultControls();
		}

		if (_currentControlled != null && _currentHeadLook != null)
		{
			CheckAnimationState();

			_currentHeadLook.target = _currentHeadLookDirection;	
		}
	}

	void SetAgentDestination(Vector3 pos, RaycastHit hit, Camera camera)
	{
		if (_isCharacterBeingSpawned && _currentControlled == null)
		{
			_currentAgentPosition = camera.ScreenToWorldPoint(pos);
			SpawnCharacter(spawnIndex, _currentAgentPosition);
		}
		if (_currentControlled != null)
		{
			_currentAgentPosition = camera.ScreenToWorldPoint(pos);
			_currentAgentPosition.y = 0;
			_currentAgent.SetDestination(_currentAgentPosition);
		}
	}

	void SetAgentLookDirection(Vector3 pos, RaycastHit hit, Camera camera)
	{
		if (_currentControlled != null)
		{
			_currentHeadLookDirection = camera.ScreenToWorldPoint(pos);
			_currentHeadLookDirection.y = _currentHeadLookHeight;
		}
	}

	void EnableDefaultControls()
	{
		// Spawn character.
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

	void SpawnCharacter(int index, Vector3 spawnPosition)
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
			StartCoroutine(_lightFlicker.FlickerLights(delegate() {
				_currentControlled.SetActive(false);
				_currentControlled = null;

				SetupControlledCharacter();

				_isCharacterActive = false;
				_isCharacterBeingSpawned = true;
			}));
		}
	}

	void SetupControlledCharacter(GameObject currentControlled = null)
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

	void MoveControlledCharacter(Vector3 newPosition)
	{
		newPosition.y = 0;
		_currentAgent.SetDestination(newPosition);
		_currentAgentPosition = newPosition;
	}

	bool GetAnimationState(Animation animation, Animator currentAnimator)
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

	void SetAnimationState(Animation animation, Animator currentAnimator, bool state)
	{
		switch (animation) {

			case Animation.WALK:
				currentAnimator.SetBool(WALK, state);
				break;	
		}
	}

	void CheckAnimationState()
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
}
