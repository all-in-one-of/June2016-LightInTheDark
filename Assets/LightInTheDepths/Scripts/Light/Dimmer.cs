using UnityEngine;
using System.Collections;

namespace LightInTheDark {


public class Dimmer : MonoBehaviour {
	public enum DimmerType {
		Distance,
		Intensity
	}

	public float percentInPeriod;
	public bool useTrigSmoothing;
	public DimmerType type;
	public float cyclePeriod = 1.0f;

	private LightController _lightController;
	private bool _increasing;
	private float _percentDimmed;

	// Use this for initialization
	void Start () {
		_lightController = GetComponent<LightController> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePercentInPeriod ();
		UpdatePercentDimmed ();
		ClampPercent ();
		UpdateLight ();
	}

	void UpdateLight() {
		if (type == DimmerType.Distance) {
			_lightController.SetDistanceToPercent (_percentDimmed);
		} else {
			_lightController.SetIntensityToPercent (_percentDimmed);
		}
	}

	void UpdatePercentDimmed() {
		if (useTrigSmoothing) {
			float trigInput = percentInPeriod * Mathf.PI;
			_percentDimmed = Mathf.Abs(Mathf.Sin (trigInput));
		} else {
			_percentDimmed = percentInPeriod;
		}
	}

	void UpdatePercentInPeriod() {
		float toAdd = Time.deltaTime / cyclePeriod;
		toAdd *= (_increasing) ? 1 : -1;
		percentInPeriod += toAdd;

	}

	void ClampPercent() {
		percentInPeriod = Mathf.Clamp (percentInPeriod, 0.0f, 1.0f);

		if (percentInPeriod >= 1.0f) {
			_increasing = false;
		}
		if (percentInPeriod <= 0.0f) {
			_increasing = true;
		}
	}
}

}