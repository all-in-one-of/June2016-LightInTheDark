using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

	public Vector3 posOffset = new Vector3(0,3,-7);
	public float rotSpeed = 4f;
	public float lookSpeed = 0.5f;
	public float initialRotation = 2.0f;

	public float offsetMin = 6;
	public float offsetMax = 8;
	public float speedScale = 5;
	private float _rotY;
	private float _rotationOffsetY;

    void Start() {
		_rotationOffsetY = initialRotation;
		_rotationOffsetY = Mathf.Clamp (_rotationOffsetY, offsetMin, offsetMax);

		transform.position = target.position - target.transform.TransformDirection(posOffset);

		transform.LookAt (target.position - transform.position + _rotationOffsetY * Vector3.up);
    }

    void FixedUpdate() {
        if (Input.GetAxis("Mouse X") != 0){
            _rotY += Input.GetAxis("Mouse X") * rotSpeed;
        }

		if (Input.GetAxis("Mouse Y") != 0){
			_rotationOffsetY -= Input.GetAxis("Mouse Y") * lookSpeed;
			_rotationOffsetY = Mathf.Clamp (_rotationOffsetY, offsetMin, offsetMax);
		}

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
		transform.position = target.position - (rotation * target.transform.TransformVector(posOffset)) + _rotationOffsetY * Vector3.up;

		Quaternion targetRot = Quaternion.LookRotation (target.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, speedScale*Time.deltaTime);

		transform.LookAt (target.position);
    }
}
