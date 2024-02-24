using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridObject
{
    public enum Type
    {
        Empty,
        Mine,
        MineNum1,
        MineNum2,
        MineNum3,
        MineNum4,
        MineNum5,
        MineNum6,
        MineNum7,
        MineNum8
    }
    
    private Grid<MapGridObject> _grid;
    private int _x;
    private int _y;
    private Type _type;
    private bool _isRevealed;
    private bool _isFlagged;
    
    public MapGridObject(Grid<MapGridObject> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        _type = Type.Empty;
        _isRevealed = false;
    }

    public Type GetGridType()
    {
        return _type;
    }

    public bool IsRevealed()
    {
        return _isRevealed;     
    }

    public bool IsFlagged()
    {
        return _isFlagged;
    }
}
