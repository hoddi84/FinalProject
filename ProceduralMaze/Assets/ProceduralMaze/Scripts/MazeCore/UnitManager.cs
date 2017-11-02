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
    public GameObject[] unitD8;
    public GameObject[] unitD9;
    public GameObject[] unitD10;
    public GameObject[] unitE;
    public GameObject[] unitE1;
    public GameObject[] unitE2;
    public GameObject[] unitE3;
    public GameObject[] unitE4;
    public GameObject[] unitE5;
    public GameObject[] unitE6;
    public GameObject[] unitE7;
    public GameObject[] unitE8;
    public GameObject[] unitE9;
    public GameObject[] unitE10;
    public GameObject[] unitE11;
    public GameObject[] unitF;
    public GameObject[] unitF1;
    public GameObject[] unitF2;
    public GameObject[] unitF3;
    public GameObject[] unitF4;
    public GameObject[] unitG;
    public GameObject[] unitG1;
    public GameObject[] unitH;
    public GameObject[] unitH1;
    public GameObject[] unitH2;
    public GameObject[] unitH3;
    public GameObject[] unitH4;
    public GameObject[] unitH5;
    public GameObject[] unitH6;
    public GameObject[] unitH7;
    public GameObject[] unitK;
    public GameObject[] unitK1;

    Dictionary<string, GameObject> pathDict = new Dictionary<string, GameObject>();

    private Action<string, GameObject> onInstantiate = null;

    public Action<Dictionary<string, GameObject>> onPathDictUpdate = null;

    //private PropManager propManager;

    private void Awake()
    {
        onInstantiate += UpdatePathDictionary;

        //propManager = GetComponent<PropManager>();
        //propManager.onPropsReady += GetPathDictionaryOnPropsInitial;

    }

    private void Start()
    {
        Initialize(unitA);
    }

    private void InstantiateUnit(UnitTrigger trigger)
    {

        switch (trigger.toType)
        {
            case "TypeA":

                CheckInstantiatedUnit(trigger, "TypeA", unitA);
                break;

            case "TypeB":

                CheckInstantiatedUnit(trigger, "TypeB", unitB);
                break;

            case "TypeC":

                CheckInstantiatedUnit(trigger, "TypeC", unitC);
                break;

            case "TypeD":

                CheckInstantiatedUnit(trigger, "TypeD", unitD);
                break;

            case "TypeD1":

                CheckInstantiatedUnit(trigger, "TypeD1", unitD1);
                break;

            case "TypeD2":

                CheckInstantiatedUnit(trigger, "TypeD2", unitD2);
                break;
            
            case "TypeD3":

                CheckInstantiatedUnit(trigger, "TypeD3", unitD3);
                break;

            case "TypeD4":

                CheckInstantiatedUnit(trigger, "TypeD4", unitD4);
                break;

            case "TypeD5":
                CheckInstantiatedUnit(trigger, "TypeD5", unitD5);
                break;

            case "TypeD6":
                CheckInstantiatedUnit(trigger, "TypeD6", unitD6);
                break;

            case "TypeD7":
                CheckInstantiatedUnit(trigger, "TypeD7", unitD7);
                break;
            
            case "TypeD8":
                CheckInstantiatedUnit(trigger, "TypeD8", unitD8);
                break;
            
            case "TypeD9":
                CheckInstantiatedUnit(trigger, "TypeD9", unitD9);
                break;

            case "TypeD10":
                CheckInstantiatedUnit(trigger, "TypeD10", unitD10);
                break;

            case "TypeE":

                CheckInstantiatedUnit(trigger, "TypeE", unitE);
                break;

            case "TypeE1":

                CheckInstantiatedUnit(trigger, "TypeE1", unitE1);
                break;

            case "TypeE2":

                CheckInstantiatedUnit(trigger, "TypeE2", unitE2);
                break;

            case "TypeE3":

                CheckInstantiatedUnit(trigger, "TypeE3", unitE3);
                break;

            case "TypeE4":

                CheckInstantiatedUnit(trigger, "TypeE4", unitE4);
                break;
            
            case "TypeE5":

                CheckInstantiatedUnit(trigger, "TypeE5", unitE5);
                break;

            case "TypeE6":

                CheckInstantiatedUnit(trigger, "TypeE6", unitE6);
                break;

            case "TypeE7":

                CheckInstantiatedUnit(trigger, "TypeE7", unitE7);
                break;

            case "TypeE8":

                CheckInstantiatedUnit(trigger, "TypeE8", unitE8);
                break;

            case "TypeE9":

                CheckInstantiatedUnit(trigger, "TypeE9", unitE9);
                break;

            case "TypeE10":

                CheckInstantiatedUnit(trigger, "TypeE10", unitE10);
                break;

            case "TypeE11":

                CheckInstantiatedUnit(trigger, "TypeE11", unitE11);
                break;
            
            case "TypeG":

                CheckInstantiatedUnit(trigger, "TypeG", unitG);
                break;

            case "TypeG1":

                CheckInstantiatedUnit(trigger, "TypeG1", unitG1);
                break;

            case "TypeF":

                CheckInstantiatedUnit(trigger, "TypeF", unitF);
                break;

            case "TypeF1":

                CheckInstantiatedUnit(trigger, "TypeF1", unitF1);
                break;

            case "TypeF2":

                CheckInstantiatedUnit(trigger, "TypeF2", unitF2);
                break;

            case "TypeF3":

                CheckInstantiatedUnit(trigger, "TypeF3", unitF3);
                break;

            case "TypeF4":

                CheckInstantiatedUnit(trigger, "TypeF4", unitF4);
                break;

            case "TypeH":
                CheckInstantiatedUnit(trigger, "TypeH", unitH);
                break;
            
            case "TypeH1":
                CheckInstantiatedUnit(trigger, "TypeH1", unitH1);
                break;
            
            case "TypeH2":
                CheckInstantiatedUnit(trigger, "TypeH2", unitH2);
                break;
            
            case "TypeH3":
                CheckInstantiatedUnit(trigger, "TypeH3", unitH3);
                break;

            case "TypeH4":
                CheckInstantiatedUnit(trigger, "TypeH4", unitH4);
                break;

            case "TypeH5":
                CheckInstantiatedUnit(trigger, "TypeH5", unitH5);
                break;
            
            case "TypeH6":
                CheckInstantiatedUnit(trigger, "TypeH6", unitH6);
                break;

            case "TypeH7":
                CheckInstantiatedUnit(trigger, "TypeH7", unitH7);
                break;

            case "TypeK":
                CheckInstantiatedUnit(trigger, "TypeK", unitK);
                break;

            case "TypeK1":
                CheckInstantiatedUnit(trigger, "TypeK1", unitK1);
                break;
        }
    }

    private void CheckInstantiatedUnit(UnitTrigger trigger, string type, GameObject[] unit)
    {
        GameObject tmp = null;

        if (!pathDict.ContainsKey(type))
        {
            int rndIndex = UnityEngine.Random.Range(0, unit.Length);
            tmp = Instantiate(unit[rndIndex]);
            ForceDisableUnit(trigger);
        }
        else
        {
            InstantiateExistingUnit(type);
            ForceDisableUnit(trigger);
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

    private void InstantiateExistingUnit(string type)
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
    
    private void ForceDisableUnit(UnitTrigger trigger)
    {
        if (trigger.fromType == trigger.isType) 
        {
            GameObject tmp = null;
            pathDict.TryGetValue(trigger.isType, out tmp);

            DeregisterListeners(tmp);
            tmp.SetActive(false);
        }
    }

    private void GetPathDictionaryOnPropsInitial()
    {
        if (onPathDictUpdate != null)
        {
            onPathDictUpdate(pathDict);
        }
    }

    private void UpdatePathDictionary(string unitType, GameObject obj)
    {
        if (!pathDict.ContainsKey(unitType))
        {
            pathDict.Add(unitType, obj);
        }

        if (onPathDictUpdate != null)
        {
            onPathDictUpdate(pathDict);
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
