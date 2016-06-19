using UnityEngine;
using System.Collections;

namespace LightInTheDark {
//TODO: Size, Speed, aggression and particle differences based on one random number for predictability
public class GrueRandomizer : MonoBehaviour {
	public float smallCap = 0.2f;
	public float mediumCap = 0.8f;
	// Use this for initialization
	void Start () {
		float typeVal = Random.value;
		if (typeVal < smallCap) {
			transform.localScale = transform.localScale * 0.75f;
		} else if (typeVal > mediumCap) {
			transform.localScale = transform.localScale * 1.25f;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float getPercentOfValueInRange (float baseScale, float min, float max)	{
		return min + (max - min) * Random.value * baseScale;
	}
}
}