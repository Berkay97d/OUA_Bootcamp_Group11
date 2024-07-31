using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using TurnSystem;
using UnityEngine;

namespace ChessPieces
{
    
    public abstract class ChessPiece : Unit
    {
        protected GridSystem m_gridSystem;
        protected int pieceStatus = 0;

        private void Start()
        {
            m_gridSystem = ChessGrid.GetGridSystem();
        }

        public void SetPieceStatus(int status)
        {
            pieceStatus = status;
        }
        public int GetPieceStatus()
        {
            return pieceStatus;
        }

        public virtual List<GridObject> GetAttackPattern()
        {
            return null;
        }

        public virtual List<GridObject> GetBishopPattern()
        {
            return null;
        }

        public virtual List<GridObject> GetKnightPattern()
        {
            return null;
        }
    }
}