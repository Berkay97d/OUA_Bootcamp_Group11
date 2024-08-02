using _Scripts.Grid_System;
using ChessPieces;
using System.Collections.Generic;
using _Scripts;
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
        ChessPieceMovement.OnChessPieceMove += AddMove;
        IterationController.OnIterationCompleted += IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationReset += IterationControllerOnOnIterationReset;
        IterationController.OnIterationCompletedWithKingLoss += IterationControllerOnOnIterationCompleted;
    }
    

    private void IterationControllerOnOnIterationReset()
    {
        ResetIterations();
    }

    private void IterationControllerOnOnIterationCompleted()
    {
        NextIteration();
    }

    private void OnDestroy()
    {
        ChessPieceMovement.OnChessPieceMove -= AddMove;
        IterationController.OnIterationCompleted -= IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationReset -= IterationControllerOnOnIterationReset;
        IterationController.OnIterationCompletedWithKingLoss -= IterationControllerOnOnIterationCompleted;
    }

    private void AddMove(UnitTurnData unitTurnData)
    {
        MoveEntry entry = new MoveEntry(unitTurnData.chessPiece, unitTurnData.previousGrid, unitTurnData.targetGrid, currentIteration);
        if (!moveLog.ContainsKey(currentIteration))
        {
            moveLog[currentIteration] = new List<MoveEntry>();
        }
        moveLog[currentIteration].Add(entry);
        UpdateLogText();
    }

    private void NextIteration()
    {
        currentIteration++;
        if (!moveLog.ContainsKey(currentIteration))
        {
            moveLog[currentIteration] = new List<MoveEntry>();
        }
        UpdateLogText();
    }

    private void ResetIterations()
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
                logText.text += $"LEVEL {iteration.Key}:\n";
                foreach (var entry in iteration.Value)
                {
                    logText.text += entry.ToString() + "\n";
                }
                logText.text += "\n";
            }
        }
    }
}