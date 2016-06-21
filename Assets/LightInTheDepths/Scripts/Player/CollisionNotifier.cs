using UnityEngine;
using System.Collections;

namespace LightInTheDark {
public class CollisionNotifier : MonoBehaviour {
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.collider.tag != "Ground") {
			JumperLight jumper = hit.gameObject.GetComponent<JumperLight> ();

			if (jumper != null) {
				jumper.OnPlayerCollided (transform.position);
			}
		}
	}
}
}