using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController SharedInstance { get; private set; }
    private Unit _whiteUnit;
    private List<Unit> _blackUnits;
    private int _currentBlackUnitIndex = 0;
    private bool _isWhiteTurn = true;

    private void Awake() 
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            _blackUnits = new List<Unit>();
            var allUnits = GameObject.FindObjectsOfType<Unit>();
            foreach (var unit in allUnits)
            {
                if (unit.team == Team.White)
                {
                    _whiteUnit = unit;
                }
                else 
                {
                    _blackUnits.Add(unit);
                }
            }
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
        if (_isWhiteTurn)
        {
            Debug.Log("White team's turn.");
            _whiteUnit.TakeTurn();
        }
        else
        {
            if (_blackUnits.Count > 0)
            {
                var currentUnit = _blackUnits[_currentBlackUnitIndex];
                currentUnit.TakeTurn();
            }
        }
    }

    public void EndTurn()
    {
        if (_isWhiteTurn)
        {
            _isWhiteTurn = false;
            _currentBlackUnitIndex = 0;
            Debug.Log("Black teams turn.");
        }
        else 
        {
            _currentBlackUnitIndex++;
            if (_currentBlackUnitIndex >= _blackUnits.Count)
            {
                _isWhiteTurn = true;
            }
        }
        StartTurn();
    }
}
