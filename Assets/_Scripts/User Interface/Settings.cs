using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject musicPanel;
    public GameObject oyunPanel;

    public AudioSource musicSource; // M�zik kayna��
    public Slider volumeSlider; // Ses kontrol slider�

    void Start()
    {
        settingsPanel.SetActive(false);
        musicPanel.SetActive(false);

        if (musicSource != null && volumeSlider != null)
        {
            volumeSlider.value = musicSource.volume; // Slider'�n ba�lang�� de�erini ses seviyesine e�itle
            volumeSlider.onValueChanged.AddListener(SetVolume); // Slider de�i�ikliklerinde SetVolume metodunu �a��r
        }
    }

    void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume; // M�zik kayna��n�n ses seviyesini slider de�eri ile ayarla
        }
    }

    public void OpenPanel() // setting paneli a�ar
    {
        settingsPanel.SetActive(true); 
        oyunPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ClosePanel() // setting paneli kapat�r
    {
        settingsPanel.SetActive(false);
        oyunPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OpenMusicPanel()
    {
        musicPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ReloadGame()
    {
        // Sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("UI Scene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
