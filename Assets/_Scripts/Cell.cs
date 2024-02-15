using UnityEngine;

public enum State { Unrevealed, Hover, Revealed, Flagged }

public class Cell : MonoBehaviour
{
    
    private State _state;
    private bool _hasMine = false;
    [SerializeField] private GameObject _unrevealed;
    [SerializeField] private GameObject _hoverBorders;
    [SerializeField] private GameObject _flag;
    [SerializeField] private GameObject _mine;
    
    private void OnMouseOver()
    {
        HandleMouseClicks(); 
    }

    private void OnMouseExit()
    {
        if (_state == State.Revealed || _state == State.Flagged) return;
        SetState(State.Unrevealed);
    }
    private void HandleMouseClicks()
    {
        if (Input.GetMouseButton(1))
        {
            SetState(State.Flagged);
            return;
        }
        if (Input.GetMouseButton(0))
        {
            SetState(State.Revealed);
            return;
        }
        if (_state == State.Revealed || _state == State.Flagged)
        {
            return;
        }
        SetState(State.Hover);
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
            case State.Unrevealed:
                _unrevealed.SetActive(true);
                _hoverBorders.SetActive(false);
                
                break;
            case State.Hover:
                _hoverBorders.SetActive(true);

                break;
            case State.Flagged:
                _hoverBorders.SetActive(false);
                _flag.SetActive(true);
                
                break;
            case State.Revealed:
                _unrevealed.SetActive(false);
                _hoverBorders.SetActive(false);
                CheckForMine();
                
                break;
            default:
                _unrevealed.SetActive(true);
                break;
        }
    }

    private void CheckForMine()
    {
        if (_hasMine)
        {
            _mine.SetActive(true);
        }
    }
}
