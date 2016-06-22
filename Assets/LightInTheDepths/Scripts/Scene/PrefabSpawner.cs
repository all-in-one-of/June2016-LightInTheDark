using UnityEngine;
using System.Collections;

namespace LightInTheDark {
public class PrefabSpawner : MonoBehaviour {
	public GameObject prefabToSpawn;

	public Vector3 maxDistances;
	bool randomYRotation = true;
	public int minToSpawn = 1;
	public int maxToSpawn = 1;

	// Use this for initialization
	void Start () {
		int toSpawn = Random.Range (minToSpawn, maxToSpawn);

		for (int index = 0; index < toSpawn; index++) {
			Spawn ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn () { 
		Vector3 offset = new Vector3(getOffset(maxDistances.x),getOffset(maxDistances.y),getOffset(maxDistances.z));
		Quaternion rot = transform.rotation;
		if (randomYRotation) {
			rot = Quaternion.Euler (0, Random.Range (-360, 360), 0) * rot;
		}
		Instantiate(prefabToSpawn, transform.position + offset, rot);
	}

	float getOffset (float maxDiff) {
		float randomWithNeg = Random.value * 2 - 1;
		return randomWithNeg * maxDiff;
	}
}
}