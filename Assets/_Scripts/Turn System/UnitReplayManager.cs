using System;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using ChessPieces;
using TurnSystem;
using UnityEngine;

public class UnitReplayManager : MonoBehaviour
{
    public List<GridObject> _targetGrids = new();
    private Unit _unit;
    private int _positionIndex = 0;
    private GridSystem m_gridSystem;
    public static event Action OnKingWin;
    public bool isDone;

    private void Awake()
    {
        m_gridSystem = ChessGrid.GetGridSystem();
        _unit = GetComponent<Unit>();
    }

    private void Start()
    {
        IterationController.OnIterationReset += OnIterationReset;
        IterationController.OnIterationCompleted += OnIterationReset;
        IterationController.OnIterationCompletedWithKingLoss += OnIterationReset;
    }

    private void OnDestroy()
    {
        IterationController.OnIterationReset -= OnIterationReset;
        IterationController.OnIterationCompleted -= OnIterationReset;
        IterationController.OnIterationCompletedWithKingLoss -= OnIterationReset;
    }

    private void OnIterationReset()
    {
        _positionIndex = 0;
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
            _targetGrids.Add(nextGrid);
        }
    }

    public void MoveToPosition()
    {
        var currentGridPosition = m_gridSystem.GetGridPositionFromWorldPosition(transform.position);
        var currentGridObject = m_gridSystem.GetGridObject(currentGridPosition);
        _unit.GetComponent<UnitRewindManager>().AddPosition(currentGridObject);
        if (_targetGrids.Count > _positionIndex)
        {
            var gridObject = _targetGrids[_positionIndex];
            _unit.SetPosition(gridObject.GetGridPosition());
            _positionIndex++;
            if (_unit is King && gridObject.GetGridPosition()._z == 7)
            {
                OnKingWin?.Invoke();
                _positionIndex = 0;
            }
        }

    }

    public void ResetReplayData()
    {
        if (!isDone)
        {
            _targetGrids.Clear();
        }
        _positionIndex = 0;
    }
}
