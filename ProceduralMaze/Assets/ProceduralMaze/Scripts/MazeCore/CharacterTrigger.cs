using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeUI;

public class CharacterTrigger : MonoBehaviour {

	private CharacterManager _characterManager;
	private SimpleInterfaceController _simpleInterfaceController;
	private const string PLAYER = "Player";
	private const string SPAWN_START = "SpawnStart";
	private const string SPAWN_END = "SpawnEnd";

	private float _scaryValue = 0;
	private float _presenceValue = 0;

	private Vector3 spawnStart;
	private Vector3 spawnEnd;
	private int spawnIndex = 0;
	public bool isWalking = true;
	private bool hasSpawned = false;

	private void Awake()
	{
		_characterManager = FindObjectOfType<CharacterManager>();
		_simpleInterfaceController = FindObjectOfType<SimpleInterfaceController>();

		if (_simpleInterfaceController != null)
		{
			_simpleInterfaceController.onPresenceSliderChanged += OnPresenceSliderChanged;
			_simpleInterfaceController.onScarySliderChanged += OnScarySliderChanged;

			_scaryValue = _simpleInterfaceController.scarySlider.value;
			_presenceValue = _simpleInterfaceController.presenceSlider.value;
		}
	}

	private void OnDisable()
	{
		if (_simpleInterfaceController != null)
		{
			_simpleInterfaceController.onPresenceSliderChanged -= OnPresenceSliderChanged;
			_simpleInterfaceController.onScarySliderChanged -= OnScarySliderChanged;
		}
	}

	private void Start()
	{
		spawnStart = GameObject.Find(SPAWN_START).transform.position;
		spawnEnd = GameObject.Find(SPAWN_END).transform.position;

		GameObject.Find(SPAWN_START).SetActive(false);
		GameObject.Find(SPAWN_END).SetActive(false);
	}

	private void OnPresenceSliderChanged(float newValue)
	{
		_presenceValue = newValue;
	}

	private void OnScarySliderChanged(float newValue)
	{
		_scaryValue = newValue;
	}

	private IEnumerator SpawnCharacter(Vector3 spawnEnd)
	{
		if (_scaryValue <= .6f) { spawnIndex = Random.Range(3,4); }
		else { spawnIndex = Random.Range(0,5); }

		_characterManager.SpawnCharacterSimple(spawnIndex, spawnStart, spawnEnd);
		if (!isWalking)
		{
			_characterManager.SetRunAnimationState();
		}
		_characterManager.MoveCharacterSimple(spawnEnd);

		yield return new WaitUntil(() => _characterManager.GetCurrentNavAgent().isStopped);

		_characterManager.DespawnCharacterSimple();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!hasSpawned)
		{
			if (other.tag == PLAYER)
			{
				float rnd = Random.Range(0.0f, 1.0f);
				if (rnd <= _presenceValue)
				{
					StartCoroutine(SpawnCharacter(spawnEnd));
					hasSpawned = true;
				}
			}
		}
	}
}
