using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using ChessPieces;
using TurnSystem;
using UnityEngine;

public class HighlightActions : MonoBehaviour
{
    public static event Action<List<GridObject>, Color> OnHighlightTiles;
    public static event Action OnClearTiles;
    void Start()
    {
        //ChessPieceMovement.OnChessPieceMove += MoveHighlightCheck;
        ChessPieceMovement.OnSpecialKingMove += KingHighlightCheck;
        ChessPieceFire.OnChessPieceFire += FireHighlight;
        Unit.OnHasTurnChanged += OnHasTurnChanged;
        IterationController.OnIterationCompleted += IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationCompletedWithKingLoss += IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationReset += IterationControllerOnOnIterationCompleted;
    }

    private void OnDestroy()
    {
        //ChessPieceMovement.OnChessPieceMove -= MoveHighlightCheck;
        ChessPieceMovement.OnSpecialKingMove -= KingHighlightCheck;
        ChessPieceFire.OnChessPieceFire -= FireHighlight;
        Unit.OnHasTurnChanged -= OnHasTurnChanged;
        IterationController.OnIterationCompleted -= IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationCompletedWithKingLoss -= IterationControllerOnOnIterationCompleted;
        IterationController.OnIterationReset -= IterationControllerOnOnIterationCompleted;
    }

    private void IterationControllerOnOnIterationCompleted()
    {
        StartCoroutine(InnerRoutine());
        
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(1f);
            OnClearTiles?.Invoke();    
        }
        
    }

    private void OnHasTurnChanged(GridPosition arg1, bool arg2)
    {
        if (arg2)
        {
            Color moveHighlightColor = Color.green;
            OnClearTiles?.Invoke();

            var gridsToHighlight = ChessGrid.GetGridSystem().GetGridObject(arg1).GetMovableGrids();
            OnHighlightTiles?.Invoke(gridsToHighlight, moveHighlightColor);    
        }
        
    }

    private void MoveHighlightCheck(UnitTurnData unitTurnData)
    {
        Color moveHighlightColor = Color.green;
        OnClearTiles?.Invoke();

        var gridsToHighlight = unitTurnData.targetGrid.GetMovableGrids();
        OnHighlightTiles?.Invoke(gridsToHighlight, moveHighlightColor);
    }

    
    
    private void KingHighlightCheck(ChessPiece _chessPiece, GridObject _currentGridObject, List<GridObject> movableGrids, bool isNormalMove)
    {
        Color kingHighlightColor;
        
        if (isNormalMove)
        {
             kingHighlightColor = Color.green;    
        }
        else
        {
             kingHighlightColor = Color.blue;
        }
        
        OnClearTiles?.Invoke();
        OnHighlightTiles?.Invoke(movableGrids, kingHighlightColor);
    }

    private void FireHighlight(ChessPiece _chessPiece, GridObject currentGrid, bool isFire)
    {
        var attackTiles = _chessPiece.GetAttackPattern();
        var moveTiles = currentGrid.GetNeighboorGrids();

        Color fireHighlightColor = Color.red;
        Color moveHighlightColor = Color.green;

        if(isFire)
        {
            OnClearTiles?.Invoke();
            OnHighlightTiles?.Invoke(attackTiles, fireHighlightColor);
        }

        else
        {
            OnClearTiles?.Invoke();
            OnHighlightTiles?.Invoke(moveTiles, moveHighlightColor);
        }
    }
}
