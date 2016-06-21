using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace LightInTheDark {
[RequireComponent(typeof(PlayerState))]
public class PlayerDeathHandler : MonoBehaviour {
	public bool deathEnabled = true;
	private PlayerState _state;	

	void Start() {
		_state = GetComponent<PlayerState> ();
	}

	void OnKilledByGrue() {
		if (deathEnabled) {
			_state.isDieing = true;
			SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		}
	}
}
}