using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        //button down trap door open/close
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Feft");
        //button up
    }
}
