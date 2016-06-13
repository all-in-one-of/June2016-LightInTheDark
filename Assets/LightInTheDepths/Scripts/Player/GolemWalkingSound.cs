using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(PlayerState))]
public class GolemWalkingSound : MonoBehaviour {
	[SerializeField]
	private AudioSource _walkingSource;

	private PlayerState _state;	

	void Start() {
		_state = GetComponent<PlayerState> ();
	}

	void Update () {
		bool shouldClipPlay = _state.isWalking && !_state.isJumping;

		if (shouldClipPlay && !_walkingSource.isPlaying) {
			_walkingSource.Play ();
		}

		if (!shouldClipPlay && _walkingSource.isPlaying) {
			_walkingSource.Stop ();
		}
	}
		
}

}