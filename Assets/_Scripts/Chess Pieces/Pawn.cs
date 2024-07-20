using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class Pawn : ChessPiece
    {
        public override List<GridObject> GetAttackPattern()
        {
            Debug.Log("ATTACK PARTTERN GELDİ PAWN");
            
            List<GridObject> attackTiles = new List<GridObject>();

            var piecePos = this.GetGridPosition();
            
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x + 1, piecePos._z + 1)));
            attackTiles.Add(m_gridSystem.GetGridObject(new GridPosition(piecePos._x - 1, piecePos._z + 1)));
            
            return attackTiles;
        }
    }
}