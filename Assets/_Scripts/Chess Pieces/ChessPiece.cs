using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessPieceType
{
    None = 0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6 
}

public class ChessPiece : MonoBehaviour
{
    public int side;
    public int currentX;
    public int currentY;
    public ChessPieceType type;

    private Vector3 desiredPosition;
    private Vector3 desiredScale = new Vector3(0.5f, 0.5f, 0.5f);

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, Time.deltaTime * 10);
    }

    public List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        var allMoves = new int[,] {
            {currentX - 1, currentY + 1},
            {currentX, currentY + 1},
            {currentX + 1, currentY + 1},
            {currentX - 1, currentY},
            {currentX + 1, currentY},
            {currentX - 1, currentY - 1},
            {currentX - 1, currentY - 1},
            {currentX + 1, currentY - 1}
        };
        
        for(int i = 0; i < allMoves.Length; i++)
        {
            if(board[allMoves[i,0], allMoves[i,1]] == null)
            {
                r.Add(new Vector2Int(currentX, currentY + 1));
            }
        }

        return r;
    }

    public virtual void SetPosition(Vector3 position, bool force = false)
    {
        desiredPosition = position;
        if(force)
            transform.position = desiredPosition;
    }

    public virtual void SetScale(Vector3 scale, bool force = false)
    {
        desiredScale = scale;
        if(force)
            transform.localScale = desiredScale;
    }

}