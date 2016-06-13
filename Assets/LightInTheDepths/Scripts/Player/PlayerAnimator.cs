using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class PlayerAnimator : MonoBehaviour {
	private Animator _animator;
	private PlayerState _state;	

	void Start() {
		_state = GetComponent<PlayerState> ();
		_animator = GetComponentInChildren<Animator> ();
	}

	void Update () {
		_animator.SetBool ("Walking", _state.isWalking);
		_animator.SetBool ("Jumping", _state.isJumping);
	}
}

}