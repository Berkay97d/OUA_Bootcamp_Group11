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

    public AudioSource musicSource; // Müzik kaynaðý
    public Slider volumeSlider; // Ses kontrol sliderý

    void Start()
    {
        settingsPanel.SetActive(false);
        musicPanel.SetActive(false);

        if (musicSource != null && volumeSlider != null)
        {
            volumeSlider.value = musicSource.volume; // Slider'ýn baþlangýç deðerini ses seviyesine eþitle
            volumeSlider.onValueChanged.AddListener(SetVolume); // Slider deðiþikliklerinde SetVolume metodunu çaðýr
        }
    }

    void SetVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = volume; // Müzik kaynaðýnýn ses seviyesini slider deðeri ile ayarla
        }
    }

    public void OpenPanel() // setting paneli açar
    {
        settingsPanel.SetActive(true); 
        oyunPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ClosePanel() // setting paneli kapatýr
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
        // Sahneyi yeniden yükle
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
