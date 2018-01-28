using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
    public Transform player;
    // public Transform projectileSpawnLocation;
    private UnityEngine.AI.NavMeshAgent agent;
    
    // MoveSpeed and Max/Min distance to shoot emeny
   
   
    //A flag to indicate if this player owns any active projectiles
    // private bool isOwner;
    //A flag to indicate if this player's projectile should split into 3
    // private bool split;
    //The scale that the player's projectiles should be
    // private float scale;
    //References to projectiles given by the projectile pool
    // private GameObject[] projectile;
    //The player's infection information
    // private Infection infections;
    // public Transform target;

    public enum State 
    {
        PATROL,
        CHASE,
        Flee
    }

	public Transform[] wayPoints;
    public State state;
    private bool alive;     
    private int wayPointInd = 0;
    public float patrolSpeed = 10.0f;
    public float chaseSpeed = 15.0f;
    private const int MaxDist = 10;
    private const int MinDist = 5;



    
    
    void Start () 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // agent.destination = wayPoints[wayPointInd].position;
        
        state = BotMovement.State.PATROL;
        alive = true;
        agent.autoBraking = false;
        agent.speed = patrolSpeed;
        // StartCoroutine(FSM());
    }

    // IEnumerator FSM() 
    // {
    //     while (alive) {
    //         switch (state)
    //         {	
    //             case State.PATROL:
    //                 Patrol();
    //                 break;
    //             case State.CHASE:
    //                 Chase();
    //                 break;
    //             yield return null;

    //         }
    //     }
    // }

    void Patrol() {
        agent.speed = patrolSpeed;

        if (wayPoints.Length == 0) 
        {
        	return;
        }
    	agent.destination = wayPoints[wayPointInd].position;

        float distance = Vector3.Distance(this.transform.position, wayPoints[wayPointInd].position);

        if (distance >= 3) 
        {
            agent.SetDestination(wayPoints[wayPointInd].position);
        }
        else if (distance <= 3)
        {
            wayPointInd++;
            if (wayPointInd >= wayPoints.Length) 
            {
                wayPointInd = 0;
            }
        }
    }

    void Chase() {
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

        // float distance = Vector3.Distance(this.transform.position, target.transform.position);
        
        // if (distance <= MaxDist) 
        // {
        //     // Fire();
        //     Patrol();
        // }
    }

    void Update() {
        Patrol();
    	// if (!agent.pathPending && agent.remainingDistance < 0.5f)
     //       Patrol();
    }

    

    // void Update() 
    // {   
 
    //     float currDist = Vector3.Distance(agent.nextPosition, target.position);

    //     if (currDist <= MinDist && currDist <= MaxDist) {
    //          Fire();

    //     }
    //     else {
    //         agent.SetDestination(point);
    //     }
    // }

    // void Fire() 
    // {   
    //     if (infections.infectionNum > 0)
    //     {
    //         //Check that this player isn't the owner of an infection that's already been launched
    //         foreach (GameObject projectile in PoolManager.GetProjectilePool())
    //         {
    //             if (projectile.activeSelf && projectile.GetComponent<Projectile>().GetOwner() == gameObject)
    //             {
    //                 isOwner = true;
    //             }
    //         }

    //         //If the player doesn't own any projectiles in the scene, fire a projectile
    //         if (!isOwner)
    //         {
    //             //Get a projectile(s) from the pool
    //             if(split)
    //             {
    //                 projectile = new GameObject[3];
    //                 scale = 0.3f;
    //             }
    //             else	
    //             {
    //                 projectile = new GameObject[1];
    //                 scale = 1.0f;
    //             }


    //             for(int p = 0; p < projectile.Length; ++p)
    //             {
    //                 projectile[p] = PoolManager.GetProjectile();
    //                 //Set the projectile's position and forward vector
    //                 projectile[p].transform.position = projectileSpawnLocation.position;
    //                 projectile[p].transform.localScale = new Vector3(scale, scale, scale);

    //                 switch (p)
    //                 {
    //                     case 0:
    //                         projectile[p].transform.forward = transform.forward;
    //                         break;
    //                     case 1:
    //                         projectile[p].transform.rotation = Quaternion.Euler(0.0f, -45.0f, 0.0f) * transform.rotation;
    //                         break;
    //                     case 2:
    //                         projectile[p].transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f) * transform.rotation;
    //                         break;
    //                 }
                    

    //                 //Set the projectile's owner and enable it
    //                 projectile[p].GetComponent<Projectile>().SetOwner(gameObject);
    //                 projectile[p].SetActive(true);
    //             }
    //         }
    //     }
    // }
}
