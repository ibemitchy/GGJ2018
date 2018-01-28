using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private bool gameOver;
    private bool possibleWinner;
    private List<GameObject> spawnPoints;

    public static bool GetGameOverFlag()
    {
        return instance.gameOver;
    }
	public static GameManager GetInstance()
    {
        return instance;
    }
    public static List<GameObject> GetSpawnPoints()
    {
        return instance.spawnPoints;
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
        gameOver = false;
        possibleWinner = false;
    }
    void Update()
    {
        //Go through the list of players and see who's alive still
        // foreach(KeyValuePair<int, GameObject> player in PlayerManager.GetPlayerList())
        // {
        //     if(!possibleWinner)
        //     {
        //         possibleWinner = true;
        //     }
        //     else
        //     {
        //         possibleWinner = false;
        //         break;
        //     }
        // }

        if(possibleWinner)
        {
            gameOver = true;
        }
    }
}
