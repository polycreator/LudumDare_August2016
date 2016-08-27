using UnityEngine;
using System.Collections;

public class CameraTrap_Controller : MonoBehaviour {

    public TrapController[] trapController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateTraps()
    {
        trapController[0].GetComponent<TrapController>().ActivateTrap();
    }

}