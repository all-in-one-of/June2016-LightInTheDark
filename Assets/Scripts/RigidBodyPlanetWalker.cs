using UnityEngine;
using System.Collections;

public class RigidBodyPlanetWalker : MonoBehaviour {
	public float speed = 100.0f;
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
		updateDown ();

		_body.velocity += getGravitationalAcceleration();

		_body.velocity = Vector3.Project (_body.velocity, _relativeDown);
		Vector3 inputMovement = getInputMovement ();
		_body.velocity += inputMovement;
		_body.transform.up = -_relativeDown;

		_anim.SetFloat("Speed", inputMovement.magnitude);
	}

	void updateDown() {
		_relativeDown = (planetaryBody.transform.position - transform.position).normalized;
	}

	Vector3 getInputMovement() {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 inputMovement = new Vector3(deltaX, 0, deltaZ);
		inputMovement = Vector3.ClampMagnitude (inputMovement, speed);
		return transform.TransformDirection(inputMovement);
	}

	Vector3 getGravitationalAcceleration() {
		return gravityStrength * _relativeDown;
	}
}
