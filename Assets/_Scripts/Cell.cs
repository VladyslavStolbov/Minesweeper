using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool _hasMine = false;

    private GameManager _gameManager;
    private CellState _currentState = CellState.Unrevealed;
    private bool _isActive = true;
    private Cell[] _cells;
    [SerializeField] private GameObject _unrevealed;
    [SerializeField] private GameObject _hoverBorders;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _mine;

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    private void OnMouseOver()
    {
        HandleMouseClicks(); 
    }

    private void OnMouseExit()
    {
        if (_currentState == CellState.Revealed || _currentState == CellState.Flagged) return;
        SetState(CellState.Unrevealed);
    }
    private void HandleMouseClicks()
    {
        if (Input.GetMouseButtonDown(0) && _isActive && _currentState != CellState.Flagged)
        {
            RevealNearbyCells();
            SetState(CellState.Revealed);
        }
        else if (Input.GetMouseButtonDown(1) && _isActive)
        {
            SetState(_currentState == CellState.Flagged ? CellState.Unrevealed : CellState.Flagged);
        }
        else if (_isActive && _currentState != CellState.Flagged)
        {
            SetState(CellState.Hover);
        }
    }

    private void SetState(CellState cellState)
    {
        _currentState = cellState;
        HandleStates();
    }
    
    private void HandleStates()
    {
        switch (_currentState)
        {
            case CellState.Unrevealed:
                _unrevealed.SetActive(true);
                _hoverBorders.SetActive(false);
                _flag.SetActive(false);
                _isActive = true;
                break;
            case CellState.Hover:
                _hoverBorders.SetActive(true);
                break;
            case CellState.Flagged:
                _hoverBorders.SetActive(false);
                _flag.SetActive(true);
                break;
            case CellState.Revealed:
                _unrevealed.SetActive(false);
                _hoverBorders.SetActive(false);
                CheckForMine();
                _isActive = false;
                break;
            default:
                SetState(CellState.Unrevealed);
                break;
        }
    }

    private void CheckForMine()
    {
        if (_hasMine)
        {
            _mine.SetActive(true);
        }
    }

    private void RevealNearbyCells()
    {
        List <Cell> nearbyCells = GetNearbyCells();

        foreach (Cell cell in nearbyCells)
        {
            if (!cell._hasMine)
            {
                cell.SetState(CellState.Revealed);
            }
        }
    }
    

    private List<Cell> GetNearbyCells()
    {
        List<Cell> nearbyCells = new List<Cell>();
        List<Cell> availableCells = _gameManager.GetAvailableCells();
        Vector2 currentCellPosition = transform.position;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Skip the current cell
                if (x == 0 && y == 0)
                {
                    continue;
                }

                Vector2 nearbyCellPosition = currentCellPosition + new Vector2(x, y);

                foreach (var cell in availableCells)
                {
                    Vector2 cellPosition = cell.transform.position;

                    if (cellPosition != nearbyCellPosition) continue;
                    nearbyCells.Add(cell);
                    break;
                }
            }
        }
        return nearbyCells;
    }
}
