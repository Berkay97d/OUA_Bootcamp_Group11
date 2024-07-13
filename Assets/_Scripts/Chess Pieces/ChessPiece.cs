using System;
using System.Collections.Generic;
using TurnSystem;
using UnityEngine;

namespace ChessPieces
{
    
    public abstract class ChessPiece : Unit
    {

        public virtual List<Vector2Int> GetAttackPattern(ref ChessPiece[,] board, int tileCountX, int tileCountY)
        {
            return null;
        }
        
    }
}