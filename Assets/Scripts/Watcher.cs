using UnityEngine;
using System.Collections;

public class Watcher : MonoBehaviour {
	public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt (target);
	}
}
