using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private Image[] _ımages;
    [SerializeField] private Color _dis;
    
    
    private int iteration = 1;

    private void Start()
    {
        IterationController.OnIterationCompleted += On;
        IterationController.OnIterationCompletedWithKingLoss += On;
    }

    private void OnDestroy()
    {
        IterationController.OnIterationCompleted += On;
        IterationController.OnIterationCompletedWithKingLoss += On;
    }

    private void On()
    {
        iteration++;
        Adjust();
    }

    private void Adjust()
    {
        for (int i = 0; i < iteration; i++)
        {
            _ımages[i].gameObject.SetActive(true);
        }
        
        for (int i = 0; i < iteration-1; i++)
        {
            _ımages[i].color = _dis;
        }
        
        _ımages[iteration].color = Color.white;
    }
}
