using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> GetAttackPattern(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        return base.GetAttackPattern(ref board, tileCountX, tileCountY);
    }
}
