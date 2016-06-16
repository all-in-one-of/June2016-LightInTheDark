using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueWander : MonoBehaviour {
	public MovementPriority priority = MovementPriority.LOW;
	public float percentMaxSpeed = 0.33f;

	public float distance = 5.0f;
	public float radius = 1.0f;
	public float jitter = 1.0f;

	private GrueState _state;
	private GrueMovement _movementController;
	private float _angle = 0;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
		_angle = GetRandomWithNeg() * Mathf.PI;
	}
	
	// Update is called once per frame
	void Update () {
		_angle += jitter * Time.deltaTime * GetRandomWithNeg ();

		Vector3 circleLocation = new Vector3(Mathf.Cos(_angle), transform.position.y, Mathf.Sin(_angle));
		Vector3 ahead = transform.forward;
		Vector3 circleCenter = transform.position + distance * ahead;
		circleCenter.y = transform.position.y;
		Vector3 target = circleCenter + circleLocation;
		Vector3 toTarget = target - transform.position;
		toTarget.y = 0;
		toTarget.Normalize ();

		Debug.Log (_angle);
		_movementController.AddMovement (percentMaxSpeed * _state.maxSpeed * toTarget, priority);
	}

	float GetRandomWithNeg() {
		return 2 * (Random.value - 0.5f);
	}
}

}