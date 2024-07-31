using System;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using InputSystem;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceFire : MonoBehaviour
    {
        private ChessPiece _chessPiece;
        public static event Action<ChessPiece, GridObject, bool> OnChessPieceFire;
        private List<GridObject> attackTiles = new List<GridObject>();
        private GridObject currentGridObject;

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            GameInput.m_instance.OnFireInput += Fire;
            FireButton.OnFireButtonClick += FireButtonOnOnFireButtonClick;

            IterationController.OnIterationCompleted += CancelFireableTiles;
            IterationController.OnIterationReset += CancelFireableTiles;
        }

        private void OnDestroy()
        {
            GameInput.m_instance.OnFireInput -= Fire;
            FireButton.OnFireButtonClick -= FireButtonOnOnFireButtonClick;
            
            IterationController.OnIterationCompleted -= CancelFireableTiles;
            IterationController.OnIterationReset -= CancelFireableTiles;
        }

        private void FireButtonOnOnFireButtonClick(bool isActive)
        {
            if (isActive)
            {
                GetFireableTiles();
                return;
            }
            
            CancelFireableTiles();
        }
        

        public void GetFireableTiles()
        {
            Debug.Log("FİRE ACTİVE");
            
            _chessPiece.SetPieceStatus(1);
            currentGridObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece, currentGridObject, true);
        }

        public void CancelFireableTiles()
        {
            Debug.Log("FİRE DİSABLED");
            
            _chessPiece.SetPieceStatus(0);
            currentGridObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece,  currentGridObject, false);
        }

        private void Fire()
        {
            if (!_chessPiece.GetTurn() || _chessPiece.GetPieceStatus() != 1) return;

            attackTiles = _chessPiece.GetAttackPattern();
            for (int i = 0; i < attackTiles.Count; i++)
            {
                Debug.Log("Chess Piece hit: " + attackTiles[i].GetGridPosition());
            }
            
            OnChessPieceFire?.Invoke(_chessPiece,  currentGridObject, false);
            _chessPiece.SetPieceStatus(0);
            _chessPiece.EndTurn();
                    
        }
    }
}