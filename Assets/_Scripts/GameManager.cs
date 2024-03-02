using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Grid _grid;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    { 
        _grid = Grid.Instance; 
    }

    public void WinGame()
    {
        
    }
    
    public void LoseGame()
    {
        _grid.RevealBoard();
    }
}

