using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

public class HighlightActions : MonoBehaviour
{
    public static event Action<List<GridObject>, Color> OnHighlightTiles;
    public static event Action OnClearTiles;
    void Start()
    {
        ChessPieceMovement.OnChessPieceMove += MoveHighlightCheck;
        ChessPieceMovement.OnSpecialKingMove += KingHighlightCheck;
        ChessPieceFire.OnChessPieceFire += FireHighlight;
    }

    private void MoveHighlightCheck(ChessPiece chessPiece, GridObject fromGrid, GridObject toGrid)
    {
        Color moveHighlightColor = Color.green;
        OnClearTiles?.Invoke();

        var gridsToHighlight = toGrid.GetMovableGrids();
        OnHighlightTiles?.Invoke(gridsToHighlight, moveHighlightColor);
    }

    private void KingHighlightCheck(ChessPiece _chessPiece, GridObject _currentGridObject, List<GridObject> movableGrids)
    {
        Color kingHighlightColor = Color.blue;
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
