using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int _width { get; private set; } = 9;
    public int _height { get; private set; } = 9;
    private int _minesAmount = 10;
    private float _tileSize = 1f;
    [SerializeField] private Cell _cell;
    [SerializeField] private GameObject _grid;

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

    public List<Cell> GetAvailableCells()
    {
        return _grid.GetComponentsInChildren<Cell>().ToList();
    }
    
    private void CreateGameBoard()
    {
        for (int row = 0; row < _height; row++)
        {
            for (int col = 0; col < _width; col++)
            {
                Transform cell = Instantiate(_cell.transform, _grid.transform, true);
                float xIndex = col - ((_width - 1) / 2.0f);
                float yIndex = row - ((_height - 1) / 2.0f);
                cell.localPosition = new Vector2(xIndex * _tileSize, yIndex * _tileSize); ;
            }
        }
    }

    private void AssignMines()
    {
        // HashSet for ensure no duplicate indexes
        Cell[] cells = _grid.GetComponentsInChildren<Cell>();
        HashSet<int> mineIndexes = new ();

        while (mineIndexes.Count < _minesAmount)
        {
            int randomIndex = Random.Range(0, cells.Length);
            mineIndexes.Add(randomIndex);
        }

        foreach (int index in mineIndexes)
        {
            cells[index]._hasMine = true;
        }
    }
}
