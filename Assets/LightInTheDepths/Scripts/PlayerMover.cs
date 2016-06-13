using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	private Vector3 _nextMovement = Vector3.zero;
	private PlayerState _state;

	// Use this for initialization
	void Start () {
		_state = GetComponent<PlayerState> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_state.CharController.Move (_nextMovement);
		_nextMovement.Set (0, 0, 0);
	}

	public void addMovement(Vector3 movement) {
		_nextMovement += movement;
	}
}
