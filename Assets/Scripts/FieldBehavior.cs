using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBehavior : MonoBehaviour
{

    [SerializeField] private GameObject _cell;
    [SerializeField] private GameObject _food;
    [SerializeField] private Vector2 _areaSpawn;

    private void Awake()
    {
        //StartField();
    }

    private void StartField() 
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject b = Instantiate(_cell, new Vector3(Random.Range(-_areaSpawn.x, _areaSpawn.x), Random.Range(-_areaSpawn.y, _areaSpawn.y), 0), Quaternion.identity);
            b.name = "bacterium";
            //b.GetComponent<AI>().Init(genome);
        }

        for (int i = 0; i < 1000; i++)
        {
            GameObject food = Instantiate(_food, new Vector3(Random.Range(-_areaSpawn.x, _areaSpawn.x), Random.Range(-_areaSpawn.y, _areaSpawn.y), 0), Quaternion.identity);
            food.name = "food";
        }
    }

}
