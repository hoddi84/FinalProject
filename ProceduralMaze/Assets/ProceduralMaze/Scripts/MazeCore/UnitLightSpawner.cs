using UnityEngine;

namespace MazeCore.Lighting {
	
	public class UnitLightSpawner : MonoBehaviour {

		public GameObject unitLight;

		private GameObject _tmp;
		private GameObject _parentOfLights;

		private void Awake()
		{
			_parentOfLights = GameObject.Find("LIGHTS");
			
			if (_parentOfLights == null)
			{
				_parentOfLights = new GameObject("LIGHTS");
			}

		}

		private void OnEnable()
		{
			_tmp = Instantiate(unitLight, transform.position, transform.rotation);
			_tmp.transform.parent = _parentOfLights.transform;
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


