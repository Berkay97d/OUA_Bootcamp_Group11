using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace TurnSystem
{


    public class TurnController : MonoBehaviour
    {
        public static TurnController SharedInstance { get; private set; }
        private List<Unit> _units = new ();
        private int _currentUnitIndex = 0;
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
            ResetIteration();
        }

        public void StartTurn()
        {
            if (_currentUnitIndex == 0)
            {
                // Unit index 0 is for king, therefore white's turn
                Debug.Log("White team's turn.");
            }

            if (_currentUnitIndex >= _units.Count)
            {
                _currentUnitIndex = 0;
            }
            
            var currentUnit = _units[_currentUnitIndex];
            
            currentUnit.TakeTurn();
        }

        public void EndTurn()
        {
            if (_currentUnitIndex < _units.Count)
            {
                _currentUnitIndex++;
                StartTurn();
                return;
            }

            if (_units.Count-1 == _currentUnitIndex)
            {
                _currentUnitIndex = 0;   
            }
            
            StartTurn();
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