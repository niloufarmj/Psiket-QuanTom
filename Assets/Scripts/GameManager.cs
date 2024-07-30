using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject jerryPrefab;

    public SpeedSystem currentSpeed;
    int currentPower;
    GameObject mainJerry;

    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        InitJerry();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentPower <= 5)
                AddNewJerrys();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentPower >= 1)
                RemoveJerrys();
        }
    }

    void InitJerry()
    {
        currentPower = 0;

        currentSpeed = new SpeedSystem
        {
            moveSpeed = 16f,
            maxWaitTime = 0.4f,
            range = 200f
        };

        mainJerry = Instantiate(jerryPrefab, transform.position, Quaternion.identity);
        mainJerry.tag = "MainJerry";
    }

    void AddNewJerrys()
    {
        currentPower++;

        currentSpeed.moveSpeed -= 2.2f;
        currentSpeed.maxWaitTime += 0.1f;
        currentSpeed.range += 100;


        GameObject[] jerrys = GameObject.FindGameObjectsWithTag("Jerry");
        
        for (int i = 0; i < jerrys.Length; i++)
        {
            Instantiate(jerryPrefab, jerrys[i].transform.position, Quaternion.identity);
        }

        Instantiate(jerryPrefab, mainJerry.transform.position, Quaternion.identity);
    }

    void RemoveJerrys()
    {
        currentPower--;
        currentSpeed.moveSpeed += 2.2f;
        currentSpeed.maxWaitTime -= 0.1f;
        currentSpeed.range -= 100;

        GameObject[] jerrys = GameObject.FindGameObjectsWithTag("Jerry");

        for (int i = 0; i < jerrys.Length; i+=2)
        {
            Destroy(jerrys[i]);
        }

    }

}
