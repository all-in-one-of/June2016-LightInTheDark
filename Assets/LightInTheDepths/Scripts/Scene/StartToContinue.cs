using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartToContinue : MonoBehaviour {
	private AsyncOperation _loadOp;
	// Use this for initialization
	void Start () {
		_loadOp = SceneManager.LoadSceneAsync (1);
		_loadOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			_loadOp.allowSceneActivation = true;			
		}
	}
}
