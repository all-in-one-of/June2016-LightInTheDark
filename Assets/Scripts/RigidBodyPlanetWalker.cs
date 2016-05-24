using UnityEngine;
using System.Collections;

public class RigidBodyPlanetWalker : MonoBehaviour {
	public float speed = 100.0f;
	public float angularSpeed = 100.0f;
	public float gravityStrength = 9.8f;
	public GameObject planetaryBody;

	private Vector3 _relativeDown;
	private Rigidbody _body;
	private Animator _anim;

	void Start(){
		_body = GetComponent<Rigidbody> ();
		_body.constraints = RigidbodyConstraints.FreezeRotationY;
		_anim = GetComponentInChildren<Animator> ();
	}

	void FixedUpdate () {
		UpdateDown ();	
		UpdateVelocity ();
		FixRotation ();
	}

	void UpdateVelocity() {
		_body.velocity += GetGravitationalAcceleration() * Time.deltaTime;
		Vector3 towardPlanet = Vector3.Project (_body.velocity, _relativeDown);
		_body.velocity = towardPlanet;

		Vector3 inputMovement = GetInputMovement ();
		_body.velocity += inputMovement;

		_anim.SetFloat("Speed", inputMovement.magnitude);
	}

	void UpdateDown() {
		_relativeDown = (planetaryBody.transform.position - transform.position).normalized;
	}

	void FixRotation() {
		Vector3 lookatPoint = Vector3.Cross(transform.right, -_relativeDown);
		transform.LookAt (transform.position + lookatPoint, -_relativeDown);

		float deltaX = Input.GetAxis ("Horizontal") * angularSpeed * Time.deltaTime;
		transform.Rotate (0, deltaX, 0);
	}

	Vector3 GetInputMovement() {
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 inputMovement = new Vector3(0, 0, deltaZ);
		inputMovement = Vector3.ClampMagnitude (inputMovement, speed);
		return transform.TransformDirection(inputMovement);
	}

	Vector3 GetGravitationalAcceleration() {
		return gravityStrength * _relativeDown;
	}
}
