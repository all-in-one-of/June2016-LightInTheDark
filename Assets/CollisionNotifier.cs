using UnityEngine;
using System.Collections;

public class CollisionNotifier : MonoBehaviour {
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.collider.tag != "Ground") {
			hit.collider.gameObject.SendMessage ("OnPlayerCollision", hit);
		}
	}
}
