using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuUIHandler : MonoBehaviour
{
    //Other components
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        canvas.enabled = false;

        //Hook up events
        GameManager.instance.OnGameStateChanged += OnGameStateChanged;
    }

    //Line 24 to 45 is written by Yan Jun
    /*When (Return)Enter key is pressed or onRaceAgain is being called,it will load the active scene,which
     * mean the player will start playing that scene from the beginning*/
    public void OnRaceAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.Return))
    {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
        /*When Backspace key  is pressed or OnExitToMainMenu() is called,
         * it will load scene Main Menu,which mean
         * player will go to the main page*/
        if (Input.GetKeyDown(KeyCode.Backspace))
    {
          SceneManager.LoadScene("MainMenu");
    }
    }
    public void OnExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator ShowMenuCO()
    {
        yield return new WaitForSeconds(1);

        canvas.enabled = true;
    }

    //Events 
    void OnGameStateChanged(GameManager gameManager)
    {
        if (GameManager.instance.GetGameState() == GameStates.raceOver)
        {
            StartCoroutine(ShowMenuCO());
        }
    }

    void OnDestroy()
    {
        //Unhook events
        GameManager.instance.OnGameStateChanged -= OnGameStateChanged;
    }

}
