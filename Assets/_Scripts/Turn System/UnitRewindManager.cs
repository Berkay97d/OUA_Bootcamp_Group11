using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace TurnSystem
{
    public class UnitRewindManager : MonoBehaviour
    {
        public List<GridObject> _previousGrids = new();
        private GridSystem m_gridSystem;
        private Unit _unit;
        private bool _rewindCompleted;

        private void Awake()
        {
            m_gridSystem = ChessGrid.GetGridSystem();
            _unit = GetComponent<Unit>();
        }

        private void OnEnable()
        {
            ChessPieceMovement.OnChessPieceMove += UnitDidMove;
        }

        private void OnDisable()
        {
            ChessPieceMovement.OnChessPieceMove -= UnitDidMove;
        }

        public void UnitDidMove(ChessPiece piece, GridObject prevGrid, GridObject nextGrid)
        {
            if (piece == _unit)
            {
                _previousGrids.Add(prevGrid);
            }
        }

        public void AddPosition(GridObject prevGrid)
        {
            _previousGrids.Add(prevGrid);
        }

        public void ReversePosition()
        {
            var gridObject = _previousGrids[_previousGrids.Count - 1];
            var worldPositionOfGrid = m_gridSystem.GetWorldPositionFromGridPosition(gridObject.GetGridPosition());
            transform.DOMove(worldPositionOfGrid, 0.5f, false).OnComplete(() =>
            {
                _previousGrids.Remove(gridObject);
                if (_previousGrids.Count == 0)
                {
                    _rewindCompleted = true;
                    TurnController.SharedInstance.UnitsDidResetPositions();
                }
                else
                {
                    ReversePosition();
                }
            });
        }

        public bool RewindCompleted()
        {
            return _rewindCompleted;
        }

        public void ResetRewindStatus()
        {
            _rewindCompleted = false;
        }
    }
}