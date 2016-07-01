using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace LightInTheDark {
[RequireComponent(typeof(PlayerState))]
public class PlayerDeathHandler : MonoBehaviour {
	public AudioSource deathSound;
	public float deathTime = 2.0f;
	public bool deathEnabled = true;
	private PlayerState _state;	

	void Start() {
		_state = GetComponent<PlayerState> ();
	}

	void OnKilledByGrue() {
		if (!_state.isDieing && deathEnabled) {
			_state.isDieing = true;
			StartCoroutine("Die");
		}
	}

	void OnKilledByTenticle() {
		if (!_state.isDieing && deathEnabled) {
			_state.isDieing = true;
			StartCoroutine("Die");
		}
	}

	IEnumerator Die() {
		_state.isDieing = true;
		deathSound.Play ();

		float step = 1 / 60.0f;
		float shrinkPerStep = 0.95f / (deathTime / step);

		float tillDone = deathTime;

		while (tillDone > 0) {
			Vector3 scale = transform.localScale;
			scale.x -= shrinkPerStep;
			scale.y -= shrinkPerStep;
			scale.z -= shrinkPerStep;
			transform.localScale = scale;
			yield return new WaitForSeconds (step);
			tillDone -= step;
		}

		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
}
}