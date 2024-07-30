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
        protected bool[] pieceStatus = new bool[] { false, false };

        private void Start()
        {
            m_gridSystem = ChessGrid.GetGridSystem();
        }

        public void SetPieceStatus(bool status, int index)
        {
            pieceStatus[index] = status;
        }
        public bool[] GetPieceStatus()
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