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
    //A flag to indicate if this player's projectile should split into 3
    private bool split;
    //A reference to the player's camera
    private Camera playerCamera;
    //The scale that the player's projectiles should be
    private float scale;
    //References to projectiles given by the projectile pool
    private GameObject[] projectile;
    //The player's infection information
    private Infection infections;
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
    //A method to return the split flag for external use
    public bool GetSplitFlag()
    {
        return split;
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
    //A method to set the scale of the player's projectiles externally
    public void SetProjectileScale(float value)
    {
        scale = value;
    }
    //A method to set the split flag externally
    public void SetSplitFlag(bool value)
    {
        split = value;
    }

    /*             PRIVATE            */
    //A method to handle the shooting of projectiles based on player input
    void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && infections.infectionNum > 0)
        {
            //Check that this player isn't the owner of an infection that's already been launched
            foreach (GameObject spit in PoolManager.GetProjectilePool())
            {
                if (spit.activeSelf && spit.GetComponent<Projectile>().GetOwner() == gameObject)
                {
                    isOwner = true;
                }
            }

            //If the player doesn't own any projectiles in the scene, fire a projectile
            if (!isOwner)
            {
                //Get a projectile(s) from the pool
                if(split)
                {
                    projectile = new GameObject[3];
                    scale = 0.3f;
                }
                else
                {
                    projectile = new GameObject[1];
                    scale = 1.0f;
                }


                for(int p = 0; p < projectile.Length; ++p)
                {
                    projectile[p] = PoolManager.GetProjectile();

                    if(projectile[p])
                    {
                        //Set the projectile's position and forward vector
                        projectile[p].transform.position = projectileSpawnLocation.position;
                        projectile[p].transform.localScale = new Vector3(scale, scale, scale);

                        switch (p)
                        {
                            case 0:
                                projectile[p].transform.forward = transform.forward;
                                break;
                            case 1:
                                projectile[p].transform.rotation = Quaternion.Euler(0.0f, -45.0f, 0.0f) * transform.rotation;
                                break;
                            case 2:
                                projectile[p].transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f) * transform.rotation;
                                break;
                        }


                        //Set the projectile's owner and enable it
                        projectile[p].GetComponent<Projectile>().SetOwner(gameObject);
                        projectile[p].SetActive(true);
                        Debug.Log(projectile[p].name + " owner: " + gameObject.name);
                    }
                    
                }
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
        //Get reference to this player's camera and infection information
        playerCamera = transform.parent.GetComponentInChildren<Camera>();
        infections = transform.parent.GetComponent<Infection>();

        //Initialize flags
        split = false;

        //Initialize values
        scale = 1.0f;
        lookDirection = Vector3.zero;

        //Initialize arrays
        projectile = new GameObject[1];
	}
	//Unity update method
	void Update ()
    {
        Rotate();
        Fire();
	}
}
