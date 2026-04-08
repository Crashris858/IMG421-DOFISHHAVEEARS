using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{   
    public Button StartButton;
    //public Button StartButton; 
    // Start is called before the first frame update
    void Start()
    {
        //StartButton= GameObject.Find("Start").GetComponent<Button>(); 
        StartButton.onClick.AddListener(Start_Clicked);
    }

    // Update is called once per frame
    void Start_Clicked()
    {
        print("start");
        SceneManager.LoadScene("game");
    }
}
