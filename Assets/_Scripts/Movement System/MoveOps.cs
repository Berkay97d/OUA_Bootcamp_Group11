using _Scripts.Grid_System;
using UnityEngine;

public class MoveOps : MonoBehaviour
{
    private static ChessPiece[,] _chessPieces;
    private static GridSystem _gridSystem;
    private void Start()
    {
        _chessPieces = SpawnPieces.GetChessPieces();
        _gridSystem = ChessGrid.GetGridSystem();
    }

    // Move chess piece to desired position
    public static bool MoveTo(ChessPiece _chessPiece, int x, int y)
    {
        Vector2Int previousPosition = new Vector2Int(_chessPiece.currentX, _chessPiece.currentY);

        _chessPieces[x, y] = _chessPiece;
        _chessPieces[previousPosition.x, previousPosition.y] = null;

        PositionPieces.PositionSinglePiece(_chessPiece, x, y);
        return true;
    }

    // Get chess piece from where the mouse left click was made
    public static ChessPiece GetPiece(Vector3 dragPos)
    {
            GridPosition pickedGridPos = _gridSystem.GetGridPositionFromWorldPosition(new Vector3(dragPos.x, 0.5f, dragPos.z));
            Vector3 pickedWorldPos = _gridSystem.GetWorldPositionFromGridPosition(pickedGridPos);
            if(_chessPieces[(int) pickedWorldPos.x, (int) pickedWorldPos.z] != null)
            {
                return _chessPieces[(int) pickedWorldPos.x, (int) pickedWorldPos.z];
            }
            return null;
    }

    // Get exact tile position from where the mouse left click was stopped
    public static Vector3 GetPiecePos(Vector3 dropPos)
    {
        GridPosition droppedGridPos = _gridSystem.GetGridPositionFromWorldPosition(new Vector3(dropPos.x, 0.5f, dropPos.z));
        Vector3 droppedWorldPos = _gridSystem.GetWorldPositionFromGridPosition(droppedGridPos);
        return droppedWorldPos;
    }
}