using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    /*=================*/
    /*          MEMBERS             */
    /*=================*/
    /*              PUBLIC               */
    [Tooltip("The position and rotation (direction) that projectiles will spawn when shot by this player")]
    public Transform projectileSpawnLocation;

    /*             PRIVATE            */
    //A flag to indicate if this player owns any active projectiles
    private bool isOwner;
    //A reference to the player's camera
    private Camera playerCamera;
    //A reference to a projectile given by the projectile pool
    private GameObject projectile;
    //The player's infection information
    private Infection infections;
    //A 3D vector to store the mouse pointer's current screen position
    private Vector3 mousePosition;
    //A 3D vector to store the position that the player should face
    private Vector3 lookDirection;

    /*=================*/
    /*          METHODS            */
    /*=================*/
    /*              PUBLIC               */
    //A method to return the owner flag for external use
    public bool GetOwnerFlag()
    {
        return isOwner;
    }
    //A method to return a reference to the player's camera for external use
    public Camera GetPlayerCamera()
    {
        return playerCamera;
    }
    //A method to return the projectile spawning location for external use
    public Transform GetProjectileSpawnLocation()
    {
        return projectileSpawnLocation;
    }
    //A method to set the owner flag externally
    public void SetOwnerFlag(bool value)
    {
        isOwner = value;
    }
    //A method to reference the player's camera externally
    public void SetPlayerCamera(Camera camera)
    {
        playerCamera = camera;
    }

    /*             PRIVATE            */
    //A method to handle the shooting of projectiles based on player input
    void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && infections.infectionNum > 0)
        {
            //Check that this player isn't the owner of an infection that's already been launched
            foreach (GameObject projectile in PoolManager.GetProjectilePool())
            {
                if (projectile.activeSelf && projectile.GetComponent<Projectile>().GetOwner() == gameObject)
                {
                    isOwner = true;
                }
            }

            //If the player doesn't own any projectiles in the scene, fire a projectile
            if (!isOwner)
            {
                //Get a projectile from the pool
                projectile = PoolManager.GetProjectile();

                //Set the projectile's position and forward vector
                projectile.transform.position = projectileSpawnLocation.position;
                projectile.transform.forward = transform.forward;

                //Set the projectile's owner and enable it
                projectile.GetComponent<Projectile>().SetOwner(gameObject);
                projectile.SetActive(true);
            }
        }
    }
    //A method to handle rotating the player based on the mouse pointer position
    void Rotate()
    {
        lookDirection = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.y);
        transform.LookAt(lookDirection);
    }
	//Unity initialization method
	void Start ()
    {
        playerCamera = transform.parent.GetComponentInChildren<Camera>();
        infections = transform.parent.GetComponent<Infection>();

        mousePosition = Vector3.zero;
        lookDirection = Vector3.zero;
	}
	//Unity update method
	void Update ()
    {
        Rotate();
        Fire();
	}
}
