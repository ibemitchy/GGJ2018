using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameManager instance { get; private set; }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

	// Use this for initialization
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
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}