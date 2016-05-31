using UnityEngine;
using System.Collections;

public class CircleRailWalker : MonoBehaviour {
	public GameObject parentPlanet;
	public float movementSpeed = 10;
	private Vector2 _degreesAround = new Vector2(0,0);
	private Animator _animator;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_animator.SetFloat ("Speed", movementSpeed);
		_degreesAround.x += movementSpeed * Time.deltaTime;
		_degreesAround.y += movementSpeed * Time.deltaTime;

		Vector3 planetForward = parentPlanet.transform.forward;
		Vector3 offset = Quaternion.Euler (0, _degreesAround.x, _degreesAround.y) * planetForward;
		offset *= parentPlanet.transform.localScale.x/2;

		transform.position = parentPlanet.transform.position + planetForward + offset;
		Vector3 toPlanet = transform.position - parentPlanet.transform.position;
		toPlanet.Normalize ();


		transform.rotation = Quaternion.FromToRotation(transform.forward, _degreesAround.normalized) * transform.rotation;

		Vector3 lookatPoint = Vector3.Cross(transform.right, toPlanet);
		transform.LookAt (transform.position + lookatPoint, toPlanet);
	}
}
