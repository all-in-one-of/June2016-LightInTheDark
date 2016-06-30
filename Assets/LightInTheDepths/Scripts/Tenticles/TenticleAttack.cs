using UnityEngine;
using System.Collections;

namespace LightInTheDark {
[RequireComponent(typeof(TenticleState))]
public class TenticleAttack : MonoBehaviour {
	public GameObject tenticleArms;
	public float tenticleSpeedUp = 100;
	public float tenticleSpeedDown = 10;
	public float tenticleAttackHeight = 5;
	public bool randomStartPercent = true;
	public AudioSource attackSound;

	private TenticleState _state;
	// Use this for initialization
	void Start () {
		_state = GetComponent<TenticleState> ();

		if (randomStartPercent) {
			_state.tillNextAttack = Random.value * _state.attackFrequency;
		}
	}

	// Update is called once per frame
	void Update () {
		if (_state.tillNextAttack <= 0 && !_state.attacking) {
			StartAttack ();
		}
	}

	void StartAttack() {
		_state.attacking = true;
		attackSound.Play ();
		StartCoroutine ("AttackRoutine");
	}

	IEnumerator AttackRoutine() {
		float totalTime = _state.attackDuration;
		Vector3 origPosition = tenticleArms.transform.position;
		Vector3 movement = tenticleAttackHeight * Vector3.up;
		Vector3 attackPosition = tenticleArms.transform.position + movement;
		float dist = movement.magnitude;
		float moveTimeUp = dist / tenticleSpeedUp;
		float moveTimeDown = dist / tenticleSpeedDown;
		movement /= dist;

		IEnumerator raise = TenticleMove (moveTimeUp, 1/60.0f, tenticleSpeedUp, movement);
		IEnumerator lower = TenticleMove (moveTimeDown, 1/60.0f, tenticleSpeedDown, -movement);

		while (raise.MoveNext ()) {
			yield return raise.Current;
		}

		yield return new WaitForSeconds(totalTime - moveTimeUp - moveTimeDown);

		while (lower.MoveNext ()) {
			yield return raise.Current;
		}

		tenticleArms.transform.position = origPosition; // To avoid floating point errors

		_state.tillNextAttack = _state.attackFrequency;
		_state.attacking = false;
	}


	IEnumerator TenticleMove(float totalTime, float stepTime, float speed, Vector3 direction) {
		float remaining = totalTime;

		while (remaining > 0) {
			tenticleArms.transform.Translate (speed * stepTime * direction);
			remaining -= stepTime;
			yield return new WaitForSeconds (stepTime);
		}
	}

}
}