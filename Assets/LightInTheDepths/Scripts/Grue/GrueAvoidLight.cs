﻿using UnityEngine;
using System.Collections;

namespace LightInTheDark {

[RequireComponent(typeof(GrueState))]
[RequireComponent(typeof(GrueMovement))]
public class GrueAvoidLight : MonoBehaviour {
	
	public MovementPriority priority = MovementPriority.MAX;
	public float desiredDistance = 2.0f;
	public float grayArea = 3.0f;
	public float fleeSpeedPercent = 0.5f;

	private GrueState _state;
	private GrueMovement _movementController;
	private Light[] _sceneLights;

	// Use this for initialization
	void Start () {
		_state = GetComponent<GrueState> ();
		_movementController = GetComponent<GrueMovement> ();
		_sceneLights = Resources.FindObjectsOfTypeAll<Light> ();
	}

	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;

		foreach(Light light in _sceneLights) {
			updateMovementFromLight(ref movement, light);
		}

		if(movement == Vector3.zero) {
			return;
		}

		_movementController.AddMovement (movement, priority);
	}

	void updateMovementFromLight (ref Vector3 movement, Light light) {
		if (!light.isActiveAndEnabled) {
			return;
		}
		float avoidDistance = light.range * 0.5f + desiredDistance;
		float fleeDistance = avoidDistance + grayArea;

		Vector3 fromLight = transform.position - light.transform.position;
		float distSq = fromLight.sqrMagnitude;
		if (distSq > avoidDistance*avoidDistance) {
			return; // Outside avoid range
		}

		bool fleeing = distSq > fleeDistance*fleeDistance;
		float speedPercent = fleeing ? fleeSpeedPercent : 1.0f;
		fromLight.y = 0;
		fromLight.Normalize ();
		fromLight *= speedPercent * _state.maxSpeed;

		movement += fromLight;
	}
}
}