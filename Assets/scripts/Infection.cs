﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

	private bool infectedFlag;
	public int infectionNum;
    private int projectileNum;
    public int damage; 
    public Health h;
    public float time;
    private float timer;
	
	// Use this for initialization
	void Start ()
	{
		infectedFlag = true;
        infectionNum = 1;
        h = GetComponent<Health>();
        timer = time;
	}

    public void setInfectionNum(int a){
        this.infectionNum = a;
    }
    public int getInfectionNum()
    {
        return infectionNum;
    }

    // increment number of infections
	public void IncrementInfectionNumber()
	{
		this.infectionNum++;
        if (infectionNum > 0)
        {
            infectedFlag = true;
        }
	}
	
    // decrement number of infections
	public void DecrementInfectionNumber()
	{
		if (this.infectedFlag)
		{
			this.infectionNum--;
		}	
        if (infectionNum == 0)
        {
            infectedFlag = false;
        }
	}

    // if the object is hitted, decrement the number of infections
	public void HittedByProjectile()
	{
		IncrementInfectionNumber();
        projectileNum = infectionNum;
	}

    // if the object hit others successfully, increment the number of infections
    public void SuccessfulHit(){
        DecrementInfectionNumber();
        projectileNum = infectionNum;
    }

    // lunch projectile
	public void LunchProjectile()
	{
        if (projectileNum > 0){
            // 
            projectileNum--;
        }
	}

    // take damage on health by damage * infectionNum
    public void DamageHealth(){
        h.takeDamage(damage * infectionNum);
    }
	
	// Update is called once per frame
	void Update () {
        
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            DamageHealth();
			timer = time;
        }
	}
}
