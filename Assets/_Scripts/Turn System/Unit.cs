using System;
using System.Collections;
using _Scripts.Grid_System;
using UnityEngine;

namespace TurnSystem
{

    public class Unit : MonoBehaviour
    {
        
        [SerializeField] private int xPos;
        [SerializeField] private int zPos;
        
        private int _playOrder;
        private GridPosition m_myGridPosition;
        private MeshRenderer _renderer;
        private bool isPlayed = false;
        
        public Team team;
        
        
        private void Awake()
        {
            m_myGridPosition = new GridPosition(xPos, zPos);
            
            _renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void Update()
        {
            Debug.Log(gameObject.name + " " + _playOrder);
            
            if (TurnController.SharedInstance.GetCurrentUnitIndex() == _playOrder && !isPlayed)
            {
                isPlayed = true;
                StartCoroutine(EndTurn());
            }
        }

        public void TakeTurn()
        {
            Debug.Log(gameObject.name + " TAKE TURN");
            _renderer.material.color = Color.red;
        }

        private IEnumerator EndTurn()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(gameObject.name + " ENDED TURN");
            if (team == Team.White)
            {
                _renderer.material.color = Color.white;
            }
            else
            {
                _renderer.material.color = Color.black;
            }

            TurnController.SharedInstance.EndTurn();
            isPlayed = false;
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
    }
}