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
    TypeF,
    TypeF1,
    TypeF2,
    TypeF3,
    TypeF4,
    TypeG,
    TypeG1,
    TypeH,
    TypeH1,
    TypeH2,
    TypeH3,
    TypeH4,
    TypeH5,
    TypeH6,
    TypeH7,
    TypeK
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
