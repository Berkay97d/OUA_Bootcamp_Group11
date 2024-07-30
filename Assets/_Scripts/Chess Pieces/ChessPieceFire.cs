using System;
using System.Collections.Generic;
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
        }

        // CREATED FOR TEST PURPOSES
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z) && _chessPiece.GetTurn() && !_chessPiece.GetPieceStatus()[1])
            {
                GetFireableTiles();
            }

            if(Input.GetKeyDown(KeyCode.X) && _chessPiece.GetTurn() && _chessPiece.GetPieceStatus()[0])
            {
                CancelFireableTiles();
            }
        }

        private void GetFireableTiles()
        {
            _chessPiece.SetPieceStatus(true, 0);
            currentGridObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece, currentGridObject, true);
        }

        private void CancelFireableTiles()
        {
            _chessPiece.SetPieceStatus(false, 0);
            currentGridObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece,  currentGridObject, false);
        }

        private void Fire()
        {
            if (!_chessPiece.GetTurn() || !_chessPiece.GetPieceStatus()[0]) return;

            attackTiles = _chessPiece.GetAttackPattern();
            for (int i = 0; i < attackTiles.Count; i++)
            {
                Debug.Log("Chess Piece hit: " + attackTiles[i].GetGridPosition());
            }
            
            OnChessPieceFire?.Invoke(_chessPiece,  currentGridObject, false);
            _chessPiece.SetPieceStatus(false, 0);
            _chessPiece.EndTurn();
                    
        }
    }
}