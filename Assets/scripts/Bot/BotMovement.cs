using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
    // public Transform player;
    public GameObject player;
    public Transform projectileSpawnLocation;
    private UnityEngine.AI.NavMeshAgent agent;
    
    // MoveSpeed and Max/Min distance to shoot emeny
   
   
    // A flag to indicate if this player owns any active projectiles
    private bool isOwner;
    // A flag to indicate if this player's projectile should split into 3
    private bool split;
    // The scale that the player's projectiles should be
    private float scale;
    // References to projectiles given by the projectile pool
    private GameObject[] projectile;
    // The player's infection information
    private Infection infections;
    // public Transform target;

	public Transform[] wayPoints;
    private bool alive;     
    private int wayPointInd = 0;
    private int prevPointInd = 0;
    public float patrolSpeed = 10.0f;
    public float chaseSpeed = 15.0f;
    private const int MaxDist = 8;
    private const int MinDist = 4;
     
    private float range = 4.0f;
    private int multiplier = 15;
    private int numInfection = 0;
    private bool didFire;





    void Awake() {
        player = GameObject.FindWithTag("Player");
    }


    
    
    void Start () 
    {
        // player = GameObject.FindWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // infections = transform.parent.GetComponent<Infection>();
        // agent.destination = wayPoints[wayPointInd].position;
        
        
        alive = true;
        didFire = false;
        agent.autoBraking = false;
        agent.speed = patrolSpeed;
    }


    void Patrol() {
        agent.speed = patrolSpeed;

        // Quaternion newRotation = Quaternion.LookRotation(position - myTransform.position);
        
        // agent.rotation = Quaternion.Slerp(agent.rotation, Quaternion.LookRotation(player.transform.position - agent.position), rotationSpeed * Time.deltaTime);

        if (wayPoints.Length == 0) 
        {
        	return;
        }
        
    	agent.destination = wayPoints[wayPointInd].position;

        float distance = Vector3.Distance(this.transform.position, wayPoints[wayPointInd].position);

        if (distance >= 2) 
        {
            agent.SetDestination(wayPoints[wayPointInd].position);
        }
        else if (distance < 2)
        {
        //     wayPointInd++;
        //     if (wayPointInd >= wayPoints.Length) 
        //     {
        //         wayPointInd = 0;
        //     }
            prevPointInd = wayPointInd;
            wayPointInd = Random.Range(0, 8);
            while (prevPointInd == wayPointInd) 
            {
                wayPointInd = Random.Range(0, 8);
            }
        }
            
    }

    void Chase() {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.transform.position);
        // didFire = true;

        // float distance = Vector3.Distance(this.transform.position, target.transform.position);
        
        // if (distance <= MaxDist) 
        // {
        //     // Fire();
        //     Patrol();
        // }
    }

    void Flee() {
        // Vector3 runTo = this.transform.position + ((this.transform.position - player.transform.position) * multiplier);
        Vector3 farPoint = this.transform.position;
        float prevDist = 1000000.0f;
        foreach (Transform point in wayPoints) 
        {
            float currDist = Vector3.Distance(this.transform.position, point.transform.position);
            if (prevDist < currDist) 
            {
                prevDist = currDist;
                farPoint = point.transform.position;
            }
                
        }
        agent.SetDestination(farPoint);
    }


    // if alive
    // distance < range
    // 1. flee
    // 2. chase
    // else
    // 3 patrol  

    void Update() 
    {   
        // Fire();
        float currDist = Vector3.Distance(this.transform.position, player.transform.position);
        

        if (alive) 
        {
            if (currDist > range) // && !isCombat) 
            {
                Patrol();
            }
            else  {
                // isCombat = true;
                if (numInfection == 0)
                    Flee();
                else 
                    Chase();
            }
        } 
        // else {
            // Destroy(agent);
        // } 
        
    }

    void Fire() 
    {   

        if (infections.infectionNum > 0)
        {
            //Check that this player isn't the owner of an infection that's already been launched
            // foreach (GameObject projectile in PoolManager.GetProjectilePool())
            // {
            //     if (projectile.activeSelf && projectile.GetComponent<Projectile>().GetOwner() == gameObject)
            //     {
            //         isOwner = true;
            //     }
            // }

            //If the player doesn't own any projectiles in the scene, fire a projectile
            // if (!isOwner)
            // {
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
                }
            }
        // }
    }
}
