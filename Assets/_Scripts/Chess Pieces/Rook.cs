using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class Rook : ChessPiece
    {
        public override List<GridObject> GetAttackPattern()
        {
            List<GridObject> attackTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            var _gridObject = m_gridSystem.GetGridObject(piecePos);

            // Up
            for(int zLimit = piecePos._z + 1; zLimit < 8; zLimit++)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x, zLimit)));
            }

            // Down
            for(int zLimit = piecePos._z + -1; zLimit >= 0; zLimit--)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x, zLimit)));
            }

            // Left
            for(int xLimit = piecePos._x - 1; xLimit >= 0; xLimit--)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(xLimit, piecePos._z)));
            }

            // Right
            for(int xLimit = piecePos._x + 1; xLimit < 8; xLimit++)
            {
                attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(xLimit, piecePos._z)));
            }

            return attackTiles;
        }
    }
}