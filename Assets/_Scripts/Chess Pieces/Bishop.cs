using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class Bishop : ChessPiece
    {
        public override List<GridObject> GetAttackPattern()
        {
            List<GridObject> attackTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            // Up-left
            for(int xLimit = piecePos._x - 1, zLimit = piecePos._z + 1; xLimit >= 0 && zLimit < 8; xLimit--, zLimit++)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + xLimit, piecePos._z + zLimit)));
            }

            // Up-right
            for(int xLimit = piecePos._x + 1, zLimit = piecePos._z + 1; xLimit < 8 && zLimit < 8; xLimit++, zLimit++)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + xLimit, piecePos._z + zLimit)));
            }

            // Down-left
            for(int xLimit = piecePos._x - 1, zLimit = piecePos._z - 1; xLimit >= 0 && zLimit >= 0; xLimit--, zLimit--)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + xLimit, piecePos._z + zLimit)));
            }

            // Down-right
            for(int xLimit = piecePos._x + 1, zLimit = piecePos._z - 1; xLimit < 0 && zLimit >= 8; xLimit++, zLimit--)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + xLimit, piecePos._z + zLimit)));
            }

            return attackTiles;
        }
    }
}