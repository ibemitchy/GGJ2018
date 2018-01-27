using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public GameObject nagent;
	public GameObject goalObject;

	void start() 
	{
		Invoke("SpawnAgent", 2);
	}

	void spawnAgent() 
	{
		GameObject na = (GameObject) Instantiate(nagent, this.transform.position, Quaternion.identity);
		na.GetComponent<BotMovement>().goal = goalObject.transform;
		Invoke("SpawnAgent", Random.Range(2,5));
	}
}
