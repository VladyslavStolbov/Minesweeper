using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Grid<MapGridObject> _grid;

    public Map()
    {
        _grid = new Grid<MapGridObject>(7, 10, 1f, Vector3.left,
            (Grid<MapGridObject> g, int x, int y) => new MapGridObject(_grid, x, y));
    }

    public Grid<MapGridObject> GetGrid()
    {
        return _grid;
    }
}
