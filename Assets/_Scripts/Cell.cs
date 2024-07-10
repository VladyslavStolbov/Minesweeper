using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Sprites")] 
    [SerializeField] private Sprite _unclickedSprite;
    [SerializeField] private Sprite _hoveredSprite;
    [SerializeField] private Sprite _flaggedSprite;
    [SerializeField] private Sprite _flaggedCorrectSprite;
    [SerializeField] private Sprite _flaggedWrongSprite;
    [SerializeField] private Sprite _mineSprite;
    [SerializeField] private Sprite _mineActivatedSprite;
    [SerializeField] private Sprite[] _numberSprites;

    public bool _isMined { get; private set; }
    private bool _isActive = true;
    private int _minesAround;
    private GameController _gameController;
    private Board _board;
    private CellState _currentCellState = CellState.Unclicked;
    private SpriteRenderer _spriteRenderer;
    private MinesDisplay _minesDisplay;
    private Animator _animator;
    
    private void Awake()
    {
        _board = Board.Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = GameController.Instance;
        _minesDisplay = GameObject.FindGameObjectWithTag("UI/MinesDisplay").GetComponent<MinesDisplay>();
        _animator = GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        if (_isActive)
        {
            SetState(CellState.Hovered);
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftClick();
        }

        else if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }
        
        else if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            _board.ExpandIfFlagged(this);
        }
    }

    private void HandleRightClick()
    {
        if (_isActive && _board.CountFlaggedCells() < _board._minesAmount)
        {
            SetState(CellState.Flagged);
        }

        else if (_currentCellState == CellState.Flagged)
        {
            SetState(CellState.Unclicked);
        }
        _minesDisplay.UpdateMinesCount();
    }
    
    private void HandleLeftClick()
    {
        if (!_isActive) _board.ExpandIfFlagged(this);
        SetState(_isMined ? CellState.Mined : CellState.Clicked);
    }

    private void OnMouseExit()
    {
        if (_isActive)
        {
            SetState(CellState.Unclicked);
        }
    }

    public CellState GetState()
    {
        return _currentCellState;
    }

    public void SetState(CellState newCellState)
    {
        _currentCellState = newCellState;
        UpdateSprite();

        if (newCellState == CellState.Clicked && !_isMined && _board.CountMines(transform.localPosition) == 0)
        {
            _board.ExpandBoard(transform.localPosition);
        }
    }

    public void ShowGameOverState()
    {
        _isActive = false;

        if (_currentCellState == CellState.Mined) return;

        if (_isMined && _currentCellState != CellState.Flagged)
        {
            SetSprite(_mineSprite);
        }

        else if (!_isMined && _currentCellState == CellState.Flagged)
        {
            SetSprite(_flaggedWrongSprite);
        }

        else if (_isMined && _currentCellState == CellState.Flagged)
        {
            SetSprite(_flaggedCorrectSprite);
        }

        else
        {
            SetState(CellState.Clicked);
        }
    }

    public void SetMine()
    {
        _isMined = true;
    }

    private void UpdateSprite()
    {
        switch (_currentCellState)
        {
            case CellState.Unclicked:
                HandleUnclickedState();
                break;
            case CellState.Hovered:
                HandleHoveredState();
                break;
            case CellState.Flagged:
                HandleFlaggedState();
                break;
            case CellState.Mined:
                HandleMinedState();
                break;
            case CellState.Clicked:
                HandleClickedState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleUnclickedState()
    {
        _isActive = true;
        SetSprite(_unclickedSprite);
    }

    private void HandleHoveredState()
    {
        SetSprite(_hoveredSprite);
    }

    private void HandleFlaggedState()
    {
        _isActive = false;
        SetSprite(_flaggedSprite);
    }

    private void HandleMinedState()
    {
        _isActive = false;
        SetSprite(_mineActivatedSprite);
        _animator.enabled = true;
        _animator.Play("MineExplosion");
        _gameController.EndGame(GameState.Lose);
    }

    private void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    private void HandleClickedState()
    {
        _isActive = false;
        _minesAround = CountMinesAroundCurrentCell();

        if (_minesAround == 0)
        {
            DeactivateCellAndNeighbours();
        }
        else
        {
            ShowNumberOnCell();
        }
    }
    
    private int CountMinesAroundCurrentCell()
    {
        return _board.CountMines(transform.localPosition);
    }

    private void DeactivateCellAndNeighbours()
    {
        gameObject.SetActive(false);
        foreach (Cell cell in GetUnclickedNeighbours())
        {
            cell.SetState(CellState.Clicked);
        }
    }

    private IEnumerable<Cell> GetUnclickedNeighbours()
    {
        return _board.GetNeighbours(transform.localPosition)
            .Where(cell => cell.GetState() != CellState.Clicked);
    }

    private void ShowNumberOnCell()
    {
        _spriteRenderer.sprite = _numberSprites[_minesAround - 1];
    }
}