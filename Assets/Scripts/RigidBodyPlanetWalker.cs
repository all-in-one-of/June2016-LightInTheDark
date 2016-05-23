using UnityEngine;
using System.Collections;

public class RigidBodyPlanetWalker : MonoBehaviour {
	public float speed = 6.0f;
	public float gravityStrength = 9.8f;
	public GameObject planetaryBody;

	private Rigidbody _body;

	void Start(){
		_body = GetComponent<Rigidbody> ();
		_body.constraints = RigidbodyConstraints.FreezeRotationY;
	}

	void FixedUpdate () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed);
		Vector3 toPlanet = (planetaryBody.transform.position - transform.position).normalized;

		movement += (gravityStrength * toPlanet);

		Debug.Log ("movement " + movement + " toward " + toPlanet);

		_body.velocity = movement;
		_body.transform.up = -toPlanet;
	}
}
