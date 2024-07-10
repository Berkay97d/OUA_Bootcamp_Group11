using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController SharedInstance { get; private set; }
    private List<Unit> _units;
    private int _currentUnitIndex = 0;
    [SerializeField] private DemoSpawnManager _demoSpawner;

    private void Awake() 
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            _units = new List<Unit>();
            var kingUnit = _demoSpawner.SpawnKing();
            _units.Add(kingUnit);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        if (_currentUnitIndex == 0)
        {
            // Unit index 0 is for king, therefore white's turn
            Debug.Log("White team's turn.");
        }
        var currentUnit = _units[_currentUnitIndex];
        currentUnit.TakeTurn();
    }

    public void EndTurn()
    {
        if (_currentUnitIndex == 0)
        {
            // King unit just finished its turn, need to add a black unit
            // Find the play order of the new black unit
            var blackUnitPlayOrder = _units.Count;
            var blackUnit = _demoSpawner.SpawnBlackUnit(blackUnitPlayOrder);
            _units.Add(blackUnit);
            _currentUnitIndex++;
            Debug.Log("Black teams turn.");
        }
        else 
        {
            _currentUnitIndex++;
            if (_currentUnitIndex >= _units.Count)
            {
                _currentUnitIndex = 0;
            }
        }
        StartTurn();
    }
}
