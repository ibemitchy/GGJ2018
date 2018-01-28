using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance { get; private set; }

    [Tooltip("The impact effect that should spawn when the projectile collides with something")]
    public GameObject impactEffectPrefab;
    [Tooltip("The projectile object that should spawn when the player shoots")]
    public GameObject projectilePrefab;
    [Tooltip("The maximum number of impact effects allowed in this scene")]
    public int maxNumImpactEffects;
    [Tooltip("The maximum number of projectiles allowed in this scene")]
    public int maxNumProjectiles;

    //The pool of all impact effects in the scene
    private GameObject[] impactEffectPool;
    //The pool of all projectiles in the scene
    private GameObject[] projectilePool;

    //A method to return the next impact effect from the pool that is not in use
    public static GameObject GetImpactEffect()
    {
        foreach(GameObject impactEffect in instance.impactEffectPool)
        {
            if(!impactEffect.activeSelf)
            {
                return impactEffect;
            }
        }

        Debug.LogError("[PoolManager.cs] All impact effects are in use!");
        return null;
    }
    //A method to return the next projectile object from the pool that is not in use
    public static GameObject GetProjectile()
    {
        foreach(GameObject projectile in instance.projectilePool)
        {
            if(!projectile.activeSelf)
            {
                return projectile;
            }
        }

        Debug.LogError("[PoolManager.cs] All projectiles are in use!");
        return null;
    }
    //A method to return the impact effect pool
    public static GameObject[] GetImpactEffectPool()
    {
        return instance.impactEffectPool;
    }
    //A method to return the projectile pool
    public static GameObject[] GetProjectilePool()
    {
        return instance.projectilePool;
    }
    //A method to return the max number of impact effects allowed in this scene
    public static int GetMaxNumImpactEffects()
    {
        return instance.maxNumImpactEffects;
    }
    //A method to return the max number of projectiles allowed in this scene
    public static int GetMaxNumProjectiles()
    {
        return instance.maxNumProjectiles;
    }
    //A method to return a reference to this pool manager
    public static PoolManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
    }
    void Start()
    {
        //initialize the projectile pool
        projectilePool = new GameObject[maxNumProjectiles];

        //Create a parent object for the projectile objects (for hierarchy organization)
        GameObject parent = new GameObject();
        parent.name = "Projectile Pool";

        //Create each projectile and set them to being inactive
        for(int p = 0; p < maxNumProjectiles; ++p)
        {
            projectilePool[p] = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
            projectilePool[p].transform.parent = parent.transform;
            projectilePool[p].SetActive(false);
        }

        //initialize the impact effect pool
        impactEffectPool = new GameObject[maxNumImpactEffects];

        //Create a parent object for the impact effects
        GameObject impactParent = new GameObject();
        impactParent.name = "Impact Effect Pool";

        //Create each impact effect and set them to being inactive
        for(int i = 0; i < maxNumImpactEffects; ++i)
        {
            impactEffectPool[i] = Instantiate(impactEffectPrefab, Vector3.zero, Quaternion.identity);
            impactEffectPool[i].SetActive(false);
            impactEffectPool[i].transform.parent = impactParent.transform;
            impactEffectPool[i].GetComponent<ParticleSystem>().Stop();
        }
    }
}
