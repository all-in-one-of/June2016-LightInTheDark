using UnityEngine;
using System.Collections;

public class CharacterControllerPlanetWalk : MonoBehaviour {
	public float speed = 6.0f;
	public float gravityStrength = 9.8f;
	public GameObject planetaryBody;

	private CharacterController _charController;

	void Start(){
		_charController = GetComponent<CharacterController> ();
	}

	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed);
		Vector3 toPlanet = (planetaryBody.transform.position - transform.position).normalized;
		movement += (gravityStrength * toPlanet);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection(movement);
		_charController.Move (movement);
	}
}
