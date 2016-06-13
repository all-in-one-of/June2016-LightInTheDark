using UnityEngine;
using System.Collections;

public class SimplePlanetWatcher : MonoBehaviour {
	public GameObject planet;

	public float dampTime = 0.5f;
	public float distance = 50;
	public float targetDegrees;
	private Vector3 _velocity;

	// Use this for initialization
	void Start () {
		transform.position = planet.transform.position + planet.transform.right * planet.transform.lossyScale.x;
		transform.LookAt (planet.transform.position);
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 offset = Quaternion.Euler (new Vector3 (0, targetDegrees, 0)) * Vector3.forward;
		offset.Normalize ();
		offset *= distance;
		Vector3 target = planet.transform.position + offset;

		transform.position = target;
		transform.LookAt (planet.transform.position);		
	}
}
