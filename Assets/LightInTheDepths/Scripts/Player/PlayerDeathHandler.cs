using UnityEngine;
using System.Collections;

namespace LightInTheDark {
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(Animator))]
public class PlayerDeathHandler : MonoBehaviour {
	private PlayerState _state;	

	void Start() {
		_state = GetComponent<PlayerState> ();
	}

	void OnKilledByGrue() {
		_state.isDieing = true;
		Application.Quit(); // TODO: Handle properly
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
}