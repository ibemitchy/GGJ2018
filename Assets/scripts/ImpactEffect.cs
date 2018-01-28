using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    private ParticleSystem ps;
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if(!ps)
        {
            Debug.LogError("[ImpactEffect.cs] Can't find the particle system on this effect!");
        }
    }
    // Update is called once per frame
    void Update ()
    {
		if(!ps.IsAlive())
        {
            ps.Stop();
            gameObject.SetActive(false);
        }
	}
}
