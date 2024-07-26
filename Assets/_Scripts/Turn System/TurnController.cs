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
        private int _currentUnitIndex = 0;
        private bool _kingWon;
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

        public void StartTurn()
        {
            var currentUnit = _units[_currentUnitIndex];
            currentUnit.TakeTurn();
        }

        public void EndTurn()
        {
            if (!_kingWon)
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
                var blackUnitPlayOrder = _units.Count;
                var blackUnit = _demoSpawner.SpawnBlackUnit(blackUnitPlayOrder);
                _units.Add(blackUnit);
                yield return new WaitForSeconds(1);
                foreach (var unit in _units)
                {
                    unit.ResetGridPosition();
                    unit.GetComponent<ChessPieceVisual>().enabled = true;
                }
                _currentUnitIndex = 0;
                _kingWon = false;
                StartTurn();
            }
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
            }
        }
    }
}