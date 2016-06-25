using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class TenticleState : MonoBehaviour {
	public float attackFrequency = 10;
	public float attackDuration = 5;
	public float tillNextAttack;
	public bool attacking = false;

	public float PercentToAttack {
		get {
			return 1 - (tillNextAttack) / attackFrequency;
		}
	}

	void Awake() {
		tillNextAttack = attackFrequency;
	}
	void Update() {
		tillNextAttack -= Time.deltaTime;
	}
}

}