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
	public float maxSpeed = 10;
	private GrueState _state;
	private Dictionary<MovementPriority, List<Vector3>> _movementRequests = new Dictionary<MovementPriority, List<Vector3>>();
	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		_state.CharController.Move (DecideTotalMovement());
		_movementRequests.Clear ();
	}


	Vector3 DecideTotalMovement () {
		Vector3 total = Vector3.zero;
	
		AddUntilOverMax (ref total, MovementPriority.MAX);
		AddUntilOverMax (ref total, MovementPriority.HIGH);
		AddUntilOverMax (ref total, MovementPriority.LOW);

		float magSq = total.sqrMagnitude;
		if (IsOverMax(total)) {
			total.Normalize ();
			total *= maxSpeed;
		}

		return total;
	}

	void AddUntilOverMax(ref Vector3 total, MovementPriority prio) {
		foreach (Vector3 movement in _movementRequests[prio]) {
			if (IsOverMax(total)) {
				break;
			}
			total += movement;
		}
	}

	bool IsOverMax(Vector3 vec) {
		return vec.sqrMagnitude > maxSpeed * maxSpeed;
	}

	public void AddMovement(Vector3 movement, MovementPriority priority) {
		if (_movementRequests [priority] == null) {
			_movementRequests [priority] = new List<Vector3> ();
		}
		_movementRequests[priority].Add (movement);
	}
}
}