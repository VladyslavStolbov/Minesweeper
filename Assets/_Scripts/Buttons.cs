using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
