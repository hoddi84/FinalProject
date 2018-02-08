using UnityEngine;

namespace MazeCore.Lighting {
	
	public class UnitLightSpawner : MonoBehaviour {

		public GameObject unitLight;

		private GameObject _tmp = null;
		private GameObject _parentOfLights = null;
		private const string LIGHTS = "LIGHTS";

		private void Awake()
		{
			_parentOfLights = GameObject.Find(LIGHTS);
			
			if (_parentOfLights == null)
			{
				_parentOfLights = new GameObject(LIGHTS);
			}

		}

		private void OnEnable()
		{
			if (unitLight != null)
			{
				_tmp = Instantiate(unitLight, transform.position, transform.rotation);
				_tmp.transform.parent = _parentOfLights.transform;
			}
		}

		private void OnDisable()
		{
			if (_tmp != null)
			{
				_tmp.GetComponent<LightSettings>().DisableLight();
			}
		}
	}
}


