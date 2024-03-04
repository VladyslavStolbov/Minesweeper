using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Board _board;
    
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
        _board = Board.Instance; 
    }

    public void WinGame()
    {
        
    }
    
    public void LoseGame()
    {
        _board.RevealBoard();
    }
}

