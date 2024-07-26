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
        private int _positionIndex = -1;
        public List<GridObject> _previousGrids = new();
        private GridSystem m_gridSystem;
        private Unit _unit;

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
                _positionIndex++;
            }
        }

        public void ReversePosition()
        {
            var gridObject = _previousGrids[_positionIndex];
            var worldPositionOfGrid = m_gridSystem.GetWorldPositionFromGridPosition(gridObject.GetGridPosition());
            transform.DOMove(worldPositionOfGrid, 0.5f, false).OnComplete(() =>
            {
                if (_positionIndex != 0)
                {
                    _positionIndex -= 1;
                    ReversePosition();
                }
                else
                {
                    _positionIndex = -1;
                    _previousGrids.Clear();
                    TurnController.SharedInstance.UnitsDidReset();
                }
            });
        }

        public int GetPreviodGridCount()
        {
            return _previousGrids.Count;
        }
    }
}