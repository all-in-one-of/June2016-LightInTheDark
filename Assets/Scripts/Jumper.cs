using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour {

	public float minJumpTime = 1;
	public float maxJumpTime = 3;
	public float minJumpForce = 1000;
	public float maxJumpForce = 10000;
	private Rigidbody _body;
	private float _tillJump;

	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody> ();
		_tillJump = Random.value;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_tillJump -= Time.deltaTime;

		if (_tillJump <= 0) {
			Jump ();
			DecideNextJumpTime ();
		}
	}

	void Jump() {
		float force = Random.value * (maxJumpForce - minJumpForce) + minJumpForce;
		_body.AddForce (force * Vector3.up);
	}
		
	void DecideNextJumpTime() {
		_tillJump = minJumpTime + Random.value * (maxJumpTime - minJumpTime);
	}
}
