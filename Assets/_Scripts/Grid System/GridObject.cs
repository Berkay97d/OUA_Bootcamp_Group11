namespace _Scripts.Grid_System
{
    public class GridObject
    {
        private readonly GridSystem m_gridSystem;
        private readonly GridPosition m_gridPosition;
        
        
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

        
        
    }
}