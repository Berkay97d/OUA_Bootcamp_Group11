using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class Knight : ChessPiece
    {
        public override List<GridObject> GetAttackPattern()
        {
            List<GridObject> attackTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            int[,] moveOffsets = new int[,]
            {
                { -1,  2 }, { 1,  2 },  // 2-up 1-left, 2-up 1-right
                { -2,  1 }, { 2,  1 },  // 1-up 2-left, 1-up 2-right
                { -2, -1 }, { 2, -1 },  // 1-down 2-left, 1-down 2-right
                { -1, -2 }, { 1, -2 }   // 2-down 1-left, 2-down 1-right
            };

            for (int i = 0; i < moveOffsets.GetLength(0); i++)
            {
                int newX = piecePos._x + moveOffsets[i, 0];
                int newZ = piecePos._z + moveOffsets[i, 1];
                if(newX >= 0 && newX < 8 && newZ >= 0 && newZ < 8)
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(newX, newZ)));
            }

            return attackTiles;
        }
    }
}