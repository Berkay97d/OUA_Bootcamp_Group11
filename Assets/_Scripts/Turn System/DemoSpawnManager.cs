using System;
using _Scripts.Grid_System;
using UnityEngine;

namespace TurnSystem
{
    public class DemoSpawnManager : MonoBehaviour
    {
        [SerializeField] private Unit _whiteKingPrefab;
        [SerializeField] private Unit[] _enemyUnity;
        [SerializeField] private int _currentEnemyUnit = 0;

        public Unit SpawnKing()
        {
            var gridPosition = _whiteKingPrefab.GetGridPosition();
            var gridWorldPosition = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(gridPosition);
            var kingGameObject = Instantiate(_whiteKingPrefab, gridWorldPosition, Quaternion.identity);
            kingGameObject.team = Team.White;
            return kingGameObject;
        }

        public Unit SpawnBlackUnit()
        {
            var spawnUnit = _enemyUnity[_currentEnemyUnit];
            _currentEnemyUnit++;
            var blackGameObject = Instantiate(spawnUnit, spawnUnit.GetSpawnPosition(), Quaternion.identity);
            blackGameObject.team = Team.Black;
            return blackGameObject;
        }
    }
}