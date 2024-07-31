using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeft : MonoBehaviour
{
    [SerializeField] private float startTime = 20f; // Ba�lang�� s�resi (saniye olarak)
    private float timeRemaining;
    [SerializeField] public TextMeshProUGUI timerText; // Saya� i�in Text bile�eni
    // [SerializeField] public GameObject gameOverPanel; // Game Over ekran�

    private Buttons button;
    private Pieces pieces;
    private MoveBook moveBook;

    void Start()
    {
        button = FindObjectOfType<Buttons>();
        pieces = FindObjectOfType<Pieces>();
        moveBook = FindObjectOfType<MoveBook>();

        ResetIteration();
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
            ResetIteration(); // S�re s�f�rland���nda tekrar ba�lat
        }
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // void ResetTimer()
    // {
    //    timeRemaining = startTime;
        // gameOverPanel.SetActive(false); // E�er Game Over panelini tekrar gizlemek isterseniz
    //    UpdateTimerText(); // Timer metnini hemen g�ncelle
    // }

    public void ResetIteration()
    {
        timeRemaining = startTime;
        button.ResetButtons(); // Butonlar� s�f�rla
        pieces.ResetPieces();// Ta�lar� s�f�rla
        moveBook.NextIteration(); // Bir sonraki iterasyona ge�
        UpdateTimerText(); // Timer metnini hemen g�ncelle
    }

    // void GameOver()
    // {
    //     gameOverPanel.SetActive(true);
    //     // Di�er Game Over i�lemleri buraya eklenebilir
    // }
}