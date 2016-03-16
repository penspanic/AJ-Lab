using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlumberMap : MonoBehaviour
{
    const int mapColumn = 10;
    const int mapRow = 10;

    GameObject pipePrefab;

    public Road[,] roads
    {
        get;
        private set;
    }

    void Awake()
    {
        //pipePrefab = Resources.Load<GameObject>("Prefab/InGame/Plumber/Pipe");
        //GameObject map = new GameObject("Map");

        //for (int row = 0; row < 5; row++)
        //{
        //    GameObject rowParent = new GameObject("Row" + (row + 1).ToString());
        //    rowParent.transform.SetParent(map.transform);
        //    for (int col = 0; col < 10; col++)
        //    {
        //        GameObject newPipe = Instantiate<GameObject>(pipePrefab);
        //        newPipe.name = "Road" + (row + 1).ToString() + "," + (col + 1).ToString();
        //        newPipe.transform.SetParent(rowParent.transform);
        //        newPipe.transform.position = new Vector2(col * 1 - 4.5f, row * -1 + 2);
        //    }
        //}

        roads = new Road[5, 10];

        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                roads[row, col] = GameObject.Find(
                    "Road" + (row + 1).ToString() + "," + (col + 1).ToString()).GetComponent<Road>();
            }
        }
    }

    void Update()
    {

    }
}