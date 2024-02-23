using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPrefabVisual : MonoBehaviour
{
    public static GridPrefabVisual Instance { get; private set; }

    [SerializeField] private Transform _gridPrefabVisualNode;
    private List<Transform> _visualNodeList;
    private Transform[,] _visualNodeArray;
    private Grid<Cell> _grid;

    private void Awake()
    {
        Instance = this;
        _visualNodeList = new List<Transform>();
    }

    public void Setup(Grid<Cell> grid)
    {
        _grid = grid;
        _visualNodeArray = new Transform[_grid.GetWidth(), _grid.GetHeight()];

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                Vector3 gridPosition = new Vector3(x, y) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * 0.5f;
                Transform visualNode = CreateVisualNode(gridPosition);
                _visualNodeArray[x, y] = visualNode;
                _visualNodeList.Add(visualNode);
            }
        }

        HideNodeVisuals();

        grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
    }
    
    
}
