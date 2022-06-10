using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject Dead;
    public GameObject ChitPanel;
    public GameObject Interface;
    public Character character;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        DeadScreen();
        ChitPanelTurn();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void DeadScreen()
    {
        if (character.lifes <= 0)
        {
            Dead.SetActive(true);
            Interface.SetActive(false);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }
    }

    public void ChitPanelTurn()
    {
        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.D))
        {
            ChitPanel.SetActive(true);
            Interface.SetActive(false);
        }
    }

    public void ChitPanelOff()
    {
        ChitPanel.SetActive(false);
        Interface.SetActive(true);
    }

    

 
}
