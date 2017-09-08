﻿using System;
using UnityEngine;

public enum TestUnit
{
    NULL,
    TypeA,
    TypeB,
    TypeC,
    TypeD,
    TypeE,
    TypeE1,
    TypeE2,
    TypeE3,
}

public class UnitTrigger : MonoBehaviour {

    private const string PLAYER = "Player";

    public Action<UnitTrigger> onTriggerEntered = null;

    public TestUnit fromType;
    public TestUnit isType;
    public TestUnit toType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == PLAYER)
        {
            if (onTriggerEntered != null)
            {
                onTriggerEntered(this);
            }
        }
    }
}
