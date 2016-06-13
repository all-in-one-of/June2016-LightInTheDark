using UnityEngine;
using System.Collections;

public class PlanetWatcher : MonoBehaviour {
	public GameObject planet;
	public GameObject player;
	public float dampTime = 0.5f;

	private Vector3 _velocity;

	// Use this for initialization
	void Start () {
		transform.position = planet.transform.position + planet.transform.right * planet.transform.lossyScale.x;
		transform.LookAt (planet.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 playerUp = player.transform.up;

		if (Mathf.Abs(playerUp.y) < 0.7f) {
			playerUp.y = 0;
		}
		playerUp.Normalize ();

		Vector3 target = planet.transform.position + playerUp * planet.transform.lossyScale.x;
		transform.position = Vector3.SmoothDamp (transform.position, target, ref _velocity, 0.5f, 50);
		transform.LookAt (planet.transform.position);		
	}
}
