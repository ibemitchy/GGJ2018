using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour {
    public float speed;
    private bool inverseFlag;


	// Use this for initialization
    void Start () {
        inverseFlag = false;
    }
    void Update()
    {
        
    }

    void FixedUpdate () {
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
        transform.Translate(movement);
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
