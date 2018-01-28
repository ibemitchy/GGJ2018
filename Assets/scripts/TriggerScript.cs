using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {
    public GameObject trapdoor;
    public bool triggerFlag;

    private void OnTriggerEnter(Collider other)
    {
        triggerFlag = true;
        if (triggerFlag)
        {
            Debug.Log("helllllloooooooooo");
        }
        trapdoor.GetComponent<TrapDoorControl>().SetTrapState(triggerFlag);
    }
}
