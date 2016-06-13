using UnityEngine;
using System.Collections;

public class LaunchInDirection : MonoBehaviour {
	public float launchForce = 1000;
	public float rotSpeed = 90;
	public Vector3 launchDirection = new Vector3(0,1,1);

	private bool _didLaunch = false;
	private bool _doLaunch = false;
	private LineRenderer _shotVisualizer;
	private int _lineVertCount = 4;
	private Rigidbody _body;

	// Use this for initialization
	void Start () {
		_body = GetComponent<Rigidbody> ();
		_shotVisualizer = GetComponent<LineRenderer> ();
		_shotVisualizer.SetVertexCount (_lineVertCount);
		_shotVisualizer.SetWidth (0.5f, 0.1f);
		_shotVisualizer.SetColors (Color.blue, Color.blue);

		_shotVisualizer.material.color = Color.blue;


		_body.mass = float.PositiveInfinity;
	}

	// Update is called once per frame
	void Update () {
		if (_didLaunch) {
			if (Input.GetButtonDown ("Launch")) {
				_didLaunch = false; 
				_shotVisualizer.SetVertexCount (_lineVertCount);
			}
			return;
		}

		if (Input.GetButtonDown ("Launch")) {
			_doLaunch = true; 
		}

		float toRot = Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;
		Quaternion newRotation = Quaternion.Euler (new Vector3 (0, toRot, 0));
		launchDirection = newRotation * launchDirection;

		DrawShootEstimate ();
	}

	void DrawShootEstimate () {
		//Vector3
		float dist = launchForce / 100;

		for (int index = 0; index < _lineVertCount; index++) {
			_shotVisualizer.SetPosition (index, transform.position + dist/_lineVertCount * index * launchDirection);	
		}
	}

	void FixedUpdate() {
		if(_doLaunch) {
			Launch ();
			MakeCameraFollowMe ();
		}
	}

	void Launch() {
		_body.mass = 1;
		_doLaunch = false;
		_didLaunch = true;
		Debug.Log ("Launching");
		var force = launchForce * launchDirection.normalized;
		_body.AddForce (force);
		_shotVisualizer.SetVertexCount (0);
	}

	void MakeCameraFollowMe () {
		FindObjectOfType<SimpleLookingCamera> ().targetObject = gameObject;
	}
}
