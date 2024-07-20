using _Scripts.Grid_System;
using UnityEngine;

namespace TurnSystem
{
    public class Unit : MonoBehaviour
    {       
        [SerializeField] private int xPos;
        [SerializeField] private int zPos;
        private int _playOrder;
        private bool _hasTurn;
        private GridPosition m_myGridPosition;
        private MeshRenderer _renderer;
        public Team team;

        private int _moveCount;
        
        private void Awake()
        {
            _moveCount = 5;
            m_myGridPosition = new GridPosition(xPos, zPos);
            _renderer = GetComponentInChildren<MeshRenderer>();
            if (team == Team.White)
            {
                _renderer.material.color = Color.white;
            } else 
            {
                _renderer.material.color = Color.black;
            }
        }

        private void Update()
        {
            m_myGridPosition = new GridPosition(xPos, zPos);

            if (_hasTurn && _moveCount >0)
            {
                if (_moveCount == 0)
                {
                    EndTurn();
                }
            }
        }

        public void TakeTurn()
        {
            _moveCount = 5;
            _hasTurn = true;
            Debug.Log("${name}'s turn.");
            _renderer.material.color = Color.red;
        }

        public void EndTurn()
        {
            _hasTurn = false;
            Debug.Log("${name} ended its turn.");
            if (team == Team.White)
            {
                _renderer.material.color = Color.white;
            } else 
            {
                _renderer.material.color = Color.black;
            }
            TurnController.SharedInstance.EndTurn();
        }

        public void SetPlayOrder(int order)
        {
            _playOrder = order;
        }

        public GridPosition GetGridPosition()
        {
            return m_myGridPosition;
        }

        public void MoveInitPositionInstant()
        {
            transform.position = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(m_myGridPosition);
        }
        
        public void SetPosition(Vector3 movePos)
        {
            xPos = (int) movePos.x;
            zPos = (int) movePos.z;
        }

        public bool GetTurn()
        {
            return _hasTurn;
        }

    }
}