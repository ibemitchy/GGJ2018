using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }

    private Dictionary<int, GameObject> playerList;

    public static Dictionary<int, GameObject> GetPlayerList()
    {
        return instance.playerList;
    }
    public static GameObject GetPlayer(int playerID)
    {
        return instance.playerList[playerID];
    }
    public static void RegisterPlayer(int playerID, GameObject player)
    {
        instance.playerList.Add(playerID, player);
    }
    public static void UnregisterPlayer(int playerID)
    {
        instance.playerList.Remove(playerID);
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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
