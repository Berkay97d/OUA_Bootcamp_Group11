using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public static TurnController SharedInstance { get; private set; }
    private List<Unit> _units;
    private int _currentUnitIndex = 0;
    [SerializeField] private DemoSpawnManager _demoSpawner;
    [SerializeField] private Button _iterationResetButton;

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
        _currentUnitIndex++;
        if (_currentUnitIndex < _units.Count)
        {
            StartTurn();
            _iterationResetButton.interactable = false;
        }
        else
        {
            _iterationResetButton.interactable = true;
        }
    }

    public void ResetIteration()
    {
        StartCoroutine(ResetCoroutine());
    }

    private IEnumerator ResetCoroutine()
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
