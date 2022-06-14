using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_menu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject resolutionMenuUI;

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
    public void Settings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    public void ResolutionSettings() 
    {
        settingsMenuUI.SetActive(false);
        resolutionMenuUI.SetActive(true);
    }
    public void BackToMainMenu() // Из настроек в главное меню
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
    public void BackToSettings() // Из настроек разрешения в настройки
    {
        settingsMenuUI.SetActive(true);
        resolutionMenuUI.SetActive(false);
    }
}
    

