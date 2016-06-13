using UnityEngine;
using System.Collections;

public struct Range 
{
	public float min;
	public float max;

	public float getPercentInRange(float percent) {
		float normPercent = Mathf.Clamp (percent, 0.0f, 1.0f);
		float diff = max - min;
		return min + normPercent * diff;
	}

	public bool isInRange(float val) {
		return !(val < min) && !(val > max);
	}
}

