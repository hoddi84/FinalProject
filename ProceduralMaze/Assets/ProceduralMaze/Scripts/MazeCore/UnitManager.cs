using System.Collections.Generic;
using UnityEngine;

namespace MazeCore {

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
        public GameObject[] unitG2;
        public GameObject[] unitG3;
        public GameObject[] unitG4;
        public GameObject[] unitG5;
        public GameObject[] unitG6;
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
        public GameObject[] unitO;
        public GameObject[] unitO1;
        public GameObject[] unitO2;
        public GameObject[] unitO3;
        public GameObject[] unitO4;
        public GameObject[] unitL;
        public GameObject[] unitL1;
        public GameObject[] unitL2;
        public GameObject[] unitL3;
        public GameObject[] unitL4;
        public GameObject[] unitL5;
        public GameObject[] unitL6;
        public GameObject[] unitL7;
        public GameObject[] unitL8;
        public GameObject[] unitL9;
        public GameObject[] unitL10;
        public GameObject[] unitL11;
        public GameObject[] unitL12;
        public GameObject[] unitL13;
        public GameObject[] unitL14;
        public GameObject[] unitL15;
        public GameObject[] unitL16;

        private Dictionary<string, GameObject> _pathDict = new Dictionary<string, GameObject>();
        private GameObject _parentOfUnits = null;

        private void Awake()
        {
            _parentOfUnits = GameObject.Find("UNITS");

            if (_parentOfUnits == null) 
            {
                _parentOfUnits = new GameObject("UNITS");
            }
        }

        private void Start()
        {
            Initialize(unitA);
        }

        /// <summary>
        /// Instantiate the Unit corresponding to the toType
        /// in the UnitTrigger.
        /// </summary>
        /// <param name="trigger">The UnitTrigger.</param>
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

                case "TypeG2":
                    CheckInstantiatedUnit(trigger, "TypeG2", unitG2);
                    break;

                case "TypeG3":
                    CheckInstantiatedUnit(trigger, "TypeG3", unitG3);
                    break;

                case "TypeG4":
                    CheckInstantiatedUnit(trigger, "TypeG4", unitG4);
                    break;

                case "TypeG5":
                    CheckInstantiatedUnit(trigger, "TypeG5", unitG5);
                    break;

                case "TypeG6":
                    CheckInstantiatedUnit(trigger, "TypeG6", unitG6);
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

                case "TypeO":
                    CheckInstantiatedUnit(trigger, "TypeO", unitO);
                    break;

                case "TypeO1":
                    CheckInstantiatedUnit(trigger, "TypeO1", unitO1);
                    break;

                case "TypeO2":
                    CheckInstantiatedUnit(trigger, "TypeO2", unitO2);
                    break;

                case "TypeO3":
                    CheckInstantiatedUnit(trigger, "TypeO3", unitO3);
                    break;

                case "TypeO4":
                    CheckInstantiatedUnit(trigger, "TypeO4", unitO4);
                    break;

                case "TypeL":
                    CheckInstantiatedUnit(trigger, "TypeL", unitL);
                    break;

                case "TypeL1":
                    CheckInstantiatedUnit(trigger, "TypeL1", unitL1);
                    break;

                case "TypeL2":
                    CheckInstantiatedUnit(trigger, "TypeL2", unitL2);
                    break;

                case "TypeL3":
                    CheckInstantiatedUnit(trigger, "TypeL3", unitL3);
                    break;

                case "TypeL4":
                    CheckInstantiatedUnit(trigger, "TypeL4", unitL4);
                    break;
                case "TypeL5":
                    CheckInstantiatedUnit(trigger, "TypeL5", unitL5);
                    break;
                case "TypeL6":
                    CheckInstantiatedUnit(trigger, "TypeL6", unitL6);
                    break;

                case "TypeL7":
                    CheckInstantiatedUnit(trigger, "TypeL7", unitL7);
                    break;

                case "TypeL8":
                    CheckInstantiatedUnit(trigger, "TypeL8", unitL8);
                    break;

