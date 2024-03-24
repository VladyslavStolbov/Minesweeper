using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinesDisplay : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Board _board;
    private int _flagsAmount = 0;
    private int _minesLeft;
    
    private void Start()
    {
        _board = Board.Instance;
        UpdateMinesCount();
    }

    public void UpdateMinesCount()
    {
        _minesLeft = _board._minesAmount - _flagsAmount;
        _text.text = _minesLeft.ToString();
    }
}
