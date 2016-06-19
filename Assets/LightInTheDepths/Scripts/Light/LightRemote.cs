using UnityEngine;
using System.Collections;

namespace LightInTheDark {
public class LightRemote : MonoBehaviour {
	public LightController[] connectedLights;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDisable() {
		Debug.Log ("Destroyed");
		foreach(LightController controller in connectedLights) {
			Debug.Log ("Disabling Light");
			controller.IsLightEnabled = false;
		}
	}
}
}