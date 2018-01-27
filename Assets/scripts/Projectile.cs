using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*=================*/
    /*          MEMBERS             */
    /*=================*/
    /*              PUBLIC               */
    [Tooltip("How long the projectile will be on the screen (in seconds)")]
    public float lifespan;
    [Tooltip("How fast the projectile will move")]
    public float speed;
    
    /*             PRIVATE            */
    //Timer for counting how many seconds before this projectile is destroyed
    float lifeTimer;
    //The 3D velocity of this projectile
    Vector3 velocity;

    /*=================*/
    /*          METHODS            */
    /*=================*/
    /*              PUBLIC               */
    //Returns the lifespan (in seconds) of this projectile
    public float GetLifespan()
    {
        return lifespan;
    }
    //Returns the timer (in seconds) of this projectile
    public float GetLifeTimer()
    {
        return lifeTimer;
    }
    //Returns the speed of this projectile
    public float GetSpeed()
    {
        return speed;
    }
    //Sets the lifespan of the projectile to the given time (in seconds)
    public void SetLifespan(float time)
    {
        lifespan = time;
    }
    //Sets the speed of the projectile
    public void SetSpeed(float value)
    {
        speed = value;
    }

    /*             PRIVATE            */
    //Method to move the projectile forward
    void Move()
    {
        velocity = Vector3.forward * speed;
        transform.position += velocity;
    }
    //Unity collision method
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[Projectile.cs] " + gameObject.name + " hit " + collision.gameObject.name);

        //Check the tag of the other object
        if(collision.gameObject.CompareTag("Player"))
        {
            //This projectile has hit a player
        }
        else if(collision.gameObject.CompareTag("Wall"))
        {
            //This projectile has hit a wall
        }
    }
    //Unity initialization method
    void Start ()
    {
        lifeTimer = lifespan;
        velocity = Vector3.zero;
	}
	//Unity update method
	void Update ()
    {
        //Move the projectile
        Move();

        //Run the timer
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0.0f)
        {
            //If the timer is out, kill this projectile
            DestroyImmediate(gameObject);
        }
	}
}