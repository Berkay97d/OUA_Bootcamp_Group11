using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override List<Vector2Int> GetAttackPattern(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        r.Add(new Vector2Int(currentX - 1, currentY + 1));
        r.Add(new Vector2Int(currentX, currentY + 1));
        r.Add(new Vector2Int(currentX + 1, currentY + 1));
        r.Add(new Vector2Int(currentX - 1, currentY));
        r.Add(new Vector2Int(currentX + 1, currentY));
        r.Add(new Vector2Int(currentX - 1, currentY - 1));
        r.Add(new Vector2Int(currentX, currentY - 1));
        r.Add(new Vector2Int(currentX + 1, currentY - 1));
        
        return r;
    }
}
