using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        

    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resumed();
               
            }


            else
            {
                Pausused();
              
            }


        }

    }


    public void Pausused()
    {
        pauseMenu.SetActive(true);

     

        isPaused = true;
    }


    public void Resumed()
    {
        pauseMenu.SetActive(false);
       

        isPaused = false;
    }

    public void mainMnu()
    {
        SceneManager.LoadScene("Start");
        
    }


    public void OnSaveGameClicked()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}

