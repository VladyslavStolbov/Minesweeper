using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameController _gameController;
    [SerializeField] private Text timerText;
    private float _currentTime;
    private bool _isPaused;
    
    private void OnEnable()
    {
       _gameController.OnGameStart.AddListener(StartTimer);
       _gameController.OnGameEnd.AddListener(StopTimer);
    }

    private void OnDisable()
    {
        _gameController.OnGameStart.RemoveListener(StartTimer);
        _gameController.OnGameEnd.RemoveListener(StopTimer);
    }
    
    private void Awake()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameController>();
        _isPaused = true;
        _currentTime = 0f;
    }

    private void Update()
    {
        if (!_isPaused) {
            _currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(_currentTime);
        timerText.text = ((int)time.TotalSeconds).ToString();
    }

    private void StartTimer() 
    {
        _isPaused = false;
    }

    private void StopTimer()
    {
        _isPaused = true;
    }
    
}
