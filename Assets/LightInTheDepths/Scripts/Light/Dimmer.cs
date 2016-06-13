using UnityEngine;
using System.Collections;

namespace LightInTheDark {


public class Dimmer : MonoBehaviour {
	public enum DimmerType {
		Distance,
		Intensity
	}

	public DimmerType type;
	public float cyclePeriod = 1.0f;

	private LightController _lightController;
	private bool _increasing;
	private float _percentInPeriod;

	// Use this for initialization
	void Start () {
		_lightController = GetComponent<LightController> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePercentInPeriod ();
		UpdateLight ();
	}

	void UpdateLight() {
		if (type == DimmerType.Distance) {
			_lightController.SetDistanceToPercent (_percentInPeriod);
			Debug.Log ("Setting dist to " + _percentInPeriod);
		} else {
			_lightController.SetIntensityToPercent (_percentInPeriod);
		}
	}

	void UpdatePercentInPeriod() {
		_percentInPeriod += Time.deltaTime / cyclePeriod * ((_increasing) ? 1 : -1);
		_percentInPeriod = Mathf.Clamp (_percentInPeriod, 0.0f, 1.0f);

		if (_percentInPeriod >= 1.0f) {
			_increasing = false;
		}
		if (_percentInPeriod <= 0.0f) {
			_increasing = true;
		}
	}
}

}