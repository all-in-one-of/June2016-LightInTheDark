using UnityEngine;
using System.Collections;

namespace LightInTheDark {
public class PrefabSpawner : MonoBehaviour {
	public GameObject prefabToSpawn;

	public Vector3 maxDistances;
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

		Instantiate(prefabToSpawn, transform.position + offset, transform.rotation);
	}

	float getOffset (float maxDiff) {
		float randomWithNeg = Random.value * 2 - 1;
		return randomWithNeg * maxDiff;
	}
}
}