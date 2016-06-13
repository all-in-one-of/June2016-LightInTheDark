using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {
	public float gravityAccel = 9.8f;

	private Rigidbody _body;
	private GameObject[] _planets;
	// Use this for initialization
	void Start () {
		_planets = GameObject.FindGameObjectsWithTag ("Planet");
		_body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for (int index = 0; index < _planets.Length; index++) {
			_body.AddForce (getGravityVector (_planets[index]));
		}
	}

	Vector3 getGravityVector(GameObject planet) {
		Vector3 toPlanet = (planet.transform.position - transform.position);
		float distSq = toPlanet.sqrMagnitude;

		const float MAX_DIST_SQ = 1000 * 1000;
		if (distSq > MAX_DIST_SQ) {
			return Vector3.zero;
		}

		toPlanet /= Mathf.Sqrt (distSq);
		return gravityAccel * toPlanet / distSq;
	}
		

	float GetDistanceSqToPlanet(int index) {
		return (transform.position - _planets [index].transform.position).sqrMagnitude;
	}
}
