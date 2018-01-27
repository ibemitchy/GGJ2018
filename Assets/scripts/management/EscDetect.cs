using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscDetect : MonoBehaviour {

    private GameObject panel;

	// Use this for initialization
	void Start () {
        panel = GetComponent<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)){
            panel.gameObject.SetActive(true);
        }
	}
}
