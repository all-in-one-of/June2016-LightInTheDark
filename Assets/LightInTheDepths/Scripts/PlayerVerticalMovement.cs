using UnityEngine;
using System.Collections;

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
	
	// Update is called once per frame
	void Update () {
		if (_state.CharController.isGrounded) {
			_vertSpeed = 0;
		} else {
			_vertSpeed += gravity * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
		}

		if (_state.CharController.isGrounded) {
			_state.isJumping = false;
			if (Input.GetButtonDown ("Jump")) {
				_vertSpeed += jumpSpeed;
				_state.isJumping = true;
			} 
		} 

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_mover.addMovement(movement);
	}
}
