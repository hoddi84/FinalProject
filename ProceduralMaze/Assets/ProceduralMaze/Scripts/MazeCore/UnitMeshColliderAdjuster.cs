using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Place this scripts on Units, to change the MeshCollider that is
 * attached to walls, such that it is convex and a trigger, which enables
 * the VIVE controllers to vibrate upon touching them.
 */
public class UnitMeshColliderAdjuster : MonoBehaviour {

	void Awake()
	{
		AdjustMeshColliders();
	}

	void AdjustMeshColliders()
	{
		MeshCollider[] colliders = gameObject.GetComponentsInChildren<MeshCollider>();

		foreach (MeshCollider collider in colliders)
		{
			collider.convex = true;
			collider.isTrigger = true;
		}
	}
}
