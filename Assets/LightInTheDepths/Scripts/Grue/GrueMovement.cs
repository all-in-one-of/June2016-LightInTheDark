using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LightInTheDark {

public enum MovementPriority {
	MAX,
	HIGH,
	LOW
};

[RequireComponent(typeof(GrueState))]
public class GrueMovement : MonoBehaviour {
	private GrueState _state;
	private Dictionary<MovementPriority, List<Vector3>> _movementRequests = new Dictionary<MovementPriority, List<Vector3>>();
	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 movement = DecideTotalMovement ();
		_state.CharController.Move (movement);

		movement.y = 0;

		_movementRequests.Clear ();
	}


	Vector3 DecideTotalMovement () {
		Vector3 total = Vector3.zero;
	
		AddUntilOverMax (ref total, MovementPriority.MAX);
		AddUntilOverMax (ref total, MovementPriority.HIGH);
		AddUntilOverMax (ref total, MovementPriority.LOW);

		if (IsOverMax(total)) {
			total.Normalize ();
			total *= _state.maxSpeed;
		}

		return Time.deltaTime * total;
	}

	void AddUntilOverMax(ref Vector3 total, MovementPriority priority) {
		if (!_movementRequests.ContainsKey(priority)) {
			return;
		}
		foreach (Vector3 movement in _movementRequests[priority]) {
			if (IsOverMax(total)) {
				break;
			}
			total += movement;
		}
	}

	bool IsOverMax(Vector3 vec) {
		return vec.sqrMagnitude > _state.maxSpeed * _state.maxSpeed;
	}

	public void AddMovement(Vector3 movement, MovementPriority priority) {
		if (!_movementRequests.ContainsKey(priority)) {
			_movementRequests.Add(priority, new List<Vector3> ());
		}
		_movementRequests[priority].Add (movement);
	}
}
}