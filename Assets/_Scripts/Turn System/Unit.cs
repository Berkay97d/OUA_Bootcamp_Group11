using System.Collections.Generic;
using _Scripts.Grid_System;
using ChessPieces;
using DG.Tweening;
using UnityEngine;

namespace TurnSystem
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int xPos;
        [SerializeField] private int zPos;
        public bool checkMate;
        private bool _hasTurn;
        private GridPosition m_myGridPosition;
        private MeshRenderer _renderer;
        public Team team;
        private UnitRewindManager _rewindManager;
        private UnitReplayManager _replayManager;

        private void Awake()
        {
            m_myGridPosition = new GridPosition(xPos, zPos);
            _rewindManager = GetComponent<UnitRewindManager>();
            _replayManager = GetComponent<UnitReplayManager>();
            _renderer = GetComponentInChildren<MeshRenderer>();
            GetComponent<ChessPieceVisual>().enabled = true;
        }

        public void TakeTurn()
        {
            if (TurnController.SharedInstance.GetCurrentTeamTurn() != team)
            {
                Debug.Log(gameObject.name);
                _replayManager.MoveToPosition();
                EndTurn();
            }
            else
            {
                _hasTurn = true;
            }
        }

        private void Update()
        {
            if (_hasTurn)
            {
                _renderer.material.color = Color.red;
            }
            else
            {
                UpdateTeamColors();
            }
        }

        public void EndTurn()
        {
            _hasTurn = false;
            TurnController.SharedInstance.TurnEndedByUnit();
        }

        public void ReversePosition()
        {
            _rewindManager.ReversePosition();
        }

        public GridPosition GetGridPosition()
        {
            return m_myGridPosition;
        }

        private void UpdateTeamColors()
        {
            if (team == Team.White)
            {
                _renderer.material.color = Color.white;
            }
            else
            {
                _renderer.material.color = Color.black;
            }
        }

        public void SetPosition(GridPosition newPosition)
        {
            m_myGridPosition = newPosition;
        }

        public void ResetGridPosition()
        {
            m_myGridPosition = new GridPosition(xPos, zPos);
        }

        public bool GetTurn()
        {
            return _hasTurn;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(xPos, transform.position.y, zPos);
        }
    }
}