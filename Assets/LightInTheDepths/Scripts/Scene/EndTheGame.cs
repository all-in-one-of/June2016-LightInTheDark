using UnityEngine;
using System.Collections;

namespace LightInTheDark {
public class EndTheGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			Application.Quit ();
			UnityEditor.EditorApplication.isPlaying = false;
		}
	}
}
}