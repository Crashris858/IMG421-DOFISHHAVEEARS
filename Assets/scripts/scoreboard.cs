using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class scoreboard : MonoBehaviour
{
    public int Score =0; 
    public String CoolnessFactor;
    public TextMeshProUGUI ScoreUI; 
    public TextMeshProUGUI Coolness; 
    public TextMeshProUGUI Timer; 
    public float timeRemaining=60;  
    // Start is called before the first frame update
    void Start()
    {
        Coolness.text="Flopping";
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUI.text = "Score: " + Score.ToString();
        Timer.text= "Time: " + ((int)timeRemaining).ToString(); 
        //decrease timer
        timeRemaining-=Time.deltaTime; 
        if(timeRemaining<=0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void UpdateCoolness()
    {
        if(Score>=500&&Score<1000)
        {
            Coolness.text="Blubbing";
            Coolness.fontSize=60;
        }
        else if(Score>=1000&&Score<3000)
        {
            Coolness.text="Fintastic";
            Coolness.fontSize=70;
        }
        else if(Score>=3000)
        {
            Coolness.text="Flipping Awesome";
            Coolness.fontSize=80; 
        }
    }
}
