using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueChase : MonoBehaviour {
	public MovementPriority priority = MovementPriority.HIGH;
	public float percentMaxSpeed = 0.8f;
	public float lookDistance = 50;
	public float aggroDistance = 15;
	public float aggroDistanceBuffer = 5;
	public float timeBetweenGrowls = 1f;
	public Transform target;
	public AudioSource growlSource;
	public AudioSource bigGrowlSource;

	private static float _lastGrowlTime = 0;
	private static float _lastBigGrowlTime = 0;
	private bool _wasLooking = false;
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
		float effectiveAggro = aggroDistance;
		if (_state.isChasing) {
			effectiveAggro += aggroDistanceBuffer;
		}

		Vector3 toTarget = target.position - transform.position;
		float distSq = toTarget.sqrMagnitude;
		if (distSq < lookDistance*lookDistance) {
			transform.LookAt (target.position);

			if (!_wasLooking) {
				if (Time.time - _lastGrowlTime > timeBetweenGrowls) {
					growlSource.Play ();
					_lastGrowlTime = Time.time;
				}
				_wasLooking = true;
			}
		} else {
			_wasLooking = false;
		}
		if (distSq > effectiveAggro * effectiveAggro) {
			_state.isChasing = false;
			return;
		}

		if (!_state.isChasing) {
			if (Time.time - _lastBigGrowlTime > timeBetweenGrowls) {
				_lastBigGrowlTime = Time.time;
				bigGrowlSource.Play ();
			}
			_state.isChasing = true;
		}

		toTarget.y = 0;
		toTarget.Normalize ();
		toTarget *= percentMaxSpeed * _state.maxSpeed;

		_movementController.AddMovement (toTarget, priority);
	}

}
}