using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {
    [SerializeField] private Transform target;

	public float rotSpeed = 4f;
	public float lookSpeed = 0.5f;

	private float _rotY;
	private float _offsetY;
	private Vector3 _offset;
	private static float OFFSET_MAX = 45;
	private static float OFFSET_MIN = -OFFSET_MAX;


    void Start() {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    void LateUpdate() {
        if (Input.GetAxis("Mouse X") != 0){
            _rotY += Input.GetAxis("Mouse X") * rotSpeed;
        }

		if (Input.GetAxis("Mouse Y") != 0){
			_offsetY += Input.GetAxis("Mouse Y") * lookSpeed;
			_offsetY = Mathf.Min (_offsetY, OFFSET_MAX);
			_offsetY = Mathf.Max (_offsetY, OFFSET_MIN);
		}

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);

		transform.LookAt(target.position + _offsetY*Vector3.up);
    }
}
