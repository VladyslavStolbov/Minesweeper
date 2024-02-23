using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private Grid<Cell> _grid;

    public Map()
    {
        _grid = new Grid<Cell>(10, 10, 10f, Vector3.zero, (Grid<Cell> g, int x, int y) => new Cell(_grid, x, y));
    }        
}
