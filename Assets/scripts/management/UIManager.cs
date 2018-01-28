using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIManagerInstance;

    public Canvas menu;
    public Canvas hud;

    public GameObject pausePanel;
    private Slider healthSlider;
    private Slider infectionSlider;

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
            //Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.gameObject.SetActive(false);
        }
    }

    public void HealthSlider(){
        healthSlider.value = infection.h.getCurrentHealth();
    }

    public void InfectionSlider()
    {
        infectionSlider.value = infection.getInfectionNum();
    }

    // Use this for initialization
    void Start()
    {
        isPause = false;

        Slider[] sliderObjects = hud.gameObject.GetComponentsInChildren<Slider>();
        foreach(Slider slider in sliderObjects)
        {
            if(slider.name == "Health")
            {
                healthSlider = slider;
            }
            else if(slider.name == "Infection")
            {
                infectionSlider = slider;
            }

        }
        infection = GetComponent<Infection>();
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetKey(KeyCode.Escape))
        {
            menu.enabled = true;
            //isPause = true;
        }
        Debug.Log("isPause = " + isPause);

        //togglePanel(isPause);


    }
}
