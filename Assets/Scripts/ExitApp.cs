using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApp : MonoBehaviour
{//All code is written by Yan Jun
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {//if escape key ispressed,it will quit application for the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    //if function exitApp() is triggered,it will quit application for the game
    public void exitApp()
    {
        Application.Quit();
    }
}
