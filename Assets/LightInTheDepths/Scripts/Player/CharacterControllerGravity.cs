using UnityEngine;
using System.Collections;

namespace LightInTheDark {
	
public class CharacterControllerGravity : MonoBehaviour {
	public float gravity = -80f;
	public float terminalVelocity = -160.0f;
	public float minFall = -1.5f;

	private float _vertSpeed;
	private PlayerState _state;
	private PlayerMover _mover;

	// Use this for initialization
	void Start () {
		_mover = GetComponent<PlayerMover> ();
		_state = GetComponent<PlayerState> ();
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (_state.CharController.isGrounded) {
			_vertSpeed = 0;
		}
		_vertSpeed += gravity * Time.deltaTime;
		if(_vertSpeed > terminalVelocity) {
			_vertSpeed = terminalVelocity;
		}

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_mover.addMovement(movement);
	}
	}

}