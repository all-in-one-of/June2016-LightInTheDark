using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class JumperLight : MonoBehaviour {
	public bool isDisabledAtStart = true;
	public bool canActivate = true;
	public bool canDeactivate = false;

	private LightController _lightController;

	// Use this for initialization
	void Start () {
		_lightController = GetComponentInParent<LightController> ();
		if (isDisabledAtStart) {
			_lightController.IsLightEnabled = false;
		}
	}

	public void OnPlayerCollided(Vector3 playerPosition) {
		if (playerPosition.y > (transform.position.y + transform.lossyScale.y / 3)) {
			_lightController.IsLightEnabled = true;
		} 
	}

}

}