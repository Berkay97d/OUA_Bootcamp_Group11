using System;
using _Scripts.Mouse_System;
using UnityEngine;

namespace _Scripts.Grid_System
{
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
        }

        private void Start()
        {
            m_gridSystem = new GridSystem(GRID_WIDTH, GRID_HEIGHT, _tileSize);
            m_gridSystem.CreateTiles(_tilePrefabs);
        }

        public GridSystem GetGridSystem()
        {
            return ms_Instance.m_gridSystem;
        }
        
    }
}