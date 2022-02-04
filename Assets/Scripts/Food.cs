using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    private void Start()
    {
        CellsManager.Manager.Foods.Add(this);
    }

    private void OnDestroy()
    {
        CellsManager.Manager.Foods.Remove(this);
    }

}
