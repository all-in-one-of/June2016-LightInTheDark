using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(PlayerState))]
public class PlayerPuncher : MonoBehaviour {
	public float punchTime = 0.3f;
	public float punchReach = 3;
	public GameObject chest; 

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
		RaycastHit[] hits = Physics.RaycastAll (chest.transform.position, punchReach * transform.forward);

		foreach(RaycastHit hit in hits) {
			GameObject hitObject = hit.collider.gameObject;
			Breakable breakable = hitObject.GetComponent<Breakable> ();

			if (breakable != null) {
				breakable.Break ();
			}
		}

		hits = Physics.RaycastAll (chest.transform.position, 3 * punchReach * transform.forward);

		foreach (RaycastHit hit in hits) {
			GameObject hitObject = hit.collider.gameObject;
			Punchable punchable = hitObject.GetComponent<Punchable> ();

			if (punchable != null) {
				punchable.OnPunched (transform.position);
			}
		}
	}
}

}