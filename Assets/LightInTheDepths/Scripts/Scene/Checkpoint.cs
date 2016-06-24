using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	public float distanceToGet = 10;
	private bool _playerHasBeenNear = false;
	private GameObject _player;
	// Use this for initialization
	void Start () {
		Object.DontDestroyOnLoad (this);
		_player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (_player.transform.position, transform.position) < distanceToGet) {
			_playerHasBeenNear = true;
		}
	}

	void OnLevelWasLoaded(int level) {
		_player = GameObject.FindGameObjectWithTag ("Player");
		if (_playerHasBeenNear) {
			_player.transform.position = transform.position;
		}
	}
}
