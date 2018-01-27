using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	private int maxHealth = 100;
    private int currentHealth = maxHealth;

    // If object hit by bullet, current health decreases by amount
    public void takeDamage(int amount)
   	{
		int healthAtferDamage = currentHealth - amount;
        if (healthAtferDamage <= 0)
        {
            currentHealth = 0;
            Debug.Log("Dead!");
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
