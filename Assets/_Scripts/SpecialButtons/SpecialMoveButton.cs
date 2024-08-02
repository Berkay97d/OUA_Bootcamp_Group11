using System;
using _Scripts.Grid_System;
using ChessPieces;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.SpecialButtons
{
    public enum MoveType
    {
        Bishop,
        Knight
    }
    
    public class SpecialMoveButton : MonoBehaviour
    {
        [SerializeField] private MoveType _moveType;
        [SerializeField] private Button _fireButton;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _disableColor;
    
        public static event Action<bool,MoveType> OnSpecialMoveButtonClick;

        private bool m_isMoveSelected;
        private static bool m_isUsedCurrentIteration;

        private void Start()
        {
            _buttonImage.color = _disableColor;
            m_isMoveSelected = false;
        
            _fireButton.onClick.AddListener(OnFireButtonClic);
            
            ChessPieceMovement.OnChessPieceMove += OnChessPieceMove;
            IterationController.OnIterationCompleted += ResetSpecialMove;
            IterationController.OnIterationReset += ResetSpecialMove;
            IterationController.OnIterationCompletedWithKingLoss += ResetSpecialMove;
        }
        
        private void OnDestroy()
        {
            ChessPieceMovement.OnChessPieceMove -= OnChessPieceMove;
            IterationController.OnIterationCompleted -= ResetSpecialMove;
            IterationController.OnIterationReset -= ResetSpecialMove;
            IterationController.OnIterationCompletedWithKingLoss -= ResetSpecialMove;
        }
        
        private void ResetSpecialMove()
        {
            m_isUsedCurrentIteration = false;
        }

        private void OnChessPieceMove(UnitTurnData unitTurnData)
        {
            if (m_isMoveSelected)
            {
                m_isUsedCurrentIteration = true;
            }
            
            _buttonImage.color = _disableColor;
            m_isMoveSelected = false;
        }

        private void OnFireButtonClic()
        {
            Debug.Log("BUTTON TIKLANDIII");

            if (TurnController.SharedInstance.GetCurrentTeamTurn() == Team.Black)
            {
                Debug.Log("BLACK SPECİAL MOVE YAPAMAZ");
                return;
            }

            if (m_isUsedCurrentIteration)
            {
                return;
            }
            
            if (!m_isMoveSelected)
            {
                m_isMoveSelected = true;
                OnSpecialMoveButtonClick?.Invoke(m_isMoveSelected, _moveType);
            
                _buttonImage.color = Color.white;
            
                return;
            }

            _buttonImage.color = _disableColor;
        
            m_isMoveSelected = false;
            OnSpecialMoveButtonClick?.Invoke(m_isMoveSelected, _moveType);
        }
    }
}