using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override List<Vector2Int> GetAttackPattern(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        // Down
        for(int i = currentY - 1; i >= 0; i--)
        {
            r.Add(new Vector2Int(currentX, i));
        }

        // Up
        for(int i = currentY + 1; i < 8; i++)
        {
            r.Add(new Vector2Int(currentX, i));
        }

        // Left
        for(int i = currentX - 1; i <= 0; i--)
        {
            r.Add(new Vector2Int(i, currentY));
        }

        // Right
        for(int i = currentX + 1; i < 8; i++)
        {
            r.Add(new Vector2Int(i, currentY));
        }
       
        return r;
    }
}
