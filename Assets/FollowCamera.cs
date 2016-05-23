using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	[SerializeField] private Transform _target;

	public float followDistance = 5.0f;
	public float positionDamping = 0.05f;


	private Vector3 _velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
		Vector3 forward = _target.transform.forward * followDistance;
		Vector3 needPos = _target.transform.position - forward;
		transform.position = Vector3.SmoothDamp(transform.position, needPos,
			ref _velocity,positionDamping);
		transform.LookAt (_target.transform);
		transform.rotation = _target.transform.rotation;
	}
}
