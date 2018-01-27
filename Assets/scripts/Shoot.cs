using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera playerCamera;
    private Vector3 mousePosition;
    private Vector3 lookDirection;

    void Rotate()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.nearClipPlane));
        Debug.Log(mousePosition);


        lookDirection = new Vector3(mousePosition.x, transform.position.y, mousePosition.y);
        transform.LookAt(lookDirection);
    }
	// Use this for initialization
	void Start ()
    {
        playerCamera = GetComponentInChildren<Camera>();

        mousePosition = Vector3.zero;
        lookDirection = Vector3.zero;
	}
	// Update is called once per frame
	void Update ()
    {
        Rotate();
        
	}
}
