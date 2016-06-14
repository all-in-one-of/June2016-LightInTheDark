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
		_lightController = GetComponent<LightController> ();
		if (isDisabledAtStart) {
			_lightController.IsLightEnabled = false;
		}
	}

	void OnPlayerCollision(ControllerColliderHit hit) {
		Vector3 other = hit.controller.transform.position;

		if (other.y > (transform.position.y + transform.lossyScale.y/2)) {
			_lightController.IsLightEnabled = true;
		}
	}

}

}