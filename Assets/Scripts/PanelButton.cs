using UnityEngine;
using System.Collections;

enum ButtonState {
	Idle,
	Disabled,
	Pressing
}

public class PanelButton : MonoBehaviour {

	private ButtonState state_ = ButtonState.Idle;
	private bool enabled_ = true;
	private float startTime_ = 0;
	private Vector3 originalPos_ ;

	const float PressTime = 1.0f;
	const float Depression = 0.015f;

	void Start() {
		originalPos_ = gameObject.transform.localPosition;
	}

	float ButtonPosition(float t) {
		return Mathf.Sin(Mathf.PI * t);
	}
	
	void Update() {
		if (state_ == ButtonState.Pressing) {
			var timeRatio = Mathf.Clamp01((Time.time - startTime_) / PressTime);
			var depress = ButtonPosition(timeRatio);
			var newPos = originalPos_;
			newPos -= gameObject.transform.rotation * new Vector3(0, depress * Depression, 0);
			gameObject.transform.localPosition = newPos;

			if (timeRatio == 1.0) {
				state_ = enabled_ ? ButtonState.Idle : ButtonState.Disabled;
			}
		}
	}

	public bool Press() {
		if (state_ != ButtonState.Idle) {
			return false;
		}

		state_ = ButtonState.Pressing;
		startTime_ = Time.time;
		return true;
	}

	public void SetEnabled(bool newEnabled) {
		enabled_ = newEnabled;
		if (state_ != ButtonState.Pressing) {
			state_ = enabled_ ? ButtonState.Idle : ButtonState.Disabled;
		}
	}
}
