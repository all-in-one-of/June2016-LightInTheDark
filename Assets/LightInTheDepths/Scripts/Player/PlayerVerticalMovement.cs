using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class PlayerVerticalMovement : MonoBehaviour {
	public float jumpSpeed = 100.0f;	
	public float gravity = -200f;
	public float terminalVelocity = -160.0f;

	private float _vertSpeed;
	private PlayerState _state;
	private PlayerMover _mover;

	void Start () {
		_mover = GetComponent<PlayerMover> ();
		_state = GetComponent<PlayerState> ();
	}

	void Update () {
		if (_state.CharController.isGrounded) {
			_state.isJumping = false;
			if (Input.GetButtonDown ("Jump")) {
				_vertSpeed += jumpSpeed;
				_state.isJumping = true;
			} 
		} 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_state.isJumping && _state.CharController.isGrounded) {
			_vertSpeed = 0;
		} 

		_vertSpeed += gravity * Time.deltaTime;

		if (_vertSpeed < terminalVelocity) {
			_vertSpeed = terminalVelocity;
		}

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_mover.addMovement(movement);
	}
}

}