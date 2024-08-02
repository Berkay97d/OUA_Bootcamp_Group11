using _Scripts.Grid_System;
using ChessPieces;

public class UnitTurnData
{
    public bool isFire;
    public GridObject previousGrid;
    public GridObject targetGrid;
    public GridObject shotGrid;
    public ChessPiece chessPiece;

    public UnitTurnData(bool isFire, GridObject prev, GridObject target, GridObject shot, ChessPiece piece)
    {
        this.isFire = isFire;
        this.previousGrid = prev;
        this.targetGrid = target;
        this.shotGrid = shot;
        this.chessPiece = piece;
    }
}
