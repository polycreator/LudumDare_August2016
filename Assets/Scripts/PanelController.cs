using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour {

	private string cameraIndex_ = "4";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Input.GetMouseButtonDown(0)) {
			if (Physics.Raycast(ray, out hit)) {
				var button = hit.collider.gameObject.GetComponent<PanelButton>();
				if (button && button.Press()) {
					// camera switchers
					if (button.name.StartsWith("CamButton")) {
						cameraIndex_ = button.name.Substring("CamButton".Length);
						var allCams = GameObject.FindGameObjectsWithTag("Security Camera");
						foreach (var cam in allCams) {
							cam.GetComponent<Camera>().enabled = cam.name == "camera_0" + cameraIndex_;
						}
					}
					else if (button.name.StartsWith("TrapButton")) {
						// trap activators
						var curCam = GameObject.Find("camera_0" + cameraIndex_);
						if (curCam) {
							var trapCtl = curCam.GetComponent<CameraTrap_Controller>();
							if (trapCtl) {
								var trapIx = int.Parse(button.name.Substring("TrapButton".Length));
								if (trapCtl.trapController.Length > trapIx) {
									trapCtl.trapController[trapIx].ActivateTrap();
								}
							}
						}
					}
				}
			}
		}
	}
}
