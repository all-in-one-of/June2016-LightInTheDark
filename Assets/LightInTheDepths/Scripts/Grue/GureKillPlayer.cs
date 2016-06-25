using UnityEngine;
using System.Collections;
namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
public class GureKillPlayer : MonoBehaviour {
	public float killDistance = 12;
	private GameObject _player;
	private GrueState _state;
	void Start () {
		_state = GetComponent<GrueState> ();
		_player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		float playerDistSq = (_player.transform.position - transform.position).sqrMagnitude;

		if (playerDistSq < killDistance * killDistance) {
			KillPlayer ();
		}
	}
		
	void KillPlayer() {
		_player.SendMessage ("OnKilledByGrue");
	}
}
}