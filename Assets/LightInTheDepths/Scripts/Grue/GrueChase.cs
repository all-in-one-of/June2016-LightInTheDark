using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueChase : MonoBehaviour {
	public MovementPriority priority = MovementPriority.HIGH;
	public float percentMaxSpeed = 0.8f;
	public float aggroDistance = 15;
	public float aggroDistanceBuffer = 5;
	public Transform target;

	private GrueState _state;
	private GrueMovement _movementController;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();

		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}
	}

	// Update is called once per frame
	void Update () {
		transform.LookAt (target.position);
		float effectiveAggro = aggroDistance;
		if (_state.isChasing) {
			effectiveAggro += aggroDistanceBuffer;
		}
		float aggroDistanceSq = effectiveAggro * effectiveAggro;

		Vector3 toTarget = target.position - transform.position;
		if (toTarget.sqrMagnitude > aggroDistanceSq) {
			_state.isChasing = false;
			return;
		}
		_state.isChasing = true;
		toTarget.y = 0;
		toTarget.Normalize ();
		toTarget *= percentMaxSpeed * _state.maxSpeed;

		_movementController.AddMovement (toTarget, priority);
	}

}
}