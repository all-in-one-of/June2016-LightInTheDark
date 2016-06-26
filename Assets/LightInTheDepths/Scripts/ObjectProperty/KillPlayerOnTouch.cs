using UnityEngine;
using System.Collections;

public class KillPlayerOnTouch : MonoBehaviour {
	private GameObject _player;

	private Collider _collider;
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		_collider = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(_collider.bounds.Contains(_player.transform.position)) {
			_player.SendMessage ("OnKilledByTenticle");
		}
	}
}
