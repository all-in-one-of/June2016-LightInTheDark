using UnityEngine;
using System.Collections;

namespace LightInTheDark {
[RequireComponent(typeof(TenticleState))]
public class TenticleWarning : MonoBehaviour {
	public Vector3 safeColor = new Vector3(139/255.0f,103/255.0f,103/255.0f);
	public Vector3 dangerColor = new Vector3(200/255.0f,0/255.0f,0/255.0f);

	private TenticleState _state;
	private ParticleSystem _particles;
	private float _emissionOrig;
	// Use this for initialization
	void Start () {
		_state = GetComponent<TenticleState> ();
		_particles = GetComponentInChildren<ParticleSystem> ();
		_emissionOrig = _particles.emission.rate.constantMax;
		_particles.startLifetime = _state.attackDuration;
	}
	
	// Update is called once per frame
	void Update () {
		if (_state.attacking) {
			if (_particles.emission.rate.constantMax != 0) {
				var emission = _particles.emission;
				var rate = emission.rate;
				rate.constantMax = 0;
				emission.rate = rate;
			}

		} else {
			if (_particles.emission.rate.constantMax == 0) {
				var emission = _particles.emission;
				var rate = emission.rate;
				rate.constantMax = _emissionOrig;
				emission.rate = rate;
			}

			UpdateColor ();
		}

	}

	void UpdateColor ()	{
		Vector3 currColor = Vector3.Lerp(safeColor, dangerColor, _state.PercentToAttack);
		_particles.startColor = new Color (currColor.x, currColor.y, currColor.z);
	}
}
}