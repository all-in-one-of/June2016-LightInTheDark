using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueAvoidTenticle : MonoBehaviour {

	public MovementPriority priority = MovementPriority.MAX;
	public float desiredDistance = 3.0f;
	public float grayArea = 5.0f;
	public float fleeSpeedPercent = 0.75f;

	private GrueState _state;
	private GrueMovement _movementController;
	private GameObject[] _tenticles;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
		_tenticles = GameObject.FindGameObjectsWithTag("Tenticle");
	}

	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;

		foreach(GameObject tenticle in _tenticles) {
			if (tenticle != null) {
				updateMovementFromTenticle (ref movement, tenticle);
			}
		}

		if(movement == Vector3.zero) {
			return;
		}

		_movementController.AddMovement (movement, priority);
	}

	//TODO: Look into ways to unduplicate this logic with GrueAvoidLight
	void updateMovementFromTenticle (ref Vector3 movement, GameObject tenticle) {
		float avoidDistance = desiredDistance;
		float fleeDistance = avoidDistance + grayArea;

		Vector3 fromLight = transform.position - tenticle.transform.position;
		fromLight.y = 0;
		float distSq = fromLight.sqrMagnitude;
		if (distSq > fleeDistance*fleeDistance) {
			return; // Outside avoid range
		}

		bool fleeing = distSq > avoidDistance*avoidDistance;
		float speedPercent = fleeing ? fleeSpeedPercent : 1.0f;
		fromLight.y = 0;
		fromLight.Normalize ();
		fromLight *= speedPercent * _state.maxSpeed;

		movement += fromLight;
	}

}
}