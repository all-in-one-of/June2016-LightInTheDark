using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public interface Punchable {
	void OnPunched(Vector3 punchSource);
}
}