using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour {
    public float speed;

	// Use this for initialization
    void Start () {
        
    }
    void Update()
    {
        
    }

    void FixedUpdate () {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        float verticalMovement = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(horizontalMovement, 0, verticalMovement);
        transform.Translate(movement);
	}

    //give a new speed for player movement.
    //used for speed up boost 
    public void SetSpeed(int newSpeed){
        speed = newSpeed;
    }
    public float GetSpeed(){
        return speed;
    }
}
