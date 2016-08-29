using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.LoadLevelAdditive("level01");
        Application.LoadLevelAdditive("control_room");
        Application.LoadLevelAdditive("ControlData");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
