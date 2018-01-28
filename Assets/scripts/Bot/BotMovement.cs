using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
    // public Transform target;
    public GameObject target;
    public Transform projectileSpawnLocation;
    private UnityEngine.AI.NavMeshAgent agent;
    
    // MoveSpeed and Max/Min distance to shoot emeny
   
   
    // A flag to indicate if this target owns any active projectiles
    private bool isOwner;
    // A flag to indicate if this target's projectile should split into 3
    private bool split;
    // The scale that the target's projectiles should be
    private float scale;
    // References to projectiles given by the projectile pool
    private GameObject[] projectile;
    // The target's infection information
    private Infection infections;
    

	public GameObject[] wayPoints;
    private bool alive;     
    private int wayPointInd = 0;
    private int prevPointInd = 0;
    public float patrolSpeed = 10.0f;
    public float chaseSpeed = 15.0f;
     
    public float sightRange = 5.0f;
    public float attackRange = 1.0f;
    private int multiplier = 15;
    private bool didFire;
    public float speed;
    

    public void SetOwnerFlag(bool isFlag) {
        isOwner = isFlag;
    }

    public bool GetOwnerFlag() {
        return isOwner;
    }


    
    
    void Start () 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        infections = transform.GetComponent<Infection>();
        
        
        alive = true;
        didFire = false;
        agent.autoBraking = false;
        agent.speed = patrolSpeed;
    }


    void Patrol() {
        Debug.Log("Patrolling");
        agent.speed = patrolSpeed;

        // Quaternion newRotation = Quaternion.LookRotation(position - myTransform.position);
        
        // agent.rotation = Quaternion.Slerp(agent.rotation, Quaternion.LookRotation(target.transform.position - agent.position), rotationSpeed * Time.deltaTime);

        if (wayPoints.Length == 0) 
        {
        	return;
        }
        
    	agent.destination = wayPoints[wayPointInd].transform.position;

        float distance = Vector3.Distance(this.transform.position, wayPoints[wayPointInd].transform.position);

        if (distance >= 2) 
        {
            agent.SetDestination(wayPoints[wayPointInd].transform.position);
        }
        else if (distance < 2)
        {
            prevPointInd = wayPointInd;
            wayPointInd = Random.Range(0, 8);
            while (prevPointInd == wayPointInd) 
            {
                wayPointInd = Random.Range(0, 8);
            }
        }
            
    }

    void Chase() {
        transform.LookAt(target.transform);

        agent.speed = chaseSpeed;
        Debug.Log("chasing target");
        float currDist = Vector3.Distance(transform.position, target.transform.position);
        if (currDist <= attackRange) {
            agent.SetDestination(transform.position);
            Fire();
        }
        else
        {
            agent.SetDestination(target.transform.position);
        }
        
  
    }

    void Flee() {
        Debug.Log("Running away");
        //Vector3 targetDir = target.transform.position - transform.position;
        //float step = speed * Time.deltaTime;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward*-1, targetDir, step, 0.0F);
        //Debug.Log("running from target");
        //transform.rotation = Quaternion.LookRotation(newDir);

        //Go to random waypoint
        float distance = Vector3.Distance(this.transform.position, wayPoints[wayPointInd].transform.position);

        if (distance >= 2)
        {
            agent.SetDestination(wayPoints[wayPointInd].transform.position);
        }
        else if (distance < 2)
        {
            prevPointInd = wayPointInd;
            wayPointInd = Random.Range(0, 8);
            while (prevPointInd == wayPointInd)
            {
                wayPointInd = Random.Range(0, 8);
            }
        }
        transform.LookAt(wayPoints[wayPointInd].transform);
        agent.SetDestination(wayPoints[wayPointInd].transform.position);
    }

    void UpdateTarget() {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, sightRange, transform.forward, out hitInfo)) 
        {
            if (hitInfo.transform.CompareTag("Player") || hitInfo.transform.CompareTag("Bot")) 
            {
                target = hitInfo.transform.gameObject;
            }
        }
    }

    void Update() 
    {   
        UpdateTarget();
        if (target) 
        {
            if (infections.getInfectionNum() > 0)
                Chase();
            else 
                Flee();
        }
        else 
            Patrol();

    }

    void Fire() 
    {
        //Check that this target isn't the owner of an infection that's already been launched
        GameObject owner = GetComponentInChildren<Rigidbody>().gameObject;
        foreach (GameObject projectile in PoolManager.GetProjectilePool())
        {
            if (projectile.activeSelf && projectile.GetComponent<Projectile>().GetOwner() == owner)
            {
                isOwner = true;
            }
        }

        //If the target doesn't own any projectiles in the scene, fire a projectile
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
                //Set the projectile's position and forward vector

                if(projectile[p] != null)
                {
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
                    projectile[p].GetComponent<Projectile>().SetOwner(owner);
                    projectile[p].SetActive(true);
                    Debug.Log(projectile[p].name + " owner: " + owner.name);
                }
                
            }
        }
    }
    
}
