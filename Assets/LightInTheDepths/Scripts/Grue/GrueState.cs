using UnityEngine;
using System.Collections;

namespace LightInTheDark {
[RequireComponent(typeof(CharacterController))]
public class GrueState : MonoBehaviour {
	public bool isChasing = false;
	public bool isKilling = true;
	public bool isDieing = false;
	public float maxSpeed = 10;

	private CharacterController _charController;

	public CharacterController CharController {
		get {
			return _charController;
		}
	}

	void Awake () {
		_charController = GetComponent<CharacterController> ();
	}

}
}