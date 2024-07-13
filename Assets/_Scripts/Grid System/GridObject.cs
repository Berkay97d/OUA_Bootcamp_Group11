using System;
using System.Collections.Generic;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>
    /// GridObject class holds GridSystem that grid object belongs,
    /// we should use GridObject class for our logical operations about grid.
    /// </para>
    /// </summary>
    public class GridObject
    {
        public event Action<bool> OnSelectedStatusChanged; 
        
        private readonly GridSystem m_gridSystem;
        private readonly GridPosition m_gridPosition;
        private bool m_isSelected;
        private bool m_isOccupied;
        
        
        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            m_gridSystem = gridSystem;
            m_gridPosition = gridPosition;
        }
        
        public GridPosition GetGridPosition()
        {
            return m_gridPosition;
        }

        public GridSystem GetGridSystem()
        {
            return m_gridSystem;
        }

        public void SetIsSelected(bool isSelected)
        {
            if (isSelected != m_isSelected)
            {
                OnSelectedStatusChanged?.Invoke(isSelected);
                m_isSelected = isSelected;
            }
        }

        public List<GridObject> GetNeighboorGrids()
        {
            return null;
        }

        public void SetIsOccupied(bool isOccupied)
        {
            m_isOccupied = isOccupied;
        }

        public bool GetIsOccupied()
        {
            return m_isOccupied;
        }

        public bool GetIsSelected()
        {
            return m_isSelected;
        }

        
        
    }
}