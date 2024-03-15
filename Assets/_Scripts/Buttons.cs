using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Exit();
        }
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
