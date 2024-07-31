using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

public class MoveEntry
{
    private ChessPiece _chessPiece;
    private GridObject _fromGrid;
    private GridObject _toGrid;
    private int _iteration;

    public MoveEntry(ChessPiece chessPiece, GridObject fromGrid, GridObject toGrid, int iteration)
    {
        _chessPiece = chessPiece;
        _fromGrid = fromGrid;
        _toGrid = toGrid;
        _iteration = iteration;
    }

    public override string ToString()
    {
        string pieceName = _chessPiece.name; // veya _chessPiece.GetName(); ta��n ad�n� buraya ekleyin
        string fromPosition = _fromGrid.GetGridPosition().ToString(); // Ta��n ba�lad��� yerin konumu
        string toPosition = _toGrid.GetGridPosition().ToString(); // Ta��n gitti�i yerin konumu

        return $"Iteration {_iteration}: {pieceName} moved from {fromPosition} to {toPosition}";
    }
}
