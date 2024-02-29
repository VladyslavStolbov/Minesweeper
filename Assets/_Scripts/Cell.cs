using System;
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
    private int _minesAround;
    private SpriteRenderer _spriteRenderer;
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
        if (_minesAround == 0) gameObject.SetActive(false);
        else _spriteRenderer.sprite = _numberSprites[_minesAround - 1];
       
    }
}
