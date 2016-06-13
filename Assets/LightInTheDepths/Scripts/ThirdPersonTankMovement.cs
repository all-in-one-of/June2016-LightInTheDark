using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class ThirdPersonTankMovement : MonoBehaviour {
	public float rotSpeed = 15.0f;
	public float moveSpeed = 6.0f;

	private float _vertSpeed;
	private PlayerState _state;

	void Start() {
		_state = GetComponent<PlayerState> ();
	}

	void FixedUpdate() {
		Vector3 movement = Vector3.zero;

		float horInput = Input.GetAxis("Horizontal");
		float vertInput = Input.GetAxis("Vertical");
		if (horInput != 0 || vertInput != 0)
		{
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			Vector3 look = movement;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);

			Quaternion tmp = transform.rotation;

			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			movement = transform.TransformDirection(movement);
			transform.rotation = tmp;

			look.z = Mathf.Abs (look.z);
			look = transform.TransformDirection(look);
			Quaternion direction = Quaternion.LookRotation(transform.forward + look);
			transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
		}

		_state.CharController.Move(movement);
	}
}
