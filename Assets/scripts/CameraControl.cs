using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    private Transform target;
	// Use this for initialization
	void Start () {
        target = transform.parent.GetComponentInChildren<Rigidbody>().transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
	}
}
