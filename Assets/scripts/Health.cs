using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour 
{
	private int maxHealth;
    public int currentHealth;
    public RectTransform healthBar;


    //private float nextActionTime = 0.0f;
    //public float period = 30.0f;



    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }


    private void Update()
    {
        //if (period > nextActionTime)
        //{
        //    nextActionTime += 0.1f;
        //}
        //else
        //{
        //    takeDamage(1);
        //    nextActionTime = 0.0f;
        //}

        //// update health bar
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        ////
    }   



    // If object hit by bullet, current health decreases by amount
    public void takeDamage(int amount)
   	{
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead!");
            currentHealth = 0;

            gameObject.SetActive(false);
        }
   	}

   	// If object get heal, current health increases by amount
   	public void giveHeal(int amount) 
   	{ 		
   		int healthAtfterHeal = currentHealth + amount;

   		// If character dead already, user can't heal his character
   		if (currentHealth == 0)  
   		{
   			Debug.Log("Already Dead! Impossible to heal");
   		}
   		// if health after heal is less than or equal to maxHealth, 
   		// currentHealth is euqal to healthAtfterHeal
   		else if (healthAtfterHeal <= maxHealth) 
   		{
   			currentHealth = healthAtfterHeal;
   		} 
   		// if health after heal is greather than maxHealth, 
   		// currentHealth will be equal to maxHealth
   		else 
   		{
   			currentHealth = maxHealth;
   		}
   	}

   	public void updateMaxHealth(int newMaxHealth) 
    {
   		maxHealth = newMaxHealth;
   	}

   	public int getMaxHealth() {
   		return maxHealth;
   	}

   	public void updateCurrentHealth(int newCurrentHealth) 
   	{
   		currentHealth = newCurrentHealth;
   	}

   	public int getCurrentHealth() 
   	{
   		return currentHealth;
   	}

}
