using System.Collections;
using System.Collections.Generic;
using TurnSystem;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private GameObject _horse;
    [SerializeField] private GameObject _bishop;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnController.SharedInstance.GetCurrentTeamTurn() == Team.White)
        {
            _fire.SetActive(false);
            _horse.SetActive(true);
            _bishop.SetActive(true);
            return;
        }
        _fire.SetActive(true);
        _horse.SetActive(false);
        _bishop.SetActive(false);
        
    }
}
