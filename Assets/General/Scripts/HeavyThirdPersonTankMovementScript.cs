﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class HeavyThirdPersonTankMovementScript : MonoBehaviour {
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public float pushForce = 3.0f;

    private float _vertSpeed;
    private CharacterController _charController;
	private ControllerColliderHit _contact;

    void Start() {
        _vertSpeed = minFall;
        _charController = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

			Quaternion tmp = transform.rotation;

			transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
			movement = transform.TransformDirection(movement);
			transform.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

		if (_charController.isGrounded){
            if (Input.GetButtonDown("Jump")){
                _vertSpeed = jumpSpeed;
            }
            else {
                _vertSpeed = -0.1f;
            }
        } else {
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if(_vertSpeed < terminalVelocity) {
                _vertSpeed = terminalVelocity;
            }
			if (_contact != null) {            
				if (_charController.isGrounded) {
					if (Vector3.Dot (movement, _contact.normal) < 0) {
						movement = _contact.normal * moveSpeed;
					} else {
						movement += _contact.normal * moveSpeed;
					}
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
        }

        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
    void OnControllerColliderHit(ControllerColliderHit hit) {
        _contact = hit;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

}
