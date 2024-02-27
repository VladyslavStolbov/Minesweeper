using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Transform _cell;
    [SerializeField] private int _width = 9;
    [SerializeField] private int _height = 9;
    [SerializeField] private int _minesAmount = 10;
    [SerializeField] private float _cellSize = 1f;

    private List<Cell> _cells;
    
    private void Start()
    {
        _cells = new List<Cell>();
        Setup(_width, _height, _cellSize);
    }

    private void Setup(int width, int height, float cellSize)
    {
        CreateGrid(width, height, cellSize);
        AssignMines();
    }

    private void CreateGrid(int width, int height, float cellSize)
    {
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                Transform newCell = Instantiate(_cell, gameObject.transform, true);
                float xPosition = col - (width - 1) / 2f;
                float yPosition = row - (height - 1) / 2f;
                newCell.localPosition = new Vector2(xPosition * cellSize, yPosition * cellSize);
                _cells.Add(newCell.GetComponent<Cell>()); 
            }
        }
    }

    private void AssignMines()
    {
        int minesPlaced = 0;
        while (minesPlaced != _minesAmount)
        {
            Cell cell = _cells[Random.Range(0, _cells.Count)];
            if (cell.IsMined()) continue;
            cell.SetMine();
            minesPlaced++;
        }
    }
}
