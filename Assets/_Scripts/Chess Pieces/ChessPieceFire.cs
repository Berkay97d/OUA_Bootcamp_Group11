using System;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using InputSystem;
using TurnSystem;
using UnityEngine;

namespace ChessPieces
{
    public class ChessPieceFire : MonoBehaviour
    {
        private ChessPiece _chessPiece;
        private ChessPieceMovement _chessPieceMovement;
        public static event Action<ChessPiece, GridObject, bool> OnChessPieceFire;
        public static event Action<UnitTurnData> OnChessPieceShot;
        private List<GridObject> attackTiles = new List<GridObject>();
        private GridObject currentGridObject;

        private void Start()
        {
            _chessPiece = GetComponent<ChessPiece>();
            _chessPieceMovement = GetComponent<ChessPieceMovement>();
            GameInput.m_instance.OnFireInput += Fire;
            FireButton.OnFireButtonClick += FireButtonOnOnFireButtonClick;

            IterationController.OnIterationCompleted += CancelFireableTiles;
            IterationController.OnIterationReset += CancelFireableTiles;
            IterationController.OnIterationCompletedWithKingLoss += CancelFireableTiles;
        }

        private void OnDestroy()
        {
            GameInput.m_instance.OnFireInput -= Fire;
            FireButton.OnFireButtonClick -= FireButtonOnOnFireButtonClick;

            IterationController.OnIterationCompleted -= CancelFireableTiles;
            IterationController.OnIterationReset -= CancelFireableTiles;
            IterationController.OnIterationCompletedWithKingLoss -= CancelFireableTiles;
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
            OnChessPieceFire?.Invoke(_chessPiece, currentGridObject, false);
        }

        private void Fire()
        {
            if (!_chessPiece.GetTurn() || _chessPiece.GetPieceStatus() != 1) return;

            attackTiles = _chessPiece.GetAttackPattern();

            var selectedGridObject = GridObjectSelectionSystem.GetSelectedGridObject();

            if (attackTiles.Contains(selectedGridObject))
            {
                OnChessPieceFire?.Invoke(_chessPiece, currentGridObject, false);
                var turnData = new UnitTurnData(true, null, null, selectedGridObject, _chessPiece);
                OnChessPieceShot?.Invoke(turnData);
                _chessPiece.SetPieceStatus(0);
                _chessPiece.EndTurn();
                Debug.Log("ATEŞ ETTİM");

                var objs = FindObjectsOfType<King>();

                foreach (var king in objs)
                {
                    if (king.team == Team.White && king.GetGridPosition().Equals(selectedGridObject.GetGridPosition()))
                    {
                        Debug.Log("KRALI VURDUM");
                        _chessPieceMovement.RaiseOnKingLoss();
                        _chessPiece.SetPieceStatus(0);
                        return;
                    }
                    _chessPiece.SetPieceStatus(0);
                    Debug.Log("KRALI VURAMADIM");
                }

                return;
            }

            Debug.Log("ATEŞ ETMEK İÇİN SEÇTİĞİNİZ TİLE UYGUN DEĞİL");
        }

        public void FireByReplay(GridObject targetGrid)
        {
            var objs = FindObjectsOfType<King>();

            foreach (var king in objs)
            {
                if (king.team == Team.White && king.GetGridPosition().Equals(targetGrid.GetGridPosition()))
                {
                    Debug.Log("KRALI VURDUM");
                    _chessPieceMovement.RaiseOnKingLoss();
                    _chessPiece.SetPieceStatus(0);
                    return;
                }
                _chessPiece.SetPieceStatus(0);
                Debug.Log("KRALI VURAMADIM");
            }
        }
    }
}