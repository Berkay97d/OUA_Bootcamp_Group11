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

            // 2-up 1-left, 2-up 1-right
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x - 1, piecePos._z + 2)));
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + 1, piecePos._z + 2)));
            
            // 1-up 2-left, 1-up 2-right
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x - 2, piecePos._z + 1)));
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + 2, piecePos._z + 1)));

            // 1-down 2-left, 1-down 2-right
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x - 2, piecePos._z - 1)));
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + 2, piecePos._z - 1)));

            // 2-down 1-left, 2-down 1-right
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x - 1, piecePos._z - 2)));
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + 1, piecePos._z - 2)));

            return attackTiles;
        }
    }
}