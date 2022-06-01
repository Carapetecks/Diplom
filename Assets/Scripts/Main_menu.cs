using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    
    public GameObject mainMenuUI;
    private void Update()
    {
        if (Pause_menu.GameIsPaused == true)
        {
            Time.timeScale = 1;
        }

    }
    
public void Play()
    {
        SceneManager.LoadScene("FirstScene");     
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

