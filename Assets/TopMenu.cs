using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TurnSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopMenu : MonoBehaviour
{
    [SerializeField] private Image[] _ımages;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private Button _quit;
    [SerializeField] private Button _menu;
    
    
    private int z;
    
    private void Start()
    {
        
        DemoSpawnManager.OnNewUnitSpawn += OnNewUnitSpawn;
        IterationController.OnIterationCompleted += OnIterationCompleted;
        
        _quit.onClick.AddListener(Quit);
        _menu.onClick.AddListener(Menu);
    }

    private void Menu()
    {
        SceneManager.LoadScene(0);
    }

    private void Quit()
    {
        Application.Quit();
    }
    
    private void OnIterationCompleted()
    {
        IEnumerator InnerRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            
            if (z == 6)
            {
                _gameOverMenu.SetActive(true);
            }    
        }
        
    }

    private void OnNewUnitSpawn()
    {
        z++;
        Adjust();
    }

    private void OnDestroy()
    {
        DemoSpawnManager.OnNewUnitSpawn -= OnNewUnitSpawn;
    }
    

    private void Adjust()
    {
        _ımages[z].gameObject.SetActive(true);
    }
}
