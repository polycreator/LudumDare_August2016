using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {

    public AnimationClip activate;
    public AnimationClip reset;

    private Animation animationController;

    public bool trapIsSet = true;

	// Use this for initialization
	void Start () {
	
        animationController = gameObject.GetComponent<Animation>();

	}
	
	// Update is called once per frame
	void Update () {

        if (trapIsSet == false)
        {
            if (animationController.IsPlaying(""))
            {

            }

        }
	
	}

    public void ActivateTrap()
    {
        trapIsSet = false;
        animationController.Play();
    }
}
