using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _width = 9;
    private int _height = 9;
    private int _mines = 10;
    private float _tileSize = 1f;
    
    [SerializeField] private Transform _cell;
    [SerializeField] private Transform _grid;
    
    private void Start()
    {
        CreateGameBoard();
    }

    private void CreateGameBoard()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                Transform cell = Instantiate(_cell, _grid, true);
                float xIndex = col - ((_width - 1) / 2.0f);
                float yIndex = row - ((_height - 1) / 2.0f);
                cell.localPosition = new Vector2(xIndex * _tileSize, yIndex * _tileSize);
                
            }
        }
    }
}
