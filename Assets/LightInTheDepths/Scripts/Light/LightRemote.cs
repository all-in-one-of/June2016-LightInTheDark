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
		foreach(LightController controller in connectedLights) {
			if (controller != null) {
				controller.IsLightEnabled = false;
			}
		}
	}
}
}