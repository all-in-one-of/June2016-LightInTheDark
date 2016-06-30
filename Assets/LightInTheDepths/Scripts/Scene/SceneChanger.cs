using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	public string sceneToLoad;

	private AsyncOperation _loadOp;
	void Start() {
		
		_loadOp = SceneManager.LoadSceneAsync (sceneToLoad);
		_loadOp.allowSceneActivation = false;
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.SendMessage ("OnExitLevel");

			StartCoroutine ("StartTransition", other.gameObject);
		}
	}

	IEnumerator StartTransition(GameObject player) {
		Camera cam = GameObject.FindObjectOfType<Camera> ();
		OrbitCamera orbit = cam.GetComponent<OrbitCamera> ();
		if (orbit != null) {
			Destroy (orbit);
		}
		LookAt look = cam.gameObject.AddComponent<LookAt> ();
		look.toLookAt = player.transform;
		yield return new WaitForSeconds(1.0f);
		_loadOp.allowSceneActivation = true;
	}
}
