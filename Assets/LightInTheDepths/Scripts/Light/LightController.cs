using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class LightController : MonoBehaviour {
	[SerializeField]
	private Vector2 _intensityRange;
	[SerializeField]
	private Vector2 _lightDistanceRange;

	[SerializeField]
	private Light _light;
	private RangedValue _lightIntensity;
	private RangedValue _lightDistance;

	void Start () {
		_lightIntensity.setRange (_intensityRange);
		_lightIntensity.Value = _light.intensity;

		_lightDistance.setRange (_lightDistanceRange);
		_lightDistance.Value = _light.range;
	}


	public float SetIntensityToPercent(float percent) {
		_lightIntensity.setPercentTo(percent);
		_light.intensity = _lightIntensity.Value;
		return _light.intensity;
	}
	public float SetDistanceToPercent(float percent) {
		_lightDistance.setPercentTo(percent);

		_light.range = _lightDistance.Value;
		return _light.range;
	}

	public void SetLightOn(bool enabled) {
		_light.enabled = enabled;
	}

}

}