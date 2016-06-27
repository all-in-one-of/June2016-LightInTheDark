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
			_loadOp.allowSceneActivation = true;
		}
	}
}
