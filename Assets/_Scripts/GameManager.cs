using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _width  = 9;
    private int _height  = 9;
    private int _minesAmount = 10;
    private float _tileSize = 1f;
    [SerializeField] private Cell _cell;
    [SerializeField] private GridManger GridManger;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        CreateGameBoard();
        AssignMines();
    }
    
    private void CreateGameBoard()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                Transform cell = Instantiate(_cell.transform, GridManger.transform, true);
                float xIndex = col - ((_width - 1) / 2.0f);
                float yIndex = row - ((_height - 1) / 2.0f);
                cell.localPosition = new Vector2(xIndex * _tileSize, yIndex * _tileSize); ;
            }
        }
    }

    private void AssignMines()
    {
        // HashSet for ensure no duplicate indexes
        List<Cell> cells = GridManger.GetAvailableCells();
        HashSet<int> mineIndexes = new();

        while (mineIndexes.Count < _minesAmount)
        {
            int randomIndex = Random.Range(0, cells.Count);
            mineIndexes.Add(randomIndex);
        }

        foreach (int index in mineIndexes)
        {
            cells[index]._hasMine = true;
        }
    }
}
