using System;
using ChessPieces;
using UnityEngine;

namespace _Scripts
{
    public class IterationController : MonoBehaviour
    {
        public static event Action OnIterationCompleted;

        private void Start()
        {
            ChessPieceMovement.OnKingWin += OnKingWin;
        }

        private void OnDestroy()
        {
            ChessPieceMovement.OnKingWin -= OnKingWin;
        }

        private void OnKingWin()
        {
            OnIterationCompleted?.Invoke();
        }

       
    }
}