using System;
using _Scripts.Grid_System;
using UnityEngine;

namespace TurnSystem
{
    public class DemoSpawnManager : MonoBehaviour
    {
        [SerializeField] private Unit _whiteKingPrefab;
        [SerializeField] private Unit[] _enemyUnity;

        
        public Unit SpawnKing()
        {
            var gridPosition = _whiteKingPrefab.GetGridPosition();
            var gridWorldPosition = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(gridPosition);
            
            var kingGameObject = Instantiate(_whiteKingPrefab, gridWorldPosition, Quaternion.identity);
            
            kingGameObject.team = Team.White;
            kingGameObject.SetPlayOrder(0);
            return kingGameObject;
        }

        public Unit SpawnBlackUnit(int playOrder)
        {
            var spawnUnit = _enemyUnity[playOrder - 1];
            var gridPosition = spawnUnit.GetGridPosition();
            var gridWorldPosition = ChessGrid.GetGridSystem().GetWorldPositionFromGridPosition(gridPosition);
            
            var blackGameObject = Instantiate(spawnUnit, gridWorldPosition, Quaternion.identity);
            blackGameObject.MoveInitPositionInstant();
            
            blackGameObject.team = Team.Black;
            blackGameObject.SetPlayOrder(playOrder);
            return blackGameObject;
        }
    }
}