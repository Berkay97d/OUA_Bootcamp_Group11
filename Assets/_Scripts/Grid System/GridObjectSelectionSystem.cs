using System;
using _Scripts.Mouse_System;
using UnityEngine;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>
    /// This class calculate grid object that mouse hold over via mouse world and grid system
    /// </para>
    /// </summary>
    public class GridObjectSelectionSystem : MonoBehaviour
    {
        private static GridObjectSelectionSystem ms_Instance;
        private GridObject m_selectedGridObject;
        private GridSystem m_gridSystem;

        private void Awake()
        {
            ms_Instance = this;
        }

        private void Start()
        {
            m_gridSystem = ChessGrid.GetGridSystem();
            
            m_selectedGridObject = new GridObject(m_gridSystem, new GridPosition(0, 0));
        }

        private void FixedUpdate()
        {
            var info = MouseWorld.GetMouseMovementInfo();

            if (!info.IsHit && m_selectedGridObject!= null)
            {
                m_selectedGridObject.SetIsSelected(false);
                m_selectedGridObject = null;
                return;
            }
            
            if (!info.IsHit && m_selectedGridObject == null)
            {
                m_selectedGridObject = null;
                return;
            }
            
            var mousePos = MouseWorld.GetMousePosition();
            var gridPosition = m_gridSystem.GetGridPositionFromWorldPosition(mousePos);
            var gridObject = m_gridSystem.GetGridObject(gridPosition);

            if (gridObject == m_selectedGridObject) return;

            if (m_selectedGridObject != null) m_selectedGridObject.SetIsSelected(false);
            m_selectedGridObject = gridObject;
            m_selectedGridObject.SetIsSelected(true);
        }

        public static GridObject GetSelectedGridObject()
        {
            return ms_Instance.m_selectedGridObject;
        }
    }
}