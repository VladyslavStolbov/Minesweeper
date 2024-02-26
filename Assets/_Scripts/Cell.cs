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
    
    private State _currentState = State.Unclicked;
    private SpriteRenderer _spriteRenderer;
    private bool _isActive = true;
    private bool _isFlagged = false;
    private bool _isMined = false;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        SetState(State.Hovered);
        if (Input.GetMouseButton(0))
        {
            SetState(State.Clicked);
        }

        if (Input.GetMouseButton(1))
        {
            SetState(State.Flagged);
        }
    }

    private void OnMouseExit()
    {
        if (_isActive)
        {
            _spriteRenderer.sprite = _unclickedSprite;
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
                _spriteRenderer.sprite = _minedSprite;
                break;
            case State.Clicked:
                _isActive = false;
                gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
