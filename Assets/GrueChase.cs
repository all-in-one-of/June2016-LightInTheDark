using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueChase : MonoBehaviour {
	public MovementPriority priority = MovementPriority.HIGH;
	public float percentMaxSpeed = 0.8f;
	public float aggroDistance = 5;
	public float aggroDistanceBuffer = 1;
	public Transform target;

	private GrueState _state;
	private GrueMovement _movementController;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
	}

	// Update is called once per frame
	void Update () {
		float effectiveAggro = aggroDistance;
		if (_state.isChasing) {
			effectiveAggro += aggroDistanceBuffer;
		}
		float aggroDistanceSq = effectiveAggro * effectiveAggro;

		Vector3 toTarget = target.position - transform.position;
		if (toTarget.sqrMagnitude > aggroDistanceSq) {
			return;
		}

		toTarget.y = 0;
		toTarget.Normalize ();
		toTarget *= percentMaxSpeed * _state.maxSpeed;

		_movementController.AddMovement (toTarget, priority);
	}

}
}