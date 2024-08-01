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
        }

        private void Start()
        {
            //CREATING VISUALS
            m_gridSystem.CreateTiles(_tilePrefabs);
            IterationController.OnIterationCompleted += OnIterationCompleted;
            IterationController.OnIterationReset += OnIterationCompleted;
            IterationController.OnIterationCompletedWithKingLoss += OnIterationCompleted;
        }

        private void OnDestroy()
        {
            IterationController.OnIterationCompleted -= OnIterationCompleted;
            IterationController.OnIterationReset -= OnIterationCompleted;
            IterationController.OnIterationCompletedWithKingLoss -= OnIterationCompleted;
        }

        private void OnIterationCompleted()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    m_gridSystem.GetAllGridObjects()[i,j].SetIsOccupied(false);
                    Debug.Log(i + " " + j + " reset");
                }
            }
        }


        public static GridSystem GetGridSystem()
        {
            return ms_Instance.m_gridSystem;
        }
        
    }
}