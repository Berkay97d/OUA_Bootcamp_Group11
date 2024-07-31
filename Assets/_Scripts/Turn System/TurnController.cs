using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private DemoSpawnManager _demoSpawner;
        [SerializeField] private bool isWhite;

        // Şu an sıranın hangi takımda olduğunu gösteren bir flag
        private Team _currentTeamTurn;
        private bool _isResetManual;


        private void Start()
        {
            if (SharedInstance == null)
            {
                SharedInstance = this;
                _currentTeamTurn = Team.White;
                var king = _demoSpawner.SpawnKing();
                _units.Add(king);
                GiveTurnToUnit();
            }
            else
            {
                Destroy(gameObject);
            }
            
            IterationController.OnIterationReset += OnIterationReset;
        }

        
        
        private void OnIterationReset()
        {
            _isResetManual = true;
            ResetUnitPositions();
        }

        private void OnEnable()
        {
            IterationController.OnIterationCompleted += KingWon;
        }

        private void OnDisable()
        {
            IterationController.OnIterationCompleted -= KingWon;
        }

        private void Update()
        {
            Debug.Log(_currentTeamTurn);
            
            if (Input.GetKeyDown(KeyCode.J))
            {
                _kingWon = false;
                StartCoroutine(InnerRoutine());
                IEnumerator InnerRoutine()
                {
                    // Need to wait 1 second so that the last move of the king is visible
                    // To get rid of this, the event OnKingWin should be called when the king is AT the last row, not when last row is clicked
                    yield return new WaitForSeconds(1f);
                    ResetUnitPositions();
                }   
            }
        }

        // Called when King unit of White team has reached the last row of the grid.
        // Called by the ChessPieceMovement when the player has moved the last row as King
        // Called by the UnitReplayManager when the replay of King's moves has reached the last row of the grid
        private void KingWon()
        {
            _kingWon = true;
            StartCoroutine(InnerRoutine());
            IEnumerator InnerRoutine()
            {
                // Need to wait 1 second so that the last move of the king is visible
                // To get rid of this, the event OnKingWin should be called when the king is AT the last row, not when last row is clicked
                yield return new WaitForSeconds(1f);
                ResetUnitPositions();
            }
        }

        // Gives the turn to the current unit in the order, order is determined by "_currentUnitIndex".
        public void GiveTurnToUnit()
        {
            
            var currentUnit = _units[_currentUnitIndex];
            currentUnit.TakeTurn();
            /*StartCoroutine(InnerRoutine());
            
            IEnumerator InnerRoutine()
            {
                yield return new WaitForSeconds(1f);
                
            }*/
        }

        // The unit which had the turn has made their move. If the game state allows it, (meaning that this move just ended did not end the current iteration) give the turn to the next unit in order.
        public void TurnEndedByUnit()
        {
            if (!_kingWon)
            {
                _currentUnitIndex = (_currentUnitIndex + 1) % _units.Count;
                GiveTurnToUnit();
            }
        }

        // The current iteration has reached an end, and all units go back to their first positions.
        public void ResetUnitPositions()
        {
            foreach (var unit in _units)
            {
                // ChessPieceVisual cancels the rewind movement, need to be disabled during the rewind phase.
                unit.GetComponent<ChessPieceVisual>().enabled = false;
                unit.ReversePosition();
            }
        }

        // Called by each unit which has completed the rewind phase. In order to proceed, this makes sure that all 
        // units completed their rewind phase.
        public void UnitsDidResetPositions()
        {
            var allUnitsReset = true;
            foreach (var unit in _units)
            {
                if (!unit.GetComponent<UnitRewindManager>().RewindCompleted())
                {
                    allUnitsReset = false;
                }
            }

            if (allUnitsReset)
            {
                foreach (var unit in _units)
                {
                    unit.GetComponent<UnitRewindManager>().ResetRewindStatus();
                }
                ResetIteration();
            }
        }

        // This is the method used to reset the current iteration. Here, we check the game state and reset everything accordingly.
        public void ResetIteration()
        {
            foreach (var unit in _units)
            {
                // Need to reset the gridPositions to initial so that "ChessPieceVisual" do not move the units to their 
                // last grid they have been to 
                unit.ResetGridPosition();
                // Enabling the ChessPieceVisual after completing the rewind and resetting gridPosition.
                unit.GetComponent<ChessPieceVisual>().enabled = true;
            }

            if (_isResetManual)
            {
                _currentUnitIndex = 0;
                GiveTurnToUnit();

                if (_currentTeamTurn == Team.White)
                {
                    ResetReplayDataOfKing();
                }
                else
                {
                    ResetReplayDataOfEnemyUnits();
                }
                
                _isResetManual = false;
                return;
            }

            if (_currentTeamTurn == Team.White) // The player has played as king in this iteration.
            {
                if (_kingWon) // The player has successfully reached the last row of the grid as King
                {
                    AddEnemyUnit(); // Add an enemy unit
                    _currentUnitIndex = 0; // Give turn back to king, which now will be played by Replay.
                    _currentTeamTurn = Team.Black; // Iteration was successful for player, so switch teams.
                    ResetReplayDataOfEnemyUnits(); // Reset the replay of enemy units, since new moves will be recorded
                    _kingWon = false; // reset king's win flag.
                }
                else // The player could not reach the last row of the grid as White King.
                {
                    _currentUnitIndex = 0; // Give turn back to king, which now will be played by Player again.
                    ResetReplayDataOfKing(); // Reset the replay of king, since new moves will be recorded
                }
            }
            else // The player has played as black units in this iteration.
            {
                Debug.Log("KİNG WONNNNN: "+_kingWon);
                if (_kingWon) // The player has failed to stop the King from reaching last grid
                {
                    _currentUnitIndex = 0; // Give turn back to king, which now will be played by Replay.
                    ResetReplayDataOfEnemyUnits(); // Reset the replay of enemy units, since new moves will be recorded
                    _kingWon = false;
                }
                else // The player has successfully stopped the King from reaching the last row of the grid
                {
                    _currentUnitIndex = 0; // Give turn back to king, which now will be played by Player again.
                    ResetReplayDataOfKing(); // Reset the replay of king, since new moves will be recorded
                    _currentTeamTurn = Team.White; // Iteration was successful for player, so switch teams. 
                }
            }
            GiveTurnToUnit(); // Start the current iteration       
        }

        private void AddEnemyUnit()
        {
            var blackUnit = _demoSpawner.SpawnBlackUnit();
            _units.Add(blackUnit);
        }

        public Team GetCurrentTeamTurn()
        {
            return _currentTeamTurn;
        }

        private void ResetReplayDataOfKing()
        {
            var king = _units[0];
            king.GetComponent<UnitReplayManager>().ResetReplayData();
        }

        private void ResetReplayDataOfEnemyUnits()
        {
            for (var i = 1; i < _units.Count; i++)
            {
                var blackUnit = _units[i];
                blackUnit.GetComponent<UnitReplayManager>().ResetReplayData();
            }
        }

       
    }
}