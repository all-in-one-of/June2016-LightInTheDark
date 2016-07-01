using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerMover))]
public class ThirdPersonTankMovement : MonoBehaviour {
	public float rotSpeed = 15.0f;
	public float moveSpeed = 1.0f;

	private float _vertSpeed;
	private PlayerState _state;	
	private PlayerMover _mover;
	private bool _forceMarch = false;

	void Start() {
		_mover = GetComponent<PlayerMover> ();
		_state = GetComponent<PlayerState> ();
	}

	void FixedUpdate() {
		if (_state.isDieing) {
			_state.isWalking = false;
			
			return;
		}
		Vector3 movement = Vector3.zero;

		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");

		 if(_forceMarch) {
			vertInput = 1;
			horInput = 0;
		}

		if (horInput != 0 || vertInput != 0) {
			Vector3 look = new Vector3 (horInput * moveSpeed, 0, 0);
			movement.z = vertInput * moveSpeed;

			movement = Vector3.ClampMagnitude (movement, moveSpeed);
			Quaternion tmp = transform.rotation;

			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
			movement = transform.TransformDirection (movement);
			transform.rotation = tmp;

			look.z = Mathf.Abs (look.z);
			look = transform.TransformDirection (look);
			Quaternion direction = Quaternion.LookRotation (transform.forward + look);
			transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
			_state.isWalking = true;
		} else {
			_state.isWalking = false;
		}

		_mover.AddMovement(movement);
	}

	void OnStartWalkForward() {
		_forceMarch = true;
	}

	void OnStopWalkForward() {
		_forceMarch = false;
	}

}

}