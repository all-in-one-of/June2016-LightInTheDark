using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class CharacterControllerJumper : MonoBehaviour {
	public float jumpSpeed = 1500.0f;

	private float _vertSpeed;
	private PlayerState _state;
	private PlayerMover _mover;

	void Start () {
		_mover = GetComponent<PlayerMover> ();
		_state = GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_state.CharController.isGrounded) {
			_state.isJumping = false;
			if (Input.GetButtonDown ("Jump")) {
				Debug.Log ("Jump now");
				_vertSpeed = jumpSpeed;
				_state.isJumping = true;
			} 
		} 

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;

		Debug.Log (movement);
		_mover.addMovement(movement);
	}
}

}