using UnityEngine;
using System.Collections;

public class CharacterControllerWalker : MonoBehaviour {
	public float speed = 20.0f;
	public float angularSpeed = 100.0f;
	public float gravityStrength = 98f;
	public GameObject planetaryBody;

	private Vector3 _relativeDown;
	private CharacterController _controller;
	private Animator _anim;
	private Vector3 facing = Vector3.forward;
	private Vector3 velocity;
	void Start(){
		_controller = GetComponent<CharacterController> ();
		_anim = GetComponentInChildren<Animator> ();
	}

	void FixedUpdate () {
		UpdateDown ();	
		UpdateVelocity ();
		FixRotation ();
	}

	void UpdateVelocity() {
		velocity += GetGravitationalAcceleration () * Time.deltaTime;

		if (_controller.collisionFlags == 0) {
			_controller.Move (velocity * Time.deltaTime);
		}
		
		Vector3 inputMovement = Vector3.zero;
		if (_controller.collisionFlags > 0) {
			inputMovement = GetInputMovement ();
			velocity = inputMovement;
		}
			
		_controller.Move (velocity * Time.deltaTime);


		Debug.Log("Moved " + velocity * Time.deltaTime);
		Debug.Log("Grounded " + _controller.isGrounded);
		Debug.Log(" ");
		_anim.SetFloat("Speed", inputMovement.magnitude);
	}

	void UpdateDown() {
		_relativeDown = (planetaryBody.transform.position - transform.position).normalized;
	}

	void FixRotation() {
		float deltaX = Input.GetAxis ("Horizontal") * angularSpeed * Time.deltaTime;
		facing = Quaternion.Euler (0, deltaX, 0) * facing;

		Vector3 lookatPoint = Vector3.Cross(transform.right + facing, -_relativeDown);
		transform.LookAt(transform.position + lookatPoint, -_relativeDown);
	}

	Vector3 GetInputMovement() {
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		if (deltaZ == 0) {
			return Vector3.zero;
		}

		Vector3 inputMovement = new Vector3(0, 0, -deltaZ);
		inputMovement = Vector3.ClampMagnitude (inputMovement, speed);
		return transform.TransformDirection(inputMovement);
	}

	Vector3 GetGravitationalAcceleration() {
		return gravityStrength * _relativeDown;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject == planetaryBody) {
			Vector3 towardPlanet = Vector3.Project (velocity, _relativeDown);
			velocity -= towardPlanet;
		}
	}

	void OnDrawGizmos() {

		//Debug.DrawLine(transform.position, transform.position + _controller.velocity, Color.red);

	}
}
