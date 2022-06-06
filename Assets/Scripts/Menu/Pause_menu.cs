using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject Dead;
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
        settingsMenu.SetActive(false);
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
    public void SettingsMenu()
    {
        pauseMenuUI.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void DeadScreen()
    {
        if(character.lifes <=0)
        {
            Dead.SetActive(true);
            Interface.SetActive(false);
            Time.timeScale = 0f;
            GameIsPaused = true;

        }
    }
}
