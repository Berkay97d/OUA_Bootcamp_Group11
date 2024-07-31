using System;
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

        private void Start()
        {
            _buttonImage.color = _disableColor;
            m_isMoveSelected = false;
        
            _fireButton.onClick.AddListener(OnFireButtonClic);
        }

        private void OnFireButtonClic()
        {
            Debug.Log("BUTTON TIKLANDIII");
        
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