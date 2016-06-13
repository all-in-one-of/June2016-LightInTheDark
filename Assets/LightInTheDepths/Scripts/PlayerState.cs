using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
	public bool isPunching = false;
	public bool isWalking = false;
	public bool isJumping = false;
	public bool isDieing = false;

	private CharacterController _charController;
	private float _timeStandingStill;

	public CharacterController CharController {
		get {
			return _charController;
		}
	}

	public float TimeStandingStill {
		get {
			return _timeStandingStill;
		}
	}
		
	void Awake () {
		_charController = GetComponent<CharacterController> ();
	}
		
	void Update() {
		if (isStandingStill ()) {
			_timeStandingStill += Time.deltaTime;
		} else {
			_timeStandingStill = 0;
		}
	}

	bool isStandingStill() {
		return !isPunching && !isWalking && !isJumping && !isDieing;
	}
}
