using UnityEngine;
using System.Collections;

namespace LightInTheDark {

public class LightController : MonoBehaviour {
	[SerializeField]
	public Material _unlitMaterial;
	[SerializeField]
	private Vector2 _intensityRange;
	[SerializeField]
	private Vector2 _lightDistanceRange;

	[SerializeField]
	private Light _light;
	private RangedValue _lightIntensity;
	private RangedValue _lightDistance;
	private Material _origMaterial;
	private Color _emissionColor;
	private Renderer _renderer;
	private Vector3 _origScale;

	public bool IsLightEnabled {
		get {
			return _light.enabled;
		}
		set {
			_light.enabled = value;
			updateEmission ();
		}
	}

	void Start () {
		_lightIntensity.setRange (_intensityRange);
		_lightIntensity.Value = _light.intensity;

		_lightDistance.setRange (_lightDistanceRange);
		_lightDistance.Value = _light.range;

		_renderer = GetComponentInChildren<MeshRenderer> ();

		if (_renderer != null) {
			_origMaterial = _renderer.material;
		}

		_origScale = transform.localScale;

		updateEmission ();

	}

	void Update() {
		updateEmission ();
	}

	public void updateEmission() {
		if (_unlitMaterial == null || _renderer == null) {
			return;
		}

		_renderer.material = _light.enabled ? _origMaterial : _unlitMaterial;
	}

	public float SetIntensityToPercent(float percent) {
		_lightIntensity.setPercentTo(percent);
		_light.intensity = _lightIntensity.Value;
		return _light.intensity;
	}
	public float SetDistanceToPercent(float percent) {
		_lightDistance.setPercentTo(percent);

		transform.localScale = _origScale * (1.25f - (1 - percent)/2);

		_light.range = _lightDistance.Value;
		return _light.range;
	}
}

}