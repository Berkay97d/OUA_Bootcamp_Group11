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
        string pieceName = _chessPiece.name; // veya _chessPiece.GetName(); taşın adını buraya ekleyin
        var fromPosition = _fromGrid.GetGridPosition(); // Taşın başladığı yerin konumu
        var toPosition = _toGrid.GetGridPosition(); // Taşın gittiği yerin konumu
        pieceName = RemoveWord(pieceName, "(Clone)");
        
        return $" {pieceName} {fromPosition._x+1}-{fromPosition._z+1} --> {toPosition._x+1}-{toPosition._z+1}";
    }
    private string RemoveWord(string input, string word)
    {
        if (input.Contains(word))
        {
            input = input.Replace(word, "");
        }
        return input;
    }
}
