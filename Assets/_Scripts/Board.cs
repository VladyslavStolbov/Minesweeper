using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEditor;
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
            Cell cell = _cells[Random.Range(0, _cells.Count)];
            if (cell._isMined) continue;
            cell.SetMine();
            minesPlaced++;
        }
    }

    public int CountMines(Vector2 location)
    {
        return GetNeighbours(location).Count(cell => cell._isMined);
    }

    public IEnumerable<Cell> GetNeighbours(Vector2 pos)
    {
        return _cells.Where(cell =>
        {
            Vector2 cellPosition = cell.transform.position;
            float xDiff = Mathf.Abs(pos.x - cellPosition.x);
            float yDiff = Mathf.Abs(pos.y - cellPosition.y);

            // Check if the cell is adjacent in any direction (horizontal, vertical, or diagonal)
            return (xDiff <= 1 && yDiff <= 1) && (xDiff + yDiff > 0 && xDiff + yDiff <= 2);
        });
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
        IEnumerable<Cell> neighbours = GetNeighbours(pos);

        foreach (var neighbour in neighbours
                     .Where(neighbour => neighbour.GetState() == State.Unclicked && !neighbour._isMined))
        {
            neighbour.SetState(State.Clicked);
        }
    }

    public void CheckWinState()
    {
        int nonMinedCellsRevealed = _cells.Count(cell => !cell._isMined && cell.GetState() == State.Clicked);
        if (nonMinedCellsRevealed == _cells.Count - _minesAmount)
        {
            _gameManager.WinGame();
        }
    }

    public void ExpandIfFlagged(Cell cell)
    {
        IEnumerable<Cell> neighbours = GetNeighbours(cell.transform.localPosition);
        int flagCount = neighbours.Count(obj => obj.GetState() == State.Flagged);

        if (flagCount != CountMines(cell.transform.localPosition))
            return;

        foreach (Cell neighbour in neighbours)
        {
            if (neighbour.GetState() != State.Unclicked || neighbour._isMined)
                continue;

            neighbour.SetState(State.Clicked);

            if (CountMines(neighbour.transform.localPosition) == 0)
                ExpandBoard(neighbour.transform.localPosition);
        }
    }

}