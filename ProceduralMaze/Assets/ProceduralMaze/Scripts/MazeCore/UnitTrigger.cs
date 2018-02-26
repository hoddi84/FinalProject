using System;
using UnityEngine;

namespace MazeCore {

	public class UnitTrigger : MonoBehaviour {

		private enum EntryAxis { X,Y,Z, }
		private const string PLAYER = "Player";
		private const string EMPTY = "";
		private const string PATH_TO_SURROUNDING_WALLS = "/Core/[HospitalRoom]/Contents/";
		private const string CLONE = "(Clone)";
		private EntryAxis _entryAxis;
		private Vector3 _entryVector;
		private Vector3 _outVector;

		private GameObject _objToHide;
		private GameObject _objToShow;

		[Header("Outsite Settings")]
		public GameObject outsidePrefab;
		public string nameOfHide;
		public string nameOfShow;
		public bool showOutside;
		
		public Action<UnitTrigger> onTriggerEntered = null;

		[Header("Unit Connections")]
		public string fromType;
		public string isType;
		public string toType;

		private void Awake()
		{
			if (nameOfHide != EMPTY)
			{
				_objToHide = GameObject.Find(PATH_TO_SURROUNDING_WALLS + nameOfHide);
			}

			if (nameOfShow != EMPTY)
			{
				_objToShow = GameObject.Find(PATH_TO_SURROUNDING_WALLS + nameOfShow);
			}
		}

		private void Start()
		{
			FindAxisOfEntry();
		}

		/// <summary>
		/// Toggle surrounding walls.
		/// </summary>
		private void ToggleMainWalls()
		{
			if (_objToHide != null)
			{
				if (_objToHide.activeInHierarchy)
				{
					_objToHide.SetActive(false);
				}
			}

			if (_objToShow != null)
			{
				if (!_objToShow.activeInHierarchy)
				{
					_objToShow.SetActive(true);
				}
			}
		}

		/// <summary>
		/// Toggle outside environment.
		/// </summary>
		private void ToggleOutsideEnvironment()
		{
			if (outsidePrefab != null)
			{
				if (showOutside)
				{
					if (GameObject.Find(outsidePrefab.name + CLONE) == null)
					{
						Instantiate(outsidePrefab);
					}
				}
				else
				{
					Destroy(GameObject.Find(outsidePrefab.name + CLONE));
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == PLAYER)
			{
				_entryVector = other.transform.position;	
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.tag == PLAYER)
			{
				_outVector = other.transform.position;
				CheckColliderExit(_entryVector, _outVector, _entryAxis);
			}
		}

		/// <summary>
		/// Calculates the difference betweeen entry and exit points when going through
		/// a UnitTrigger collider, if difference is acceptable than we assume that a player
		/// has passed through the complete collider.
		/// </summary>
		/// <param name="entry">Entry value.</param>
		/// <param name="exit">Exit value.</param>
		private void CheckIfPassThrough(float entry, float exit)
		{
			float maxDiff = .15f;

			if (Mathf.Abs(entry - exit) > maxDiff)
			{
				if (onTriggerEntered != null)
				{
					onTriggerEntered(this);
					ToggleMainWalls();
					ToggleOutsideEnvironment();
				}
			}
		}

		/// <summary>
		/// Pass correct positions to CheckIfPassThrough deprending on the axis 
		/// of entry.
		/// </summary>
		/// <param name="entryPosition">Entry position.</param>
		/// <param name="exitPosition">Exit position.</param>
		/// <param name="axis">Axis of entry.</param>
		private void CheckColliderExit(Vector3 entryPosition, Vector3 exitPosition, EntryAxis axis)
		{
			switch (axis) 
			{
				case EntryAxis.X:
					CheckIfPassThrough(entryPosition.x, exitPosition.x);
					break;

				case EntryAxis.Y:
					CheckIfPassThrough(entryPosition.y, exitPosition.y);
					break;

				case EntryAxis.Z:
					CheckIfPassThrough(entryPosition.z, exitPosition.z);
					break;
			}
		}

		/// <summary>
		/// Find the axis of entry of the collider of the UnitTrigger.
		/// </summary>
		private void FindAxisOfEntry()
		{
			float smallest = 0;

			float x = gameObject.GetComponent<Collider>().bounds.extents.x;
			float y = gameObject.GetComponent<Collider>().bounds.extents.y;
			float z = gameObject.GetComponent<Collider>().bounds.extents.z;

			if (x < y)
			{
				smallest = x;
				_entryAxis = EntryAxis.X;
			}
			else 
			{
				smallest = y;
				_entryAxis = EntryAxis.Y;
			}

			if (z < smallest)
			{
				smallest = z;
				_entryAxis = EntryAxis.Z;
			}
		}
	}
}
