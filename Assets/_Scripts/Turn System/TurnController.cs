using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid_System;
using ChessPieces;
using UnityEngine;

namespace TurnSystem
{
    public class TurnController : MonoBehaviour
    {
        public static TurnController SharedInstance { get; private set; }
        private readonly List<Unit> _units = new();
        [SerializeField] private int _currentUnitIndex = 0;
        [SerializeField] private bool _kingWon;
        [SerializeField] private bool _isResettingTurn;
        [SerializeField] private DemoSpawnManager _demoSpawner;


        private void Awake()
        {
            if (SharedInstance == null)
            {
                SharedInstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                OnTurnReset();
            }
        }

        private void Start()
        {
            IterationController.OnIterationCompleted += OnIterationCompleted;
            var king = _demoSpawner.SpawnKing();
            _units.Add(king);
            StartTurn();
        }

        private void OnDestroy()
        {
            IterationController.OnIterationCompleted -= OnIterationCompleted;
        }

        private void OnIterationCompleted()
        {
            _kingWon = true;
            StartCoroutine(InnerRoutine());
            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(2f);
                foreach (var unit in _units)
                {
                    unit.GetComponent<ChessPieceVisual>().enabled = false;
                    unit.ReversePosition();
                }
            }
        }

        private void OnTurnReset()
        {
            StartCoroutine(InnerRoutine());
            IEnumerator InnerRoutine()
            {
                _isResettingTurn = true;
                _units[_currentUnitIndex].EndTurn();
                yield return new WaitForSeconds(2f);
                foreach (var unit in _units)
                {
                    unit.GetComponent<ChessPieceVisual>().enabled = false;
                    unit.ReversePosition();
                }
            }
        }

        public void StartTurn()
        {
            var currentUnit = _units[_currentUnitIndex];
            currentUnit.TakeTurn();
        }

        public void EndTurn()
        {
            if (!_kingWon && !_isResettingTurn)
            {
                _currentUnitIndex++;
                if (_currentUnitIndex < _units.Count)
                {
                    StartCoroutine(InnerRoutine());
                }
                else
                {
                    _currentUnitIndex = 0;
                    StartCoroutine(InnerRoutine());
                }
            }
            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(0.5f);
                StartTurn();
            }
        }

        public void ResetIteration()
        {
            StartCoroutine(InnerRoutine());
            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(0.5f);
                foreach (var unit in _units)
                {
                    unit.ResetGridPosition();
                    unit.GetComponent<ChessPieceVisual>().enabled = true;
                }
                yield return new WaitForSeconds(0.5f);
                _currentUnitIndex = 0;
                _isResettingTurn = false;
                StartTurn();
            }
        }

        private void AddEnemyUnit()
        {
            var blackUnit = _demoSpawner.SpawnBlackUnit();
            _units.Add(blackUnit);
        }

        public void UnitDidReset()
        {
            var allUnitsReset = true;
            foreach (var unit in _units)
            {
                if (!unit.DidCompleteReverse())
                {
                    allUnitsReset = false;
                }
            }

            if (allUnitsReset)
            {
                ResetIteration();
                if (_kingWon)
                {
                    AddEnemyUnit();
                    _kingWon = false;
                }
            }
        }
    }
}