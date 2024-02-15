using System;
using UnityEngine;
using UnityEngine.tvOS;

public enum State { Hover, Clicked, Revealed }

public class Cell : MonoBehaviour
{
    private State _state;
    [SerializeField] private GameObject _hoverBorders;
    
    private void OnMouseOver()
    {
        SetState(State.Hover);
    }

    private void OnMouseExit()
    {
    }

    private void SetState(State state)
    {
        _state = state;
        HandleStates();
    }
    
    private void HandleStates()
    {
        switch (_state)
        {
            case State.Hover:
                _hoverBorders.SetActive(true);
                break;
            case State.Clicked:
                break;
            case State.Revealed:
                break;
            default:
                Debug.Log("Default");
        }
    }
}
