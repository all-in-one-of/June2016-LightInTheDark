using UnityEngine;
using System.Collections;

public struct RangedValue
{
	[SerializeField]
	private Range _valueRange;
	private float _value;

	public Range ValueRange {
		get {
			return _valueRange;
		}
	}

	public float Value {
		get {
			return _value;
		}
		set {
			_value = Mathf.Clamp (value, _valueRange.min, _valueRange.max);
		}
	}

	public void setPercentTo(float percent) {
		_value = _valueRange.getPercentInRange (percent);
	}

	public void setRange(Vector2 range) {
		setRange (range.x, range.y);
	}
	public void setRange(float min, float max) {
		_valueRange.min = min;
		_valueRange.max = max;
		Value = _value; // Clamp the value in range if neccessary
	}
}

