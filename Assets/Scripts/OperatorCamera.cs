using UnityEngine;
using System.Collections;

public class OperatorCamera : MonoBehaviour {

	const float MinRotZ = 160;
	const float MaxRotZ = 200;
	const float RotZRange = MaxRotZ - MinRotZ;

	float lastMouseX;

	// Use this for initialization
	void Start () {
		lastMouseX = Input.mousePosition.x / Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
		var newMouseX = Input.mousePosition.x / Screen.width;
		var mouseDeltaX = newMouseX - lastMouseX;

		if (Mathf.Abs(mouseDeltaX) > 0.01 && Input.GetMouseButton(0)) {
			var transform = gameObject.GetComponent<Transform>();
			var camRotation = transform.localEulerAngles;
			camRotation.y -= mouseDeltaX * RotZRange * 2.0f;
			camRotation.y = Mathf.Clamp(camRotation.y, MinRotZ, MaxRotZ);
			transform.localEulerAngles = camRotation;
		}

		lastMouseX = newMouseX;
	}
}
