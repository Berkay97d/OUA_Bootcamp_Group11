using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

public class HighlightActions : MonoBehaviour
{
    public static event Action<List<GridObject>, Color> OnHighlightTiles;
    public static event Action<List<GridObject>> OnRemoveHighlightTiles;
    void Start()
    {
        ChessPieceMovement.OnChessPieceMove += MoveHighlightCheck;
        ChessPieceFire.OnChessPieceFire += FireHighlight;
    }

    private void MoveHighlightCheck(ChessPiece chessPiece, GridObject fromGrid, GridObject toGrid)
    {
        Color moveHighlightColor = Color.green;
        var gridsToRemoveHighlight = fromGrid.GetNeighboorGrids();
        OnRemoveHighlightTiles?.Invoke(gridsToRemoveHighlight);

        var gridsToHighlight = toGrid.GetMovableGrids();
        OnHighlightTiles?.Invoke(gridsToHighlight, moveHighlightColor);
    }

    private void FireHighlight(ChessPiece _chessPiece, GridObject currentGrid, bool isFire)
    {
        var attackTiles = _chessPiece.GetAttackPattern();
        var moveTiles = currentGrid.GetNeighboorGrids();

        Color fireHighlightColor = Color.red;
        Color moveHighlightColor = Color.green;

        if(isFire)
        {
            OnRemoveHighlightTiles?.Invoke(moveTiles);
            OnHighlightTiles?.Invoke(attackTiles, fireHighlightColor);
        }

        else
        {
            OnRemoveHighlightTiles?.Invoke(attackTiles);
            OnHighlightTiles?.Invoke(moveTiles, moveHighlightColor);
        }
    }
}
