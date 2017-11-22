﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDoorManager : MonoBehaviour {

	public float doorCloseTime;
	public float doorOpenTime;
	public float rotationAngle;

	public AudioClip openDoorClip;
	public AudioClip openHandleClip;
	public AudioClip closeDoorClip;
	public Sprite doorSpriteClosed;
	public Sprite doorSpriteOpen;
	public Sprite doorSpriteLockOpen;
	public Sprite doorSpriteLockClosed;
}
