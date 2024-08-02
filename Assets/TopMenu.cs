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
    
    
    private int iteration = 0;
    private int z;

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
        if (iteration == 1 || iteration == 3 || iteration == 5 || iteration == 7 || iteration == 9 || iteration == 11)
        {
            z++;
            for (int i = 0; i < z + 1; i++)
            {
                _ımages[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < z; i++)
            {
                _ımages[i].color = _dis;
            }

            _ımages[z].color = Color.white;
        }
    }
}
