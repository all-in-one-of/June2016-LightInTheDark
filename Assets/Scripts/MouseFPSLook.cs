using UnityEngine;
using System.Collections;

public class MouseFPSLook : MonoBehaviour {
	public enum RotationAxes{
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityHor = 9.0f;
	public float sensitivityVert = 9.0f;

	public float maxAngle = 45.0f;

	private float _rotationX = 0;

	void FixedUpdate () {
		if (axes == RotationAxes.MouseX) {
			transform.Rotate (0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
		} 
		else if (axes == RotationAxes.MouseY) {
			_rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, maxAngle * -1, maxAngle);

			float rotationY = transform.localEulerAngles.y;

			transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

		} 
		else {
			_rotationX -= Input.GetAxis ("Mouse Y") * sensitivityVert;
			_rotationX = Mathf.Clamp(_rotationX, maxAngle * -1, maxAngle);

			float delta = Input.GetAxis ("Mouse X") * sensitivityHor;
			float rotationY = transform.localEulerAngles.y + delta;

			transform.localEulerAngles = new Vector3(_rotationX, rotationY,0);

		}
	}
}
