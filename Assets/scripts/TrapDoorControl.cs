using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorControl : MonoBehaviour {
    private bool isTriggered;
	// Use this for initialization
	void Start () {
        isTriggered = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isTriggered){
            transform.Translate(0,-4,0);
        }
	}
}
