using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

	public float rotSpeed = 4f;
	public float lookSpeed = 0.5f;

	public float offsetMin = 6;
	public float offsetMax = 8;
	public float speedScale = 5;
	private float _rotY;
	private float _rotationOffsetY;
	private Vector3 _positionOffset;

    void Start() {
        _rotY = transform.eulerAngles.y;
        _positionOffset = target.position - transform.position;
		_rotationOffsetY = Mathf.Clamp (_rotationOffsetY, offsetMin, offsetMax);
    }

    void FixedUpdate() {
        if (Input.GetAxis("Mouse X") != 0){
            _rotY += Input.GetAxis("Mouse X") * rotSpeed;
        }

		if (Input.GetAxis("Mouse Y") != 0){
			_rotationOffsetY += Input.GetAxis("Mouse Y") * lookSpeed;
			_rotationOffsetY = Mathf.Clamp (_rotationOffsetY, offsetMin, offsetMax);
		}

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _positionOffset);

		Quaternion targetRot = Quaternion.LookRotation ((target.position - transform.position) + _rotationOffsetY * Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, speedScale*Time.deltaTime);
    }
}
