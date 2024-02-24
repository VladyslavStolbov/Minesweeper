using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private GridPrefabVisual _gridPrefabVisual;
    private Map _map;

    private void Start()
    {
        _map = new Map();
        _gridPrefabVisual.Setup(_map.GetGrid());
    }
}
