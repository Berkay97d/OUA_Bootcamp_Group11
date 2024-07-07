using System;
using _Scripts.Mouse_System;
using UnityEngine;

namespace _Scripts.Grid_System
{
    /// <summary>
    ///   <para>
    /// In ChessGrid class we create our grid system via GridSystem Object that we create,
    /// Don't reference this class, just use with static functions,
    /// We use this class to make outside operation with grid objects.
    /// </para>
    /// </summary>
    public class ChessGrid : MonoBehaviour
    {
        [SerializeField] private Transform _tilePrefabs;
        [SerializeField] private float _tileSize;

        private static ChessGrid ms_Instance;
        private const int GRID_WIDTH = 8;
        private const int GRID_HEIGHT = 8;
        private GridSystem m_gridSystem;


        private void Awake()
        {
            ms_Instance = this;
            
            //WE CREATE A GRID SYSTEM OBJECT AND THAT TRIGGERS THE CREATION OF GRID OBJECTS
            m_gridSystem = new GridSystem(GRID_WIDTH, GRID_HEIGHT, _tileSize);
            //CREATING VISUALS
            m_gridSystem.CreateTiles(_tilePrefabs);
        }
        
        //TODO REMOVE THIS CODE, ITS A TESTING CODE
        private void Update()
        {   
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UnityEngine.Debug.Log("SPACE PRESSED");
                if (m_gridSystem.GetGridObject(new GridPosition(0,0)).GetIsSelected())
                {
                    m_gridSystem.GetGridObject(new GridPosition(0,0)).SetIsSelected(false);
                    m_gridSystem.GetGridObject(new GridPosition(1,0)).SetIsSelected(true);
                    return;
                }
                
                if (!m_gridSystem.GetGridObject(new GridPosition(0,0)).GetIsSelected())
                {
                    m_gridSystem.GetGridObject(new GridPosition(0,0)).SetIsSelected(true);
                    m_gridSystem.GetGridObject(new GridPosition(1,0)).SetIsSelected(false);
                    return;
                }
                
            }
        }

        public static GridSystem GetGridSystem()
        {
            return ms_Instance.m_gridSystem;
        }
        
    }
}