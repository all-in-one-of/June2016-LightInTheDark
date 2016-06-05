using UnityEngine;
using System.Collections;

public class GoForward : MonoBehaviour {
	public float speed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (Vector3.forward * (speed * Time.deltaTime));
	}
}
