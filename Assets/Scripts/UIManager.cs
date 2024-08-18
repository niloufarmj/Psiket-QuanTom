using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image catched;
    public Slider locationSlider, speedSlider;
    public static UIManager instance;
    public GameObject pauseMenu, pauseButton;
    public bool isMobile;
    public GameObject mobileInput;
    public TextMeshProUGUI timerText;

    private float timer = 0f;

    void Start()
    {
        instance = this;
        timer = 0f;

        isMobile = Application.isMobilePlatform;

        //isMobile = true;

        if (isMobile) mobileInput.SetActive(true); else mobileInput.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        // Update the timer if the game is not paused
        if (!GameManager.instance.gamePaused)
        {
            UpdateTimer();
        }

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

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutesValue = Mathf.FloorToInt(timer / 60);
        int secondsValue = Mathf.FloorToInt(timer % 60);

        timerText.text = minutesValue.ToString("00") + ":" + secondsValue.ToString("00");
    }
}
