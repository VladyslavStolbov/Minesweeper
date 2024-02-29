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
    [SerializeField] private Sprite _minedSprite;
    [SerializeField] private Sprite[] _numberSprites;

    private Grid _grid;
    private Vector2 _position;
    private State _currentState = State.Unclicked;
    private SpriteRenderer _spriteRenderer;
    private int _minesAround;
    private bool _isActive = true;
    private bool _isMined = false;
    
    private void Awake()
    {
        _grid = GameObject.FindWithTag("Grid").GetComponent<Grid>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
    
    public void SetState(State newState)
    {
        _currentState = newState;
        UpdateSprite();
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
                _isActive = false;
                _spriteRenderer.sprite = _minedSprite;
                break;
            case State.Clicked:
                HandleClickedState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleClickedState()
    {
        _isActive = false;
        _minesAround = _grid.CountMines(transform.localPosition);
        if (_minesAround == 0)
        {
            Expand();
        }
        else
        {
            _spriteRenderer.sprite = _numberSprites[_minesAround - 1];
        }
       
    }

    private void Expand()
    {
        // Mark the cell as visited to prevent infinite recursion
        _isActive = false;
    
        // Hide the current cell
        gameObject.SetActive(false);
    
        // Get the neighboring cells
        List<Cell> neighbours = _grid.GetNeighbours(transform.localPosition);
    
        // Recursively reveal neighboring cells with no neighboring mines
        foreach (var neighbour in neighbours.Where(neighbour => neighbour._isActive))
        {
            neighbour.HandleClickedState();
        }
    }
}
