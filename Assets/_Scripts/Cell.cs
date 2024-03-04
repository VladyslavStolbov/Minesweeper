using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using UnityEngine;
using UnityEngine.XR;

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

    private GameManager _gameManager;
    private Board _board;
    private Vector2 _position;
    private State _currentState = State.Unclicked;
    private SpriteRenderer _spriteRenderer;
    private int _minesAround;
    private bool _isActive = true;
    private bool _isMined = false;
    
    private void Awake()
    {
        _board = GameObject.FindWithTag("Grid").GetComponent<Board>();
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
            _spriteRenderer.sprite = _mineSprite;
        }
        else if (_currentState == State.Flagged != _isMined)
        {
            _spriteRenderer.sprite = _flaggedWrongSprite;
        }
        else if (_currentState == State.Flagged && _isMined)
        {
            _spriteRenderer.sprite = _flaggedCorrectSprite;
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

    public bool IsMined()
    {
        return _isMined;
    }
    private void UpdateSprite()
    {
        switch (_currentState)
        {
            case State.Unclicked:
                _isActive = true;
                _spriteRenderer.sprite = _unclickedSprite;
                break;
            case State.Hovered:
                _spriteRenderer.sprite = _hoveredSprite;
                break;
            case State.Flagged:
                _isActive = false;
                _spriteRenderer.sprite = _flaggedSprite;
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

    private void HandleMinedState()
    {
        _isActive = false;
        _spriteRenderer.sprite = _mineActivatedSprite;
        _gameManager.LoseGame();
    }

    private void HandleClickedState()
    {
        _isActive = false;
        _minesAround = _board.CountMines(transform.localPosition);
    
        if (_minesAround == 0)
        {
            gameObject.SetActive(false);
            foreach (var cell in _board.GetNeighbours(transform.localPosition)
                         .Where(cell => cell.GetState() != State.Clicked))
            {
                cell.SetState(State.Clicked);
            }
        }
        else
        {
            _spriteRenderer.sprite = _numberSprites[_minesAround - 1];
        }
    }
}
