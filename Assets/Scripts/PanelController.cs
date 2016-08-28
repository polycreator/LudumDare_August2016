using UnityEngine;
using System.Collections;

public class PanelController : MonoBehaviour {

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
				if (button.Press()) {
					
				}
			}
		}
	}
}
