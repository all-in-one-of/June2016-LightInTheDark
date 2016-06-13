using UnityEngine;
using System.Collections;

namespace LightInTheDark {

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

	public void AddMovement(Vector3 movement) {
		_nextMovement += movement;
	}
}

}