using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(PlayerState))]
public class PlayerPuncher : MonoBehaviour {
	public float punchTime = 0.25f;
	public float punchReach = 3;

	private PlayerState _state;	
	private float _tillEndPunch = 0;
	void Start () {
		_state = GetComponent<PlayerState> ();
	}

	void Update () {
		if (_state.isPunching) {
			UpdatePunch ();
		} else {
			CheckStartPunch ();
		}
	}

	void UpdatePunch() {
		_tillEndPunch -= Time.deltaTime;

		if (_tillEndPunch <= 0) {
			_state.isPunching = false;
		}
	}

	void CheckStartPunch() {
		if (Input.GetButtonDown ("Punch")) {
			_tillEndPunch = punchTime;
			_state.isPunching = true;
			TriggerPunchEffects ();
		}
	}

	void TriggerPunchEffects() {
		RaycastHit hit;

		if (Physics.Raycast (transform.position, punchReach * transform.forward, out hit)) {
			GameObject hitObject = hit.collider.gameObject;
			Breakable breakable = hitObject.GetComponent<Breakable> ();

			if (breakable != null) {
				breakable.Break ();
			}
		}
	}
}

}