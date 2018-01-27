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
    private float lifeTimer;
    //The player that shot this projectile
    private GameObject owner;
    //The rigidbody of this projectile
    private Rigidbody body;
    //The 3D velocity of this projectile
    private Vector3 velocity;

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
    //Returns a reference to the player that shot this projectile
    public GameObject GetOwner()
    {
        return owner;
    }
    //Returns a reference to the rigidbody of this projectile
    public Rigidbody GetRigidbody()
    {
        return body;
    }
    //Returns the current velocity of this projectile
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    //Sets the lifespan of the projectile to the given time (in seconds)
    public void SetLifespan(float time)
    {
        lifespan = time;
    }
    //Sets a reference to the player that shot this projectile
    public void SetOwner(GameObject player)
    {
        owner = player;
    }
    //Sets the speed of the projectile
    public void SetSpeed(float value)
    {
        speed = value;
    }

    /*             PRIVATE            */
    //Method to handle the destruction of a projectile
    void Destroy()
    {
        //Reset the lifespan timer
        lifeTimer = lifespan;

        //Dereference the player
        owner.GetComponent<Shoot>().SetOwnerFlag(false);
        owner = null;

        //Deactivate this projectile
        gameObject.SetActive(false);
    }
    //Method to move the projectile forward
    void Move()
    {
        body.velocity = velocity;
    }
    //Unity collision method
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[Projectile.cs] " + gameObject.name + " hit " + collision.gameObject.name);

        //Check the tag of the other object
        if(collision.gameObject.CompareTag("Player"))
        {
            //Check that the target isn't the owner of this projectile
            if(collision.gameObject != owner)
            {
                //This projectile has hit a player
                //Add an infection to the player
                Debug.Log("[Projectile.cs] " + collision.gameObject.name + " given infection from " + owner.name);
                collision.transform.parent.GetComponent<Infection>().incrementInfectionNumber();

                //Remove an infection from my owner
                owner.transform.parent.GetComponent<Infection>().decrementInfectionNumber();
            }

            //Destroy this projectile
            Destroy();
        }
        else if(collision.gameObject.CompareTag("Environment"))
        {
            //This projectile has hit a wall
            //Create an impact effect on the wall

            //Reflect the projectile
            ContactPoint contact = collision.contacts[0];
            float dotProduct = Vector3.Dot(contact.point, transform.forward);
            Vector3 reflectionVector = contact.normal * dotProduct;
            reflectionVector += transform.forward;
            velocity = reflectionVector.normalized * speed;
        }
    }
    //Unity component enable logic method
    void OnEnable()
    {
        ResetVelocity();
    }
    //Method to reset the velocity to the forward
    void ResetVelocity()
    {
        velocity = transform.forward * speed;
    }
    //Unity initialization method
    void Start ()
    {
        //Reference the rigidbody
        body = GetComponent<Rigidbody>();
        if(!body)
        {
            Debug.LogError("[Projectile.cs] No rigidbody attached to " + gameObject.name);
        }

        //Set private members
        lifeTimer = lifespan;
        ResetVelocity();
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
            Destroy();
        }
	}
}