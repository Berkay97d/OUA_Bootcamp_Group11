using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Grid_System;
using _Scripts.SpecialButtons;
using InputSystem;
using TurnSystem;
using UnityEngine;

namespace ChessPieces
{
    
    public class ChessPieceMovement : MonoBehaviour
    {
        private ChessPiece _chessPiece;
        private King _kingPiece;
        private static GridSystem m_gridSystem;

        public static event Action<ChessPiece, GridObject, GridObject> OnChessPieceMove; 

        public static event Action<ChessPiece, GridObject, List<GridObject>, bool> OnSpecialKingMove;

        public static event Action OnKingWin;
        public static event Action OnKingLoss;

        private List<GridObject> movableGrids;
        private GridObject currentGridObject;

    


        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            if(_chessPiece is King)
                _kingPiece = GetComponent<King>();
            m_gridSystem = ChessGrid.GetGridSystem();
            GameInput.m_instance.OnMoveInput += Movement;
            
            currentGridObject = m_gridSystem.GetGridObject(_chessPiece.GetGridPosition());
            SpecialMoveButton.OnSpecialMoveButtonClick += OnSpecialMove;
        }

        private void OnDestroy()
        {
            GameInput.m_instance.OnMoveInput -= Movement;
            SpecialMoveButton.OnSpecialMoveButtonClick -= OnSpecialMove;
        }

        private void OnSpecialMove(bool isActive, MoveType moveType)
        {
            if (TurnController.SharedInstance.GetCurrentTeamTurn() != Team.White)
            {
                return;
            }
            
            if (!isActive)
            {
                Debug.Log("NORMAL HAREKETE DÖN");
                _chessPiece.SetPieceStatus(0);
                movableGrids = currentGridObject.GetMovableGrids();
                OnSpecialKingMove?.Invoke(_chessPiece, currentGridObject, movableGrids, true);
                return;
            }

            if (moveType == MoveType.Bishop)
            {
                Debug.Log("BISHOP");
                _kingPiece.SetCanSpecialMove(false);
                _chessPiece.SetPieceStatus(2);
                movableGrids = _kingPiece.GetBishopPattern();
                OnSpecialKingMove?.Invoke(_chessPiece, currentGridObject, movableGrids, false);
                return;
            }
            
            Debug.Log("KNİGHT");
            _kingPiece.SetCanSpecialMove(false);
            _chessPiece.SetPieceStatus(2);
            movableGrids = _kingPiece.GetKnightPattern();
            OnSpecialKingMove?.Invoke(_chessPiece, currentGridObject, movableGrids, false);
        }

        // CREATED FOR TEST PURPOSES
      

        private void Movement()
        {
            if(_chessPiece.GetTurn() && _chessPiece.GetPieceStatus() != 1)
            {
                GridObject selectedGridObject = GridObjectSelectionSystem.GetSelectedGridObject();                
                if (_chessPiece.GetPieceStatus() != 2)
                {
                    currentGridObject = m_gridSystem.GetGridObject(_chessPiece.GetGridPosition());
                    movableGrids = currentGridObject.GetMovableGrids();    
                }
                MoveTo(selectedGridObject, movableGrids);
            }
        }
        

        public void MoveTo(GridObject gridObject, List<GridObject> _movableGrids)
        {   
         
            if(_movableGrids.Contains(gridObject)  && _chessPiece.GetTurn() )
            {
                GridObject prevGridObject = currentGridObject;
                prevGridObject.SetIsOccupied(false);

                
                
                Vector3 movedPosition = m_gridSystem.GetWorldPositionFromGridPosition(gridObject.GetGridPosition());
                var movedGridPosition = m_gridSystem.GetGridPositionFromWorldPosition(movedPosition);
                var movedGridObject = m_gridSystem.GetGridObject(movedGridPosition);


                currentGridObject = movedGridObject;
                _chessPiece.SetPosition(movedGridPosition);
                gridObject.SetIsOccupied(true);
                _chessPiece.SetPieceStatus(0);    
                OnChessPieceMove?.Invoke(_chessPiece, prevGridObject, movedGridObject);

                if (_chessPiece is King && movedGridPosition._z == 7)
                {
                    OnKingWin?.Invoke();
                }

                

                _chessPiece.EndTurn();
                
            }
            else
                Debug.Log("MOVE FAILED");
        }

        public void RaiseOnKingLoss()
        {
            OnKingLoss?.Invoke();
        }
    }
}