using System;
using UnityEngine;

namespace InputSystem
{
    public class GameInput : MonoBehaviour
    {
        public event Action OnMoveInput;
        public event Action OnFireInput;
        
        private GameInputActions m_gameInputActions;
        
        
        private void Awake()
        {
            m_gameInputActions = new GameInputActions();
        }

        private void OnEnable()
        {
            m_gameInputActions.Enable();
        }

        private void OnDisable()
        {
            m_gameInputActions.Disable();
        }

        private void Update()
        {
            if (m_gameInputActions.Player.Move.WasPressedThisFrame())
            {
                OnMoveInput?.Invoke();
                Debug.Log("MOVE INPUT PRESSED");
            }
            
            if (m_gameInputActions.Player.Fire.WasPressedThisFrame())
            {
                OnFireInput?.Invoke();
                Debug.Log("FIRE INPUT PRESSED");
            }
        }
    }
}