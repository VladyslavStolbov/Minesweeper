using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Guy : MonoBehaviour
{
    [Header("Sprites")] 
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _winSprite;
    [SerializeField] private Sprite _loseSprite;
    
    private GameController _gameController;
    private Image _image;
    private Animator _animator;
    
    private void Start()
    {
        _gameController = GameController.Instance;
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (_gameController._currentState)
        {
            case GameState.Ongoing:
                _image.sprite = _defaultSprite;
                break;
            case GameState.Win:
                _animator.Play("GuyFlex");
                break;
            case GameState.Lose:
                _animator.Play("GuySurprised");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
