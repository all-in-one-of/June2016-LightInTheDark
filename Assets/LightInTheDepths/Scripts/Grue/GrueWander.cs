using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueWander : MonoBehaviour {
	public MovementPriority priority = MovementPriority.LOW;
	public float percentMaxSpeed = 0.33f;
	public float distance = 20.0f;
	public float radius = 4.0f;
	public float jitter = 2.0f;

	private GrueState _state;
	private GrueMovement _movementController;
	private float _angle = 0;

	// Use this for initialization
	void Start () {
		_angle = Random.value * 100;
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
		_angle = GetRandomWithNeg() * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFacingWall ()) {
			transform.LookAt (transform.position - transform.forward);
		}
		_angle += jitter * Time.deltaTime * GetRandomWithNeg ();

		Vector3 circleLocation = new Vector3(Mathf.Cos(_angle), transform.position.y, Mathf.Sin(_angle));

		Vector3 circleCenter = transform.position + distance * transform.forward;
		circleCenter.y = transform.position.y;
		Vector3 target = circleCenter + circleLocation;
		Vector3 toTarget = target - transform.position;
		toTarget.y = 0;
		toTarget.Normalize ();

		_movementController.AddMovement (percentMaxSpeed * _state.maxSpeed * toTarget, priority);
	}

	bool isFacingWall() {
		RaycastHit hit;
		bool gotOne = Physics.Raycast (transform.position, transform.forward, out hit, 8) || Physics.Raycast (transform.position, 3 * transform.forward - transform.up, out hit, 4);
		if (!gotOne) {
			return false;
		}

		return hit.collider.tag != "Player" && hit.collider.tag != "Grue";
	}

	float GetRandomWithNeg() {
		return 2 * (Random.value - 0.5f);
	}
}

}