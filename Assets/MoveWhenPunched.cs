using UnityEngine;
using System.Collections;

namespace LightInTheDark {
[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class MoveWhenPunched : MonoBehaviour, Punchable {
	public float punchMoveSpeed = 0.75f;
	public float timeToBePunched = 0.75f;
	public AudioSource _punchedSound;
	private GrueState _state;
	private GrueMovement _movementController;
	private Vector3 _lastPunchDirection;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPunched(Vector3 punchSource) {
		_lastPunchDirection = punchSource - transform.position;
		_lastPunchDirection.Normalize ();
		StartCoroutine ("MoveAway");
	}

	private IEnumerator MoveAway() {
		_punchedSound.Play ();
		float remaining = timeToBePunched;
		float stepTime = 1/60.0f;
		while (remaining > 0) {
			_movementController.AddMovement (_state.maxSpeed * punchMoveSpeed * _lastPunchDirection, MovementPriority.MAX);
			remaining -= stepTime;
			yield return new WaitForSeconds (stepTime);
		}
	}
}
}