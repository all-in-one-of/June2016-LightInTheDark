using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {
	public float gravityAccel = 9.8f;

	private Rigidbody _body;
	private GameObject[] _planets;
	private GameObject _closestPlanet;
	// Use this for initialization
	void Start () {
		_planets = GameObject.FindGameObjectsWithTag ("Planet");
		_body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateClosestPlanet ();
		_body.AddForce (getGravityVector ());


	}

	Vector3 getGravityVector() {
		Vector3 toClosest = (_closestPlanet.transform.position - transform.position);
		float distSq = toClosest.sqrMagnitude;
		toClosest /= Mathf.Sqrt (distSq);


		return gravityAccel * toClosest / distSq;
	}

	void UpdateClosestPlanet() {
		GameObject closestPlanet = _planets[0];
		float closestDistance = GetDistanceSqToPlanet (0);

		for (int index = 1; index < _planets.Length; index++) {
			float dist = GetDistanceSqToPlanet (index);
			if (dist < closestDistance) {
				closestDistance = dist;
				closestPlanet = _planets[index];
			}
		}
		_closestPlanet = closestPlanet;
	}

	float GetDistanceSqToPlanet(int index) {
		return (transform.position - _planets [index].transform.position).sqrMagnitude;
	}
}
