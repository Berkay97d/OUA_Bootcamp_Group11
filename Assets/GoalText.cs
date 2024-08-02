using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurnSystem;
using UnityEngine;

public class GoalText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnController.SharedInstance.GetCurrentTeamTurn() == Team.White)
        {
            text.text = "! GOAL !\nGO OF THE BOARD";
            return;
        }
        text.text = "! GOAL !\nKİLL WHİTE KİNG";
    }
}
