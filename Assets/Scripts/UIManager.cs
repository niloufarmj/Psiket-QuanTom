using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image catched;
    public Slider locationSlider, speedSlider;
    public static UIManager instance;
    public GameObject pauseMenu, pauseButton;

    void Start()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        catched.fillAmount = GameManager.instance.collisionTimer / PlayerPrefs.GetFloat("difficulty");
        locationSlider.onValueChanged.AddListener(LocationChanged);
        speedSlider.onValueChanged.AddListener(SpeedChanged);

    }
    
    public void PowerUpdate(int currentPower)
    {
        locationSlider.value = 6 - currentPower;
        speedSlider.value = currentPower;
    }

    private void LocationChanged(float value)
    {
        while (6 - GameManager.instance.currentPower > value)
        {
            GameManager.instance.AddNewJerrys();
        }
        
        while (6 - GameManager.instance.currentPower < value)
        {
            GameManager.instance.RemoveJerrys();
        }

        PowerUpdate(GameManager.instance.currentPower);
    }

    private void SpeedChanged(float value)
    {
        while (GameManager.instance.currentPower > value)
        {
            GameManager.instance.RemoveJerrys();
        }

        while (GameManager.instance.currentPower < value)
        {
            GameManager.instance.AddNewJerrys();
        }

        PowerUpdate(GameManager.instance.currentPower);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }


}
