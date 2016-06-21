using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerVerticalMovement : MonoBehaviour {
	public float jumpSpeed = 100.0f;	
	public float gravity = -200f;
	public float terminalVelocity = -160.0f;
	public GameObject feet;

	private float _tillEndJump = 0.0f;
	private float _vertSpeed;
	private PlayerState _state;
	private PlayerMover _mover;

	void Start () {
		_mover = GetComponent<PlayerMover> ();
		_state = GetComponent<PlayerState> ();
	}

	void Update () {
		_tillEndJump -= Time.deltaTime;

		if (_tillEndJump <= 0) {
			_state.isJumping = false;
		}

		if (!_state.isGrounded) {
			return;
		}
		
		if (Input.GetButtonDown ("Jump")) {
			_tillEndJump = 0.1f;
			_state.isJumping = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdatedGrounded ();

		if (_state.isJumping) {
			_vertSpeed = jumpSpeed;
		}

		_vertSpeed += gravity * Time.deltaTime;

		if (_vertSpeed < terminalVelocity) {
			_vertSpeed = terminalVelocity;
		}

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_mover.AddMovement(movement);

		if (!_state.isJumping && _state.isGrounded) {
			_vertSpeed = 0;
		} 
	}

	void UpdatedGrounded() {
		_state.isGrounded = Physics.Raycast (feet.transform.position, Vector3.down, 0.5f);
	}

}

}