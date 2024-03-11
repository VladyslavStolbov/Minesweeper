using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [Header("Sprites")] [SerializeField] private Sprite _unclickedSprite;
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
    private GameManager _gameManager;
    private Board _board;
    private State _currentState = State.Unclicked;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _board = Board.Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gameManager = GameManager.Instance;
    }

    private void OnMouseOver()
    {
        if (_isActive)
        {
            SetState(State.Hovered);
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
        _board.CheckWinState();
    }

    private void HandleRightClick()
    {
        if (_isActive)
        {
            SetState(State.Flagged);
        }

        else if (_currentState == State.Flagged)
        {
            SetState(State.Unclicked);
        }

 
    }
    
    private void HandleLeftClick()
    {
        if (!_isActive) return;
        SetState(_isMined ? State.Mined : State.Clicked);
        _board.CheckWinState();
    }

    private void OnMouseExit()
    {
        if (_isActive)
        {
            SetState(State.Unclicked);
        }
    }

    public State GetState()
    {
        return _currentState;
    }

    public void SetState(State newState)
    {
        _currentState = newState;
        UpdateSprite();

        if (newState == State.Clicked && !_isMined && _board.CountMines(transform.localPosition) == 0)
        {
            _board.ExpandBoard(transform.localPosition);
        }
    }

    public void ShowGameOverState()
    {
        _isActive = false;

        if (_currentState == State.Mined) return;

        if (_isMined && _currentState != State.Flagged)
        {
            SetSprite(_mineSprite);
        }

        else if (!_isMined && _currentState == State.Flagged)
        {
            SetSprite(_flaggedWrongSprite);
        }

        else if (_isMined && _currentState == State.Flagged)
        {
            SetSprite(_flaggedCorrectSprite);
        }

        else
        {
            SetState(State.Clicked);
        }
    }

    public void SetMine()
    {
        _isMined = true;
    }

    private void UpdateSprite()
    {
        switch (_currentState)
        {
            case State.Unclicked:
                HandleUnclickedState();
                break;
            case State.Hovered:
                HandleHoveredState();
                break;
            case State.Flagged:
                HandleFlaggedState();
                break;
            case State.Mined:
                HandleMinedState();
                break;
            case State.Clicked:
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
        _gameManager.LoseGame();
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
        foreach (var cell in GetUnclickedNeighbours())
        {
            cell.SetState(State.Clicked);
        }
    }

    private IEnumerable<Cell> GetUnclickedNeighbours()
    {
        return _board.GetNeighbours(transform.localPosition)
            .Where(cell => cell.GetState() != State.Clicked);
    }

    private void ShowNumberOnCell()
    {
        _spriteRenderer.sprite = _numberSprites[_minesAround - 1];
    }
}