using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartToContinue : MonoBehaviour {
	private static bool firstLoad = true;
	private AsyncOperation _loadOp;
	private AsyncOperation _specialLoadOp;
	// Use this for initialization
	void Start () {
		_loadOp = SceneManager.LoadSceneAsync (1);
		_loadOp.allowSceneActivation = firstLoad;

		firstLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			_loadOp.allowSceneActivation = true;			
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			_loadOp.allowSceneActivation = true;
			_specialLoadOp = SceneManager.LoadSceneAsync (6);
			_specialLoadOp.allowSceneActivation = true;
		}
	}
}
