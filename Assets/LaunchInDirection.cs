using UnityEngine;
using System.Collections;

public class LaunchInDirection : MonoBehaviour {
	public float launchForce = 1000;
	public Vector3 launchDirection = new Vector3(0,1,1);

	private bool _didLaunch = false;
	private bool _doLaunch = false;
	private LineRenderer shootVisualizer;
	private int _lineVertCount = 4;

	private Rigidbody _body;
	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody> ();
		shootVisualizer = GetComponent<LineRenderer> ();
		shootVisualizer.SetVertexCount (_lineVertCount);
		shootVisualizer.SetWidth (0.5f, 0.1f);
		shootVisualizer.SetColors (Color.blue, Color.blue);

		shootVisualizer.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_didLaunch && Input.GetButtonDown ("Launch")) {
			_doLaunch = true; 
		}

		if (!_didLaunch) {
			DrawShootEstimate ();
		}
	}

	void DrawShootEstimate () {
		//Vector3
		float dist = launchForce / 100;

		for (int index = 0; index < _lineVertCount; index++) {
			shootVisualizer.SetPosition (index, transform.position + dist/_lineVertCount * index * launchDirection);	
		}
	}

	void FixedUpdate() {
		if(_doLaunch) {
			Launch ();
			MakeCameraFollowMe ();
		}
	}

	void Launch() {
		_doLaunch = false;
		_didLaunch = true;
		Debug.Log ("Launching");
		var force = launchForce * launchDirection.normalized;
		_body.AddForce (force);
		shootVisualizer.SetVertexCount (0);
	}

	void MakeCameraFollowMe () {
		FindObjectOfType<SimpleLookingCamera> ().targetObject = gameObject;
	}
}
