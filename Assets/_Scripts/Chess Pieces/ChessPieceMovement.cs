using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using InputSystem;
using TurnSystem;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceMovement : MonoBehaviour
    {
        private static ChessPiece _chessPiece;
        private static GridSystem m_gridSystem;

        public static event Action<ChessPiece, GridObject, GridObject> OnChessPieceMove; 

        public static event Action<ChessPiece, GridObject, List<GridObject>> OnSpecialKingMove;

        public static event Action OnKingWin;

        private List<GridObject> movableGrids;
        private GridObject currentGridObject;

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            m_gridSystem = ChessGrid.GetGridSystem();
            GameInput.m_instance.OnMoveInput += Movement;

            currentGridObject = m_gridSystem.GetGridObject(_chessPiece.GetGridPosition());
        }

        // CREATED FOR TEST PURPOSES
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O) && _chessPiece.GetTurn() && !_chessPiece.GetPieceStatus()[0])
            {
                if(_chessPiece is King && _chessPiece.team == Team.White && !_chessPiece.GetPieceStatus()[1])
                {
                    _chessPiece.SetPieceStatus(true, 1);
                    _chessPiece.SetPieceStatus(false, 0);
                    movableGrids = _chessPiece.GetBishopPattern();
                    OnSpecialKingMove?.Invoke(_chessPiece, currentGridObject, movableGrids);
                }
            }

            else if(Input.GetKeyDown(KeyCode.P) && _chessPiece.GetTurn() && !_chessPiece.GetPieceStatus()[0])
            {
                if(_chessPiece is King && _chessPiece.team == Team.White && !_chessPiece.GetPieceStatus()[1])
                {
                    _chessPiece.SetPieceStatus(true, 1);
                    _chessPiece.SetPieceStatus(false, 0);
                    movableGrids = _chessPiece.GetKnightPattern();
                    OnSpecialKingMove?.Invoke(_chessPiece, currentGridObject, movableGrids);
                }
            }
        }

        private void Movement()
        {
            if(_chessPiece.GetTurn() && !_chessPiece.GetPieceStatus()[0])
            {
                GridObject selectedGridObject = GridObjectSelectionSystem.GetSelectedGridObject();
                if(!_chessPiece.GetPieceStatus()[1])
                    movableGrids = currentGridObject.GetMovableGrids();
                MoveTo(selectedGridObject, movableGrids);
            }
        }
        
        public void MoveTo(GridObject gridObject, List<GridObject> _movableGrids)
        {   
            if(_movableGrids.Contains(gridObject))
            {
                GridObject prevGridObject = currentGridObject;
                prevGridObject.SetIsOccupied(false);

                if(prevGridObject.GetVisitCount() >= 2)
                    prevGridObject.SetIsBroken(true);
                
                Vector3 movedPosition = m_gridSystem.GetWorldPositionFromGridPosition(gridObject.GetGridPosition());
                var movedGridPosition = m_gridSystem.GetGridPositionFromWorldPosition(movedPosition);
                var movedGridObject = m_gridSystem.GetGridObject(movedGridPosition);

                currentGridObject = movedGridObject;
                _chessPiece.SetPosition(movedPosition);
                gridObject.SetIsOccupied(true);

                _chessPiece.SetPieceStatus(false, 1);

                if (_chessPiece is King && movedGridPosition._z == 7)
                {
                    OnKingWin?.Invoke();
                }
                
                OnChessPieceMove?.Invoke(_chessPiece, prevGridObject, movedGridObject);
                _chessPiece.EndTurn();
            }
            else
                Debug.Log("MOVE FAILED");
        }
    }
}