using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    public GameObject[] unitA;
    public GameObject[] unitB;
    public GameObject[] unitC;
    public GameObject[] unitD;
    public GameObject[] unitD1;
    public GameObject[] unitD2;
    public GameObject[] unitD3;
    public GameObject[] unitD4;
    public GameObject[] unitD5;
    public GameObject[] unitD6;
    public GameObject[] unitD7;
    public GameObject[] unitE;
    public GameObject[] unitE1;
    public GameObject[] unitE2;
    public GameObject[] unitE3;
    public GameObject[] unitE4;
    public GameObject[] unitE5;
    public GameObject[] unitE6;
    public GameObject[] unitF;
    public GameObject[] unitF1;
    public GameObject[] unitF2;
    public GameObject[] unitF3;
    public GameObject[] unitG;

    Dictionary<TestUnit, GameObject> pathDict = new Dictionary<TestUnit, GameObject>();

    private Action<TestUnit, GameObject> onInstantiate = null;

    private void Awake()
    {
        onInstantiate += UpdatePathDictionary;
    }

    private void Start()
    {
        Initialize(unitA);
    }

    private void InstantiateUnit(UnitTrigger trigger)
    {

        switch (trigger.toType)
        {
            case TestUnit.TypeA:

                CheckInstantiatedUnit(trigger, TestUnit.TypeA, unitA);
                break;

            case TestUnit.TypeB:

                CheckInstantiatedUnit(trigger, TestUnit.TypeB, unitB);
                break;

            case TestUnit.TypeC:

                CheckInstantiatedUnit(trigger, TestUnit.TypeC, unitC);
                break;

            case TestUnit.TypeD:

                CheckInstantiatedUnit(trigger, TestUnit.TypeD, unitD);
                break;

            case TestUnit.TypeD1:

                CheckInstantiatedUnit(trigger, TestUnit.TypeD1, unitD1);
                break;

            case TestUnit.TypeD2:

                CheckInstantiatedUnit(trigger, TestUnit.TypeD2, unitD2);
                break;
            
            case TestUnit.TypeD3:

                CheckInstantiatedUnit(trigger, TestUnit.TypeD3, unitD3);
                break;

            case TestUnit.TypeD4:

                CheckInstantiatedUnit(trigger, TestUnit.TypeD4, unitD4);
                break;

            case TestUnit.TypeD5:
                CheckInstantiatedUnit(trigger, TestUnit.TypeD5, unitD5);
                break;

            case TestUnit.TypeD6:
                CheckInstantiatedUnit(trigger, TestUnit.TypeD6, unitD6);
                break;

            case TestUnit.TypeD7:
                CheckInstantiatedUnit(trigger, TestUnit.TypeD7, unitD7);
                break;

            case TestUnit.TypeE:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE, unitE);
                break;

            case TestUnit.TypeE1:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE1, unitE1);
                break;

            case TestUnit.TypeE2:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE2, unitE2);
                break;

            case TestUnit.TypeE3:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE3, unitE3);
                break;

            case TestUnit.TypeE4:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE4, unitE4);
                break;
            
            case TestUnit.TypeE5:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE5, unitE5);
                break;

            case TestUnit.TypeE6:

                CheckInstantiatedUnit(trigger, TestUnit.TypeE6, unitE6);
                break;
            
            case TestUnit.TypeG:

                CheckInstantiatedUnit(trigger, TestUnit.TypeG, unitG);
                break;

            case TestUnit.TypeF:

                CheckInstantiatedUnit(trigger, TestUnit.TypeF, unitF);
                break;

            case TestUnit.TypeF1:

                CheckInstantiatedUnit(trigger, TestUnit.TypeF1, unitF1);
                break;

            case TestUnit.TypeF2:

                CheckInstantiatedUnit(trigger, TestUnit.TypeF2, unitF2);
                break;

            case TestUnit.TypeF3:

                CheckInstantiatedUnit(trigger, TestUnit.TypeF3, unitF3);
                break;
        }
    }

    private void CheckInstantiatedUnit(UnitTrigger trigger, TestUnit type, GameObject[] unit)
    {
        GameObject tmp = null;

        if (!pathDict.ContainsKey(type))
        {
            int rndIndex = UnityEngine.Random.Range(0, unit.Length);
            tmp = Instantiate(unit[rndIndex]);
        }
        else
        {
            InstantiateExistingUnit(type);
        }

        if (tmp != null)
        {
            RegisterListeners(tmp);
        }

        if (onInstantiate != null)
        {
            onInstantiate(trigger.toType, tmp);
        }
    }

    private void InstantiateExistingUnit(TestUnit type)
    {
        GameObject t;
        pathDict.TryGetValue(type, out t);

        if (!t.activeInHierarchy)
        {
            t.SetActive(true);
            RegisterListeners(t);
        }
        else
        {
            DeregisterListeners(t);
            t.SetActive(false);
        }
    }

    private void UpdatePathDictionary(TestUnit unitType, GameObject obj)
    {
        if (!pathDict.ContainsKey(unitType))
        {
            pathDict.Add(unitType, obj);
        }
    }

    /// <summary>
    /// Initialize a random starting point Unit from 
    /// the chosen unit type.
    /// </summary>
    /// <param name="unit"></param>
    private void Initialize(GameObject[] unit)
    {
        int rndIndex = UnityEngine.Random.Range(0, unit.Length);
        GameObject tmp = Instantiate(unit[rndIndex]);
        UnitTrigger trigger = tmp.GetComponentInChildren<UnitTrigger>();

        RegisterListeners(tmp);

        if (onInstantiate != null)
        {
            onInstantiate(trigger.isType, tmp);
        }
    }

    /// <summary>
    /// Register to "onTriggerEntered" events from a gameObject Unit,
    /// that contains a UnitTrigger.
    /// </summary>
    /// <param name="unit"></param>
    private void RegisterListeners(GameObject unit)
    {
        if (unit.GetComponentsInChildren<UnitTrigger>() != null)
        {
            UnitTrigger[] triggers = unit.GetComponentsInChildren<UnitTrigger>();

            foreach (UnitTrigger trigger in triggers)
            {
                trigger.onTriggerEntered += InstantiateUnit;
            }
        }
    }

    /// <summary>
    /// Deregister to "onTriggeredEntered" events from a gameObject Unit,
    /// that contains a UnitTrigger.
    /// </summary>
    /// <param name="unit"></param>
    private void DeregisterListeners(GameObject unit)
    {
        if (unit.GetComponentsInChildren<UnitTrigger>() != null)
        {
            UnitTrigger[] triggers = unit.GetComponentsInChildren<UnitTrigger>();
            
            foreach (UnitTrigger trigger in triggers)
            {
                trigger.onTriggerEntered -= InstantiateUnit;
            }
        }
    }
}
