using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartToContinue : MonoBehaviour {
	public UnityEngine.UI.Text text;	
	private AsyncOperation _loadOp;
	// Use this for initialization
	void Start () {
		_loadOp = SceneManager.LoadSceneAsync (1);
		_loadOp.allowSceneActivation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Accept")) {
			Debug.Log ("Enter pressed");
			text.text = "Loading...";
			text.SetAllDirty ();
			_loadOp.allowSceneActivation = true;			
		}
	}
}
