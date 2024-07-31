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
        private bool m_isOccupied = false;
        private bool m_isBroken = false;
        private int m_visitCount = 0;

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
            var neighboorGrids = new List<GridObject>();
            var initialPos = GetGridPosition();

            for(int x = initialPos._x - 1; x <= initialPos._x + 1; x++)
            {
                for(int z = initialPos._z - 1; z <= initialPos._z + 1;  z++)
                {
                    if(x >= 0 && x < 8 && z >= 0 && z < 8)
                        neighboorGrids.Add(m_gridSystem.GetGridObject(new GridPosition(x, z)));
                }
            }
            neighboorGrids.Remove(m_gridSystem.GetGridObject(new GridPosition(initialPos._x, initialPos._z)));

            return neighboorGrids;
        }

        public List<GridObject> GetMovableGrids()
        {
            List<GridObject> movableGrids = new List<GridObject>();
            var neighboorGrids = GetNeighboorGrids();

            for (int i = 0; i < neighboorGrids.Count; i++)
            {
                if (!neighboorGrids[i].GetIsOccupied() && !neighboorGrids[i].GetIsBroken())
                {
                    movableGrids.Add(neighboorGrids[i]);
                }
            }

            return movableGrids;
        }

        public void SetIsOccupied(bool isOccupied)
        {
            m_isOccupied = isOccupied;
            if(isOccupied)
                m_visitCount++;
        }

        public bool GetIsOccupied()
        {
            return m_isOccupied;
        }

        public void SetIsBroken(bool isBroken)
        {
            m_isBroken = isBroken;
        }

        public bool GetIsBroken()
        {
            return m_isBroken;
        }

        public bool GetIsSelected()
        {
            return m_isSelected;
        }

        public int GetVisitCount()
        {
            return m_visitCount;
        }
        
    }
}