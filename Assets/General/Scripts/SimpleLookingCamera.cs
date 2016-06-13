using UnityEngine;
using System.Collections;

public class SimpleLookingCamera : MonoBehaviour {
	public GameObject targetObject = null;
	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		if (targetObject != null) {
			var target = targetObject.transform;
			Vector3 targetLook = target.position - transform.position;
			Quaternion targetRot = Quaternion.LookRotation (targetLook);
			transform.rotation = Quaternion.Lerp (transform.rotation, targetRot, 0.05f);
		}
	}
}
