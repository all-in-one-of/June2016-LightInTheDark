using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour {
	public string sceneToLoad;
	public bool doIntro = true;
	public bool doOutro = true;
	public float introTime = 1.0f;
	public float outroTime = 1.0f;

	void Start() {

		if (doIntro) {
			StartCoroutine("StartIntro", GameObject.FindGameObjectWithTag("Player"));
		}
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			if (doOutro) {
				StartCoroutine ("StartTransition", other.gameObject);
			} else {
				other.SendMessage ("OnExitLevel");
				SceneManager.LoadScene (sceneToLoad);
			}
		}
	}

	IEnumerator StartIntro(GameObject player) {
		if (player == null) {
			yield break;
		}
		player.SendMessage ("OnStartWalkForward");
		yield return new WaitForSeconds(introTime);
		player.SendMessage ("OnStopWalkForward");
	}

	IEnumerator StartTransition(GameObject player) {
		if (player == null) {
			SceneManager.LoadScene (sceneToLoad);
			yield break;
		}
		player.SendMessage ("OnStartWalkForward");
		Camera cam = GameObject.FindObjectOfType<Camera> ();
		OrbitCamera orbit = cam.GetComponent<OrbitCamera> ();
		Transform toLookAt = player.transform;
		if (orbit != null) {
			toLookAt = orbit.target;
			Destroy (orbit);
		}
		LookAt look = cam.gameObject.AddComponent<LookAt> ();
		look.toLookAt = toLookAt;
		yield return new WaitForSeconds(outroTime);
		player.SendMessage ("OnExitLevel");
		SceneManager.LoadScene (sceneToLoad);
	}
}
