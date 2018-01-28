using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorControl : MonoBehaviour {
    // state of the trap door, true
    public bool trapState;

    public void SetTrapState(bool state)
    {
        trapState = state;
    }

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
        if (trapState)
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
	}

}
