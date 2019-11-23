using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Time shit
    public TextMesh textMesh;
    public int startTime = 8;
    private double currentHour;
    public GameObject textMeshOB;
    public float gameTimeUntilDeath = 30;
    public float currentGameTime = 0;
    double oldHour = 0;

    //Player Shit
    public PlayerMovement pm;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMovement>();
        textMesh = textMeshOB.GetComponent<TextMesh>();
        currentHour = startTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        setTime(currentHour);
        countTime();
    }

    public void setTime(double hour)
    {
        string newTime;
        if (hour < 10)
        {
            newTime = $"0{hour}:00";
        }

        else
        {
            newTime = $"{hour}:00";
        }
        textMesh.text = newTime;

    }

    public void countTime()
    {
        currentGameTime += Time.deltaTime;
        float percentageTimeLeft = currentGameTime / gameTimeUntilDeath;
        double newHour = Math.Truncate(percentageTimeLeft * 12);
        if(newHour != oldHour)
        {
            currentHour++;
            oldHour++;
        }

        if(currentGameTime > gameTimeUntilDeath)
        {
            pm.Kill();
        }
    }
}
