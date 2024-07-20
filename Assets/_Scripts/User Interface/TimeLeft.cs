using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeft : MonoBehaviour
{
    [SerializeField] private float startTime = 60f; // Ba�lang�� s�resi (saniye olarak)
    private float timeRemaining;
    [SerializeField] public TextMeshProUGUI timerText; // Saya� i�in Text bile�eni
    // [SerializeField] public GameObject gameOverPanel; // Game Over ekran�

    void Start()
    {
        timeRemaining = startTime;
        // gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            GameOver();
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        // gameOverPanel.SetActive(true);
        // Di�er Game Over i�lemleri buraya eklenebilir
    }
}
