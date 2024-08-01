using System;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using ChessPieces;
using TurnSystem;
using UnityEngine;

public class UnitReplayManager : MonoBehaviour
{
    public List<UnitTurnData> unitTurns = new();
    private Unit _unit;
    private int _turnIndex = 0;
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
        _turnIndex = 0;
    }

    private void OnEnable()
    {
        ChessPieceMovement.OnChessPieceMove += UnitDidMove;
        ChessPieceFire.OnChessPieceShot += UnitDidMove;
    }

    private void OnDisable()
    {
        ChessPieceFire.OnChessPieceShot -= UnitDidMove;
        ChessPieceMovement.OnChessPieceMove -= UnitDidMove;
    }

    public void UnitDidMove(UnitTurnData unitTurnData)
    {
        if (unitTurnData.chessPiece == _unit)
        {
            unitTurns.Add(unitTurnData);
        }
    }

    public void MoveToPosition()
    {

        if (unitTurns.Count > _turnIndex)
        {
            var turn = unitTurns[_turnIndex];

            if (turn.isFire)
            {
                var targetedGridObject = turn.shotGrid;
                _unit.GetComponent<ChessPieceFire>().FireByReplay(targetedGridObject);
            }
            else
            {
                _unit.GetComponent<UnitRewindManager>().AddPosition(turn.previousGrid);
                var gridObject = turn.targetGrid;
                _unit.SetPosition(gridObject.GetGridPosition());
                _turnIndex++;
                if (_unit is King && gridObject.GetGridPosition()._z == 7)
                {
                    OnKingWin?.Invoke();
                    _turnIndex = 0;
                }
            }
        }
    }

    public void ResetReplayData()
    {
        if (!isDone)
        {
            unitTurns.Clear();
        }
        _turnIndex = 0;
    }
}