                case "TypeL9":
                    CheckInstantiatedUnit(trigger, "TypeL9", unitL9);
                    break;

                case "TypeL10":
                    CheckInstantiatedUnit(trigger, "TypeL10", unitL10);
                    break;

                case "TypeL11":
                    CheckInstantiatedUnit(trigger, "TypeL11", unitL11);
                    break;

                case "TypeL12":
                    CheckInstantiatedUnit(trigger, "TypeL12", unitL12);
                    break;

                case "TypeL13":
                    CheckInstantiatedUnit(trigger, "TypeL13", unitL13);
                    break;

                case "TypeL14":
                    CheckInstantiatedUnit(trigger, "TypeL14", unitL14);
                    break;

                case "TypeL15":
                    CheckInstantiatedUnit(trigger, "TypeL15", unitL15);
                    break;

                case "TypeL16":
                    CheckInstantiatedUnit(trigger, "TypeL16", unitL16);
                    break;
            }
        }

        /// <summary>
        /// Here we want to check if we have spawned this type, but also we want to check if we
        /// should spawn a new random type, i.e. if the user has gone a circle.
        /// HINT: If the new current type matches the previous type we know the user has gone backwards.
        /// </summary>
        /// <param name="unitTrigger">The UnitTrigger attached to a Unit.</param>
        /// <param name="toType">The Unit type this UnitTrigger can connect to.</param>
        /// <param name="unit">The possible Unit gameObjects this UnitTrigger can connect to.</param>
        private void CheckInstantiatedUnit(UnitTrigger unitTrigger, string toType, GameObject[] unit)
        {
            if (!_pathDict.ContainsKey(toType))
            {
                GameObject tmp = null;

                int rndIndex = UnityEngine.Random.Range(0, unit.Length);
                tmp = Instantiate(unit[rndIndex]);
                tmp.transform.parent = _parentOfUnits.transform;

                RegisterListeners(tmp);
                UpdatePathDictionary(unitTrigger.toType, tmp);
            }
            else
            {
                ActivateExistingUnit(toType);
                ForceDisableUnit(unitTrigger);
            }
        }

        /// <summary>
        /// If the Unit type being activated is currently active in the scene
        /// we can deactivate it since we know we are moving further from it, if it's 
        /// inactive in the scene we activate it because we know we are moving to it next.
        /// </summary>
        /// <param name="toType">The Unit type being activated.</param>
        private void ActivateExistingUnit(string toType)
        {
            GameObject t;
            _pathDict.TryGetValue(toType, out t);

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
        
        /// <summary>
        /// Deactivate Unit gameObject.
        /// </summary>
        /// <param name="unitTrigger">The UnitTrigger belonging to the Unit being deactivated.</param>
        private void ForceDisableUnit(UnitTrigger unitTrigger)
        {
            if (unitTrigger.fromType == unitTrigger.isType) 
            {
                GameObject tmp = null;
                _pathDict.TryGetValue(unitTrigger.isType, out tmp);

                DeregisterListeners(tmp);
                tmp.SetActive(false);

                RestartLoop(unitA);
            }
        }

        /// <summary>
        /// Restart loop. Destroy all inactive Unit gameObjects and Frames.
        /// </summary>
        private void RestartLoop(GameObject[] startingUnit)
        {
            _pathDict.Clear();

            Destroy(_parentOfUnits);
            _parentOfUnits = new GameObject("UNITS");

            GameObject tmp = GameObject.Find("FRAMES");
            Destroy(tmp);

            Initialize(startingUnit);
        }

        /// <summary>
        /// Add new UnitType and Unit into PathDictionary.
        /// </summary>
        /// <param name="unitType">The type being added.</param>
        /// <param name="unit">The unit being added.</param>
        private void UpdatePathDictionary(string unitType, GameObject unit)
        {
            if (!_pathDict.ContainsKey(unitType))
            {
                _pathDict.Add(unitType, unit);
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

            tmp.transform.parent = _parentOfUnits.transform;

            RegisterListeners(tmp);
            UpdatePathDictionary(trigger.isType, tmp);
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
}


