using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameController _gameController;
    [SerializeField] private Text timerText;
    private float currentTime;
    private bool isPaused;
    
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
        isPaused = true;
        currentTime = 0f;
    }

    private void Update()
    {
        if (!isPaused) {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = ((int)time.TotalSeconds).ToString();
    }

    private void StartTimer() 
    {
        isPaused = false;
    }

    private void StopTimer()
    {
        isPaused = true;
    }
    
}
