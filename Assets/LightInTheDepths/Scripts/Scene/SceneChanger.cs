using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	public string sceneToLoad;
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.SendMessage ("OnExitLevel");
			UnityEngine.SceneManagement.SceneManager.LoadScene (sceneToLoad);
		}
	}
}
