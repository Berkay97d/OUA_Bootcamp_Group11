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

    private void FireHighlight(List<GridObject> attackTiles, bool isFire)
    {
        if(isFire)
        {
            Color fireHighlightColor = Color.red;
            OnHighlightTiles?.Invoke(attackTiles, fireHighlightColor);
        }

        else
        {
            OnRemoveHighlightTiles?.Invoke(attackTiles);
        }
    }
}
