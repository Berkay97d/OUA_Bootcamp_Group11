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
        private ChessPiece _chessPiece;
        private GridSystem m_gridSystem;

        public static event Action<ChessPiece, GridObject, GridObject> OnChessPieceMove; 

        public static event Action OnKingWin; 

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            m_gridSystem = ChessGrid.GetGridSystem();
            GameInput.m_instance.OnMoveInput += Movement;
        }

        private void Movement()
        {
            if(_chessPiece.GetTurn())
            {
                GridObject selectedGridObject = GridObjectSelectionSystem.GetSelectedGridObject();
                MoveTo(selectedGridObject);
            }
        }

        public List<GridObject> GetMovableGrids()
        {
            GridPosition gridPos = _chessPiece.GetGridPosition();
            GridObject myGridObject = m_gridSystem.GetGridObject(gridPos);

            List<GridObject> movableGrids = new List<GridObject>();
            var neighboorGrids = myGridObject.GetNeighboorGrids();

            for (int i = 0; i < neighboorGrids.Count; i++)
            {
                if (!neighboorGrids[i].GetIsOccupied())
                {
                    movableGrids.Add(neighboorGrids[i]);
                }
            }

            return movableGrids;
        }
        
        public void MoveTo(GridObject gridObject)
        {
            var _movableGrids = GetMovableGrids();
            
            if(_movableGrids.Contains(gridObject))
            {
                GridObject prevGridObject = m_gridSystem.GetGridObject(_chessPiece.GetGridPosition());
                if(prevGridObject.GetVisitCount() < 2)
                    prevGridObject.SetIsOccupied(false);
                
                Vector3 movedPosition = m_gridSystem.GetWorldPositionFromGridPosition(gridObject.GetGridPosition());
                var movedGridPosition = m_gridSystem.GetGridPositionFromWorldPosition(movedPosition);
                var movedGridObject = m_gridSystem.GetGridObject(movedGridPosition);
                _chessPiece.SetPosition(movedPosition);
                gridObject.SetIsOccupied(true);

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