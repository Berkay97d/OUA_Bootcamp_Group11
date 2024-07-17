using System;
using System.Collections.Generic;
using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceMovement : MonoBehaviour
    {
        private ChessPiece _chessPiece;
        private Vector3 desiredPosition;


        private void Awake()
        {
            _chessPiece = GetComponent<ChessPiece>();
        }

        private void FixedUpdate()
        {
           // transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 10);
        }

        public List<GridObject> GetMovableGrids()
        {
            var gridPos = _chessPiece.GetGridPosition();
            var myGridObject = ChessGrid.GetGridSystem().GetGridObject(gridPos);

            var movableGrids = new List<GridObject>();
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
        
        public static bool MoveTo(GridObject gridObject)
        {
            return false;
        }
        
        
        public virtual void SetPosition(Vector3 position, bool force = false)
        {
            desiredPosition = position;
            if (force)
                transform.position = desiredPosition;
        }
    }
}