using System;
using _Scripts;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;
    
    private Board _board;
    public GameState _currentState { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    { 
        _board = Board.Instance;
        _currentState = GameState.Ongoing;
        OnGameStart.Invoke();
    }

    public void EndGame(GameState state)
    {
        _currentState = state;
        switch (_currentState)
        {
            case GameState.Ongoing:
                break;
            case GameState.Win:
                _board.RevealBoard();
                print("Win");
                break;
            case GameState.Lose:
                _board.RevealBoard();
                print("Lose");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        OnGameEnd.Invoke();
    }
}

