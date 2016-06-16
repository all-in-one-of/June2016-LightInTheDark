using UnityEngine;
using System.Collections;
namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueHover : MonoBehaviour {
	public float gravity = -200f;
	public float hoverHeight = 1;
	public MovementPriority priority = MovementPriority.HIGH;

	private float _vertSpeed = 0;
	private GrueState _state;
	private GrueMovement _movementController;

	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
	}

	void Update () {
		if (isWithinHoverHeight()) {
			_vertSpeed = 0;
			return;
		} 

		_vertSpeed += gravity * Time.deltaTime;

		Vector3 movement = Vector3.zero;
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;
		_movementController.AddMovement(movement, priority);
	}

	bool isWithinHoverHeight() {
		RaycastHit hit;
		bool gotHit = Physics.Raycast (transform.position, Vector3.down, out hit);

		if (gotHit) {
			return hit.distance < hoverHeight;
		}

		return gotHit;
	}
}
}