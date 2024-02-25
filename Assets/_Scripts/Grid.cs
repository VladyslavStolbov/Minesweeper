using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Transform _cell;

    private int _width = 9;
    private int _height = 9;
    private float _cellSize = 1f;
    
    private void Start()
    {
        Setup(_width, _height, _cellSize);
    }

    public void Setup(int width, int height, float cellSize)
    {
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                Transform newCell = Instantiate(_cell, gameObject.transform, true);
                float xPosition = col - (width - 1) / 2f;
                float yPosition = row - (height - 1) / 2f;
                newCell.localPosition = new Vector2(xPosition * cellSize, yPosition * cellSize);
            }
        }
    }
}
