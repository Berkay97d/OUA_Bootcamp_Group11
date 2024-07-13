using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override List<Vector2Int> GetAttackPattern(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        // Up-right
        for(int i = currentX + 1, j = currentY + 1; i < 8 && j < 8; i++, j++)
        {
            r.Add(new Vector2Int(i, j));
        }

        // Up-left
        for(int i = currentX - 1, j = currentY + 1; i >= 0 && j < 8; i--, j++)
        {
            r.Add(new Vector2Int(i, j));
        }

        // Down-right
        for(int i = currentX + 1, j = currentY - 1; i < 8 && j >= 0; i++, j--)
        {
            r.Add(new Vector2Int(i, j));
        }

        // Down-left
        for(int i = currentX - 1, j = currentY - 1; i >= 0 && j >= 0; i--, j--)
        {
            r.Add(new Vector2Int(i, j));
        }

        return r;
    }
}
