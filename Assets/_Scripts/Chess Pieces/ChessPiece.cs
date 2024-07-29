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
        private void Start()
        {
            m_gridSystem = ChessGrid.GetGridSystem();
        }
        public virtual List<GridObject> GetAttackPattern()
        {
            return null;
        }
        
    }
}