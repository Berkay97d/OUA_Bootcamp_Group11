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

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            GameInput.m_instance.OnFireInput += Fire;
        }

        // CREATED FOR TEST PURPOSES
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                GetFireableTiles();
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                CancelFireableTiles();
            }
        }

        private void GetFireableTiles()
        {
            var currentObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece, currentObject, true);
        }

        private void CancelFireableTiles()
        {
            var currentObject = ChessGrid.GetGridSystem().GetGridObject(_chessPiece.GetGridPosition());
            OnChessPieceFire?.Invoke(_chessPiece,  currentObject, false);
        }

        private void Fire()
        {
            if (!_chessPiece.GetTurn()) return;

            attackTiles = _chessPiece.GetAttackPattern();
            for (int i = 0; i < attackTiles.Count; i++)
            {
                Debug.Log("Chess Piece hit: " + attackTiles[i].GetGridPosition());
            }
            
            _chessPiece.EndTurn();
                    
        }
    }
}