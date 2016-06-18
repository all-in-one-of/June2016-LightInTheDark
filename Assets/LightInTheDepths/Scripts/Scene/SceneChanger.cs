using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	public string sceneToLoad;
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
		}
	}
}
