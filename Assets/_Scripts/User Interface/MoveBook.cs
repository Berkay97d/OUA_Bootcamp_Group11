using _Scripts.Grid_System;
using ChessPieces;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveBook : MonoBehaviour
{
    private Dictionary<int, List<MoveEntry>> moveLog = new Dictionary<int, List<MoveEntry>>();
    private int currentIteration = 1;
    public TextMeshProUGUI logText;

    void Start()
    {
        moveLog[currentIteration] = new List<MoveEntry>();
    }

    public void AddMove(ChessPiece chessPiece, GridObject fromGrid, GridObject toGrid)
    {
        MoveEntry entry = new MoveEntry(chessPiece, fromGrid, toGrid, currentIteration);
        if (!moveLog.ContainsKey(currentIteration))
        {
            moveLog[currentIteration] = new List<MoveEntry>();
        }
        moveLog[currentIteration].Add(entry);
        UpdateLogText();
    }

    public void NextIteration()
    {
        currentIteration++;
        if (!moveLog.ContainsKey(currentIteration))
        {
            moveLog[currentIteration] = new List<MoveEntry>();
        }
        UpdateLogText();
    }

    public void ResetIterations()
    {
        currentIteration = 1;
        moveLog.Clear();
        moveLog[currentIteration] = new List<MoveEntry>();
    }

    private void UpdateLogText()
    {
        if (logText != null)
        {
            logText.text = ""; // Mevcut metni temizle
            foreach (var iteration in moveLog)
            {
                logText.text += $"Iteration {iteration.Key}:\n";
                foreach (var entry in iteration.Value)
                {
                    logText.text += entry.ToString() + "\n";
                }
                logText.text += "\n";
            }
        }
    }
}