using UnityEngine;
using System.Collections;

public class Cameras_Controller : MonoBehaviour {

    public GameObject[] cameras;

    public int currentCamera = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire2"))
        {
            cameras[currentCamera].GetComponent<CameraTrap_Controller>().ActivateTraps();

        }
    }
}