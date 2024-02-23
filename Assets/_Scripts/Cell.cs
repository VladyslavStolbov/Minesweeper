using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
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
    
    private Grid<Cell> _grid;
    private int _x;
    private int _y;
    private Type _type;
    
    public Cell(Grid<Cell> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }
}
