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
        _unrevealed.SetActive(_currentState == CellState.Unrevealed);
        _hoverBorders.SetActive(_currentState == CellState.Hover);
        _flag.SetActive(_currentState == CellState.Flagged);
        _mine.SetActive(_currentState == CellState.Revealed && _hasMine);
        _number.gameObject.SetActive(_currentState == CellState.Revealed);
    }

    private void RevealNearbyEmptyCells()
    {
        IEnumerable<Cell> adjacentCells = GetAdjacentCells();
        foreach (var cell in adjacentCells.Where(cell => !cell._hasMine && cell._currentState != CellState.Revealed))
        {
            cell.AssignNumber();
            cell.Reveal();
        }
    }
    

    private IEnumerable<Cell> GetAdjacentCells()
    {
        Vector2[] directions = {
            new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1),
            new Vector2Int(-1, 0),                          new Vector2Int(1, 0),
            new Vector2Int(-1, 1),  new Vector2Int(0, 1),  new Vector2Int(1, 1)
        };

        Vector2 currentPos = _gameManager.GetPosition(this);
        return directions
            .Select(dir => _gameManager.GetCellAtPosition(currentPos + dir))
            .Where(cell => cell != null);
    }


    private void AssignNumber()
    {
        IEnumerable<Cell> nearbyCells = GetAdjacentCells();
        int minesAmount = nearbyCells.Count(cell => cell._hasMine);
        if (minesAmount > 0)
            _number.SetNumber(minesAmount);
    }
}
