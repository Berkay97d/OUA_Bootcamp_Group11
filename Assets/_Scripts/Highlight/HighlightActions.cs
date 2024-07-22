using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

public class HighlightActions : MonoBehaviour
{
    public static event Action<List<GridObject>> OnHighlightTiles;
    public static event Action<List<GridObject>> OnRemoveHighlightTiles;
    void Start()
    {
        ChessPieceMovement.OnChessPieceMove += HighlightCheck;
    }

    private void HighlightCheck(ChessPiece chessPiece, GridObject fromGrid, GridObject toGrid)
    {
        var gridsToRemoveHighlight = fromGrid.GetNeighboorGrids();
        OnRemoveHighlightTiles?.Invoke(gridsToRemoveHighlight);

        var gridsToHighlight = toGrid.GetMovableGrids();
        OnHighlightTiles?.Invoke(gridsToHighlight);
    }
}
