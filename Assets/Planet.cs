using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public GameObject parentPlanet = null;
	public float orbitingDistance = 500;
	public float rotationDegPerSec = 10;
	public float orbitDegPerSec = 0.1f; 
	public float startingOrbitDeg = 0;
	// Use this for initialization
	void Start () {
		RevolveBy (startingOrbitDeg);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		RotateBy (rotationDegPerSec * Time.deltaTime * 10);
		RevolveBy (orbitDegPerSec * Time.deltaTime * 10);
	}

	void RotateBy (float degrees) { 
		transform.Rotate (new Vector3 (0, degrees, 0));
	}

	void RevolveBy (float degrees) { 
		if (parentPlanet == null) {
			return;
		}

		Vector3 newOffset = transform.position - parentPlanet.transform.position;
		newOffset.Normalize();
		newOffset = Quaternion.Euler(new Vector3(0, degrees, 0)) * newOffset;
		newOffset *= orbitingDistance;
		transform.position = parentPlanet.transform.position + newOffset;
	}
}
