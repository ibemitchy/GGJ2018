using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

	private bool infectedFlag;
	public int infectionNum;
    private int projectileNum;
    public int damage; 
    private Health h;
    public float time;
    private float timer;
	
	// Use this for initialization
	void Start ()
	{
		infectedFlag = true;
        infectionNum = 1;
        h = GetComponent<Health>();
	}

    // increment number of infections
	public void incrementInfectionNumber()
	{
		this.infectionNum++;
	}
	
    // decrement number of infections
	public void decrementInfectionNumber()
	{
		if (this.infectedFlag)
		{
			this.infectionNum--;
		}	
	}

    // if the object is hitted, decrement the number of infections
	public void hittedByProjectile()
	{
		incrementInfectionNumber();
        projectileNum = infectionNum;
	}

    // if the object hit others successfully, increment the number of infections
    public void successfulHit(){
        decrementInfectionNumber();
        projectileNum = infectionNum;
        if(infectionNum == 0){
            infectedFlag = false;
        }
    }

    // lunch projectile
	public void lunchProjectile()
	{
        if (projectileNum > 0){
            // 
            projectileNum--;
        }
	}

    // take damage on health by damage * infectionNum
    public void damageHealth(){
        h.takeDamage(damage * infectionNum);
    }
	
	// Update is called once per frame
	void Update () {
        timer = time;
        timer -= Time.deltaTime;
        if (timer <= 00f){
            damageHealth();
        }
        timer = time;
	}
}
