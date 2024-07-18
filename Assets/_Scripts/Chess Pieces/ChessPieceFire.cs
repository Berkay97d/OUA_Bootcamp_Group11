using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceFire : MonoBehaviour
    {
        private ChessPiece _chessPiece;


        private void Awake()
        {
            _chessPiece = GetComponent<ChessPiece>();
        }

        private void FixedUpdate()
        {
            if(Input.GetKeyDown(KeyCode.F))
                Fire();
        }

        public void Fire()
        {
            List<GridObject> attackTiles = _chessPiece.GetAttackPattern();
            
            for(int i = 0; i < attackTiles.Count; i++)
                if(attackTiles[i].GetIsOccupied())
                    Debug.Log("Chess Piece hit: " + attackTiles[i].GetGridPosition());
        }
    }
}