using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;
    
    [SerializeField] private Transform _cell;
    [SerializeField] private int _width = 9;
    [SerializeField] private int _height = 9;
    [SerializeField] private int _minesAmount = 10;
    [SerializeField] private float _cellSize = 1f;

    private GameManager _gameManager;
    private List<Cell> _cells;

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
        _gameManager = GameManager.Instance;
        _cells = new List<Cell>();
        Setup(_width, _height, _cellSize);
    }

    // Debug feature
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RevealBoard();
        }
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
                CreateCell(width, height, cellSize, col, row);
            }
        }
    }

    private void CreateCell(int width, int height, float cellSize, int col, int row)
    {
        Transform newCell = Instantiate(_cell, gameObject.transform, true);
        SetCellPosition(width, height, cellSize, col, row, newCell);
        _cells.Add(newCell.GetComponent<Cell>());
    }

    private static void SetCellPosition(int width, int height, float cellSize, int col, int row, Transform newCell)
    {
        float xPosition = col - (width - 1) / 2f;
        float yPosition = row - (height - 1) / 2f;
        newCell.localPosition = new Vector2(xPosition * cellSize, yPosition * cellSize);
    }

    private void AssignMines()
    {
        int minesPlaced = 0;
        while (minesPlaced != _minesAmount)
        {
            Cell cell = _cells[UnityEngine.Random.Range(0, _cells.Count)];
            if (cell.IsMined()) continue;
            cell.SetMine();
            minesPlaced++;
        }
    }

    public int CountMines(Vector2 location)
    {
        return GetNeighbours(location).Count(cell => cell.IsMined());
    }

    public List<Cell> GetNeighbours(Vector2 pos)
    {
        List<Cell> neighbours = new List<Cell>();

        foreach (Cell cell in _cells)
        {
            Vector2 cellPosition = cell.transform.position;
            float xDiff = Mathf.Abs(pos.x - cellPosition.x);
            float yDiff = Mathf.Abs(pos.y - cellPosition.y);

            // Check if the cell is adjacent in any direction (horizontal, vertical, or diagonal)
            if ((xDiff <= 1 && yDiff <= 1) && (xDiff + yDiff > 0 && xDiff + yDiff <= 2))
            {
                neighbours.Add(cell);
            }
        }

        return neighbours;
    }


    public void RevealBoard()
    {
        foreach (Cell cell in _cells)
        {
            cell.ShowGameOverState();
        }
    }

    public void ExpandBoard(Vector2 pos)
    {
        List<Cell> neighbours = GetNeighbours(pos);

        foreach (var neighbour in neighbours
                     .Where(neighbour => neighbour.GetState() == State.Unclicked && !neighbour.IsMined()))
        {
            neighbour.SetState(State.Clicked);
        }
    }

    public void CheckWinState()
    {
        int nonMinedCellsRevealed = _cells.Count(cell => !cell.IsMined() && cell.GetState() == State.Clicked);
        if (nonMinedCellsRevealed == _cells.Count - _minesAmount)
        {
            _gameManager.WinGame();
        }
    }
}