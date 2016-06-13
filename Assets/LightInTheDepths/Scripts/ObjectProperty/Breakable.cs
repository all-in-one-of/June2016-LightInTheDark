using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class Breakable : MonoBehaviour {
	public 	AudioSource _breakSource;

	public void Break() {
		_breakSource.Play ();
		Destroy (gameObject, _breakSource.clip.length);
	}
}

}