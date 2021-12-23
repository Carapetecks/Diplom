using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{
    public static bool GameIsPaused = true;
    public GameObject mainMenuUI;   
    void Update()
    {
       

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

