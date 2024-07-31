using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using TMPro;
using TurnSystem;

public class TimeLeft : MonoBehaviour
{
    [SerializeField] private float startTime; // Baslangıc suresi (saniye olarak)
    private float timeRemaining;
    [SerializeField] public TextMeshProUGUI timerText; // Sayac icin Text bileceni
    // [SerializeField] public GameObject gameOverPanel; // Game Over ekran

    private Buttons button;
    
    private MoveBook moveBook;

    private void Awake()
    {
        timeRemaining = startTime;
    }

    void Start()
    {
        button = FindObjectOfType<Buttons>();
        
        moveBook = FindObjectOfType<MoveBook>();
        
        IterationController.OnIterationReset += IterationControllerOnOnIterationReset;
        IterationController.OnIterationCompleted += IterationControllerOnOnIterationReset;
    }

    private void OnDestroy()
    {
        IterationController.OnIterationReset -= IterationControllerOnOnIterationReset;
        IterationController.OnIterationCompleted -= IterationControllerOnOnIterationReset;
    }

    private void IterationControllerOnOnIterationReset()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        
        if (timeRemaining > 0)
        {
            timeRemaining += Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            ResetIteration(); // süre sıfırlandığında 
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.CeilToInt(timeRemaining % 60);
        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // void ResetTimer()
    // {
    //    timeRemaining = startTime;
        // gameOverPanel.SetActive(false); // Eğer Game Over panelini tekrar gizlemek isterseniz
    //    UpdateTimerText(); // Timer metnini hemen g�ncelle
    // }

    public void ResetIteration()
    {
        timeRemaining = startTime;
        button.ResetButtons(); // Butonları sıfırla
        IterationController.RaiseOnIterationReset();
        UpdateTimerText(); // Timer metnini hemen g�ncelle
    }

    // void GameOver()
    // {
    //     gameOverPanel.SetActive(true);
    //     // Di�er Game Over i�lemleri buraya eklenebilir
    // }
}