using System;
using ChessPieces;
using UnityEngine;

namespace _Scripts
{
    public class IterationController : MonoBehaviour
    {
        public static event Action OnIterationCompleted;
        public static event Action OnIterationReset;
        

        private void Start()
        {
            ChessPieceMovement.OnKingWin += OnKingWin;
            UnitReplayManager.OnKingWin += OnKingWin;
        }

        private void OnDestroy()
        {
            ChessPieceMovement.OnKingWin -= OnKingWin;
            UnitReplayManager.OnKingWin -= OnKingWin;
        }

        private void OnKingWin()
        {
            OnIterationCompleted?.Invoke();
        }

        public static void RaiseOnIterationReset()
        {
            OnIterationReset?.Invoke();
        }

    }
}