using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeLeft : MonoBehaviour
{
    [SerializeField] private float startTime = 20f; // Baþlangýç süresi (saniye olarak)
    private float timeRemaining;
    [SerializeField] public TextMeshProUGUI timerText; // Sayaç için Text bileþeni
    // [SerializeField] public GameObject gameOverPanel; // Game Over ekraný

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
            ResetIteration(); // Süre sýfýrlandýðýnda tekrar baþlat
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
        // gameOverPanel.SetActive(false); // Eðer Game Over panelini tekrar gizlemek isterseniz
    //    UpdateTimerText(); // Timer metnini hemen güncelle
    // }

    public void ResetIteration()
    {
        timeRemaining = startTime;
        button.ResetButtons(); // Butonlarý sýfýrla
        pieces.ResetPieces();// Taþlarý sýfýrla
        moveBook.NextIteration(); // Bir sonraki iterasyona geç
        UpdateTimerText(); // Timer metnini hemen güncelle
    }

    // void GameOver()
    // {
    //     gameOverPanel.SetActive(true);
    //     // Diðer Game Over iþlemleri buraya eklenebilir
    // }
}