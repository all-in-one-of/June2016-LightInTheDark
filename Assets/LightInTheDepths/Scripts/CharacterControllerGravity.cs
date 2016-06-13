using UnityEngine;
using System.Collections;

public class CharacterControllerGravity : MonoBehaviour {
	public float gravity = -80f;
	public float terminalVelocity = -160.0f;
	public float minFall = -1.5f;

	private float _vertSpeed;
	private PlayerState _state;

	// Use this for initialization
	void Start () {
		_state = GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_state.CharController.isGrounded){
			_vertSpeed += gravity * Time.deltaTime;
			if(_vertSpeed > terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}
		}
		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_state.CharController.Move(movement);
	}
}
