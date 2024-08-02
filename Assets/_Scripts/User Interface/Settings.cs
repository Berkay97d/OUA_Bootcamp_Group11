using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject musicPanel;
    public GameObject oyunPanel;

    public GameObject howToPlayPanel;
    public GameObject buttonsPanel;

    public GameObject StopPanel;

    void Start()
    {
        settingsPanel.SetActive(false);
        musicPanel.SetActive(false);
    }

    public void OpenPanel() // Setting paneli a�ar
    {
        settingsPanel.SetActive(true); 
        oyunPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        buttonsPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ClosePanel() // Setting paneli kapat�r
    {
        howToPlayPanel.SetActive(false);
        buttonsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        oyunPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ClosePanelGame()
    {
        settingsPanel.SetActive(false);
        oyunPanel.SetActive(true);
        musicPanel.SetActive(false);
        StopPanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void OpenMusicPanel()
    {
        musicPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(true);
        oyunPanel.SetActive(false);
        buttonsPanel.SetActive(false);
    }

    public void Buttons()
    {
        buttonsPanel.SetActive(true);
        oyunPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    public void PauseGame()
    {
        StopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReloadGame()
    {
        // Sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        musicPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
