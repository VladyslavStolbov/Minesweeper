using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPrefabVisual : MonoBehaviour
{
    public static GridPrefabVisual Instance { get; private set; }

    [SerializeField] private Transform _gridPrefabVisualNode;
    [SerializeField] private Sprite _flagSprite;
    [SerializeField] private Sprite _mineSprite;

    private GameObject _gridObject;
    private List<Transform> _visualNodeList;
    private Transform[,] _visualNodeArray;
    private Grid<MapGridObject> _grid;
    private bool _revealEntireMap;

    private void Awake()
    {
        Instance = this;
        _visualNodeList = new List<Transform>();
        _gridObject = new GameObject("Grid");
    }

    public void Setup(Grid<MapGridObject> grid)
    {
        _grid = grid;
        _visualNodeArray = new Transform[_grid.GetWidth(), _grid.GetHeight()];
        
        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                Vector3 gridPosition =
                    new Vector3(x, y) * _grid.GetCellSize() + Vector3.one * _grid.GetCellSize() * 0.5f;
                Transform visualNode = CreateVisualNode(gridPosition);
                _visualNodeArray[x, y] = visualNode;
                _visualNodeList.Add(visualNode);
            }
        }

        _gridObject.transform.position = new Vector3(-3.5f, -5f, 0);
    }

    private void UpdateVisual(Grid<MapGridObject> grid)
    {
        HideNodeVisuals();

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                MapGridObject gridObject = grid.GetGridObject(x, y);

                Transform visualNode = _visualNodeArray[x, y];
                visualNode.gameObject.SetActive(true);
                SetupVisualNode(visualNode, gridObject);
            }
        }   

    }

    private void SetupVisualNode(Transform visualNodeTransform, MapGridObject mapGridObject)
    {
        SpriteRenderer iconSpriteRenderer = visualNodeTransform.Find("iconSprite").GetComponent<SpriteRenderer>();
        TextMeshPro indicatorText = visualNodeTransform.Find("mineIndicatorText").GetComponent<TextMeshPro>();
        Transform unrevealedTransform = visualNodeTransform.Find("Unrevealed"); 

        if (mapGridObject.IsRevealed() || _revealEntireMap) {
            // Node is revealed
            unrevealedTransform.gameObject.SetActive(false);

            switch (mapGridObject.GetGridType()) {
            default:
            case MapGridObject.Type.Empty:
                indicatorText.gameObject.SetActive(false);
                iconSpriteRenderer.gameObject.SetActive(false);
                break;
            case MapGridObject.Type.Mine:
                indicatorText.gameObject.SetActive(false);
                iconSpriteRenderer.gameObject.SetActive(true);
                iconSpriteRenderer.sprite = _mineSprite;
                break;
            case MapGridObject.Type.MineNum1:
            case MapGridObject.Type.MineNum2:
            case MapGridObject.Type.MineNum3:
            case MapGridObject.Type.MineNum4:
            case MapGridObject.Type.MineNum5:
            case MapGridObject.Type.MineNum6:
            case MapGridObject.Type.MineNum7:
            case MapGridObject.Type.MineNum8:
                indicatorText.gameObject.SetActive(true);
                iconSpriteRenderer.gameObject.SetActive(false);
                switch (mapGridObject.GetGridType()) {
                default:
                case MapGridObject.Type.MineNum1:
                case MapGridObject.Type.MineNum2:
                case MapGridObject.Type.MineNum3:
                case MapGridObject.Type.MineNum4:
                case MapGridObject.Type.MineNum5:
                case MapGridObject.Type.MineNum6:
                case MapGridObject.Type.MineNum7:
                case MapGridObject.Type.MineNum8:
                    break;
                }
                break;
            }
        } else {
            // Node is hidden
            unrevealedTransform.gameObject.SetActive(true);

            if (mapGridObject.IsFlagged()) {
                iconSpriteRenderer.gameObject.SetActive(true);
                iconSpriteRenderer.sprite = _flagSprite;
            } else {
                iconSpriteRenderer.gameObject.SetActive(false);
            }
        }
    }

    private void HideNodeVisuals()
    {
        foreach (Transform visualNodeTransform in _visualNodeList)
        {
            visualNodeTransform.gameObject.SetActive(false);
        }
    }

    private Transform CreateVisualNode(Vector3 position)
    {
        Transform visualNodeTransform =
            Instantiate(_gridPrefabVisualNode, position, Quaternion.identity, _gridObject.transform);
        return visualNodeTransform;
    }
    
    
}
