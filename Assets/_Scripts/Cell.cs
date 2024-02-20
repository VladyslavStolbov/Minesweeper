using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool _hasMine = false;

    private GameManager _gameManager;
    private CellState _currentState = CellState.Unrevealed;
    private bool _isActive = true;
    private Cell[] _cells;
    [SerializeField] private GridManger GridManger;
    [SerializeField] private GameObject _unrevealed;
    [SerializeField] private GameObject _hoverBorders;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Number _number;

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
        HandleMouseExit();
    }

    private void HandleMouseClicks()
    {
        if (Input.GetMouseButtonDown(0) && _isActive && _currentState != CellState.Flagged)
        {
            Reveal();
        }
        else if (Input.GetMouseButtonDown(1) && _isActive)
        {
            ToggleFlag();
        }
        else if (_isActive && _currentState != CellState.Flagged)
        {
            SetState(CellState.Hover);
        }
    }

    private void Reveal()
    {
        RevealNearbyEmptyCells();
        SetState(CellState.Revealed);
    }
    
    private void ToggleFlag()
    {
        SetState(_currentState == CellState.Flagged ? CellState.Unrevealed : CellState.Flagged);
    }

    private void HandleMouseExit()
    {
        if (_currentState == CellState.Revealed || _currentState == CellState.Flagged) return;
        SetState(CellState.Unrevealed);
    }


    private void SetState(CellState cellState)
    {
        _currentState = cellState;
        HandleVisualStates();
    }

    private void HandleVisualStates()
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
                _isActive = false;
                break;
            default:
                SetState(CellState.Unrevealed);
                break;
        }
    }

    private void RevealNearbyEmptyCells()
    {
        IEnumerable<Cell> nearbyCells = GetNearbyCells();
        foreach (var cell in nearbyCells.Where(cell => !cell._hasMine && cell._currentState != CellState.Revealed))
        {
            cell.AssignNumber();
        }
    }
    

    private IEnumerable<Cell> GetNearbyCells()
    {
        List<Cell> nearbyCells = new();
        List<Cell> availableCells = GridManger.GetAvailableCells();
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


    private void AssignNumber()
    {
        IEnumerable<Cell> nearbyCells = GetNearbyCells();
        int minesAmount = nearbyCells.Count(cell => cell._hasMine);
        if (minesAmount > 0)
            _number.SetNumber(minesAmount);
    }
}
