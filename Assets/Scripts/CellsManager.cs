using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoBehaviour
{

    public static CellsManager Manager;

    public List<Cell> Cells;
    public List<Food> Foods;

    private void Awake()
    {
        if (!Manager)
            Manager = this;
    }

    public Food GetFood(Transform cell, float distance) 
    {
        foreach(Food food in Foods) 
        {
            if (Vector2.Distance(transform.position, cell.transform.position) < distance)
                return food;
        }
        return null;
    }

}
