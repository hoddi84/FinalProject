using System;
using UnityEngine;

public enum TestUnit
{
    NULL,
    TypeA,
    TypeB,
    TypeC,
    TypeD,
    TypeD1,
    TypeD2,
    TypeD3,
    TypeD4,
    TypeD5,
    TypeD6,
    TypeD7,
    TypeE,
    TypeE1,
    TypeE2,
    TypeE3,
    TypeE4,
    TypeE5,
    TypeE6,
    TypeG,
}

public class UnitTrigger : MonoBehaviour {

    private const string PLAYER = "Player";

    public Action<UnitTrigger> onTriggerEntered = null;

    public TestUnit fromType;
    public TestUnit isType;
    public TestUnit toType;

    private void OnTriggerExit(Collider other)
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
