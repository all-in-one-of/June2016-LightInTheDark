using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public Transform toLookAt;
	public Vector3 staticRotate = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (toLookAt);
		transform.Rotate (staticRotate);
	}
}
