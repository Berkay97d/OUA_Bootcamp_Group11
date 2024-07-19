using _Scripts.Grid_System;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceVisual : MonoBehaviour
    {
        private ChessPiece _chessPiece;
        private GridSystem m_gridSystem;

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            m_gridSystem = ChessGrid.GetGridSystem();
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, m_gridSystem.GetWorldPositionFromGridPosition(_chessPiece.GetGridPosition()), Time.deltaTime * 10);
        }

    }
}