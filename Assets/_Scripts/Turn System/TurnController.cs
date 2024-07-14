using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

namespace TurnSystem
{
    public class TurnController : MonoBehaviour
    {
        public static TurnController SharedInstance { get; private set; }
        private List<Unit> _units = new();
        private int _currentUnitIndex = 0;
        [SerializeField] private DemoSpawnManager _demoSpawner;
        [SerializeField] private bool _hasCompleted = true;

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
            ResetIteration();
        }

        public void StartTurn()
        {
            var currentUnit = _units[_currentUnitIndex];
            currentUnit.TakeTurn();
        }

        public void EndTurn()
        {
            _currentUnitIndex++;
            if (_currentUnitIndex < _units.Count)
            {
                StartCoroutine(InnerRoutine());
            }
            else
            {
                if (_hasCompleted)
                {
                    Debug.Log("Press space to reset iteration");
                } else {
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
                Debug.Log("Adding a new unit.");
                yield return new WaitForSeconds(1);
                var blackUnitPlayOrder = _units.Count;
                var blackUnit = _demoSpawner.SpawnBlackUnit(blackUnitPlayOrder);
                _units.Add(blackUnit);
                yield return new WaitForSeconds(1);
                Debug.Log("Turn is being assigned to king.");
                _currentUnitIndex = 0;
                StartTurn();
            }
        }

        public int GetCurrentUnitIndex()
        {
            return _currentUnitIndex;
        }
    }
}