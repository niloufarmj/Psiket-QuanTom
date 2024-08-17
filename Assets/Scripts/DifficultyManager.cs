using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public Slider difficultySlider;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetFloat("difficulty", 1);
        }
        else
        {
            Load();
        }
    }

    public void ChangeDifficulty()
    {
        Save();
    }

    private void Load()
    {
        difficultySlider.value = PlayerPrefs.GetFloat("difficulty") - 1;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("difficulty", difficultySlider.value + 1);
    }
}
