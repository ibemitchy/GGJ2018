﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementControl : MonoBehaviour {
    public float speed;
    private bool inverseFlag;
    private Rigidbody body;


	// Use this for initialization
    void Start () {
        inverseFlag = false;
        body = GetComponentInChildren<Rigidbody>();
        if (!body){
            Debug.LogError("no body");
        }
    }

    void Update () {

        float horizontalMovement;
        float verticalMovement;
        if (!inverseFlag)
        {
            horizontalMovement = Input.GetAxis("Horizontal") * speed;
            verticalMovement = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            horizontalMovement = -Input.GetAxis("Horizontal") * speed;
            verticalMovement = -Input.GetAxis("Vertical") * speed;
        }
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement);
        body.velocity = movement;
	}

    //give a new speed for player movement.
    //used for speed up boost 
    public void SetSpeed(float newSpeed){
        speed = newSpeed;
    }

    //get speed for player movement.
    public float GetSpeed(){
        return speed;
    }

    //increment speed by incSpeedVal amount
    public void IncSpeed (float incSpeedVal){
        speed = speed + incSpeedVal;
    }

    //inverse inverseFlag for powerup
    public void InvertMovement(){
        inverseFlag = !inverseFlag;
    }

}
