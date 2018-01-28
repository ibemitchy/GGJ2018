using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIManagerInstance;

    public GameObject pausePanel;
    public GameObject healthSlider;
    public GameObject infectionSlider;
    public Infection infection;
    public bool isPause;

    public static UIManager Instance()
    {    // Single Instance assurity
        return UIManagerInstance;
    }


    void Awake()
    {
        if (UIManagerInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            UIManagerInstance = this;
        }
        else if (UIManagerInstance != this)
        {
            DestroyImmediate(gameObject);
            return;
        }
    }


    void togglePanel(bool state)
    {
        if (state)
        {
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.gameObject.SetActive(false);
        }
    }

    public void resume(){
        isPause = false;
        Update();
    }

    public void HealthSlider(){
        infection.h.getCurrentHealth();
    }

    public void InfectionSlider()
    {
        infection.getInfectionNum();
    }



    // Use this for initialization
    void Start()
    {
        isPause = false;
        infection = GetComponent<Infection>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetKey(KeyCode.Q))
        {
            isPause = true;
        }
        //Debug.Log("isPause = " + isPause);

        if (isPause)
        {
            togglePanel(true);
        }
        else
        {
            togglePanel(false);
        }


    }
}
