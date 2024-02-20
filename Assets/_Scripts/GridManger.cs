using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManger : MonoBehaviour
{
    public List<Cell> GetAvailableCells()
    {
        Debug.Log(GetComponentsInChildren<Cell>());
        return GetComponentsInChildren<Cell>().ToList();
    }
}
