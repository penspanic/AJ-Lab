using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public struct NearRoads
{
    public NearRoads(Road top, Road bottom, Road left, Road right)
    {
        topRoad = top;
        bottomRoad = bottom;
        leftRoad = left;
        rightRoad = right;
    }

    public readonly Road topRoad;
    public readonly Road bottomRoad;
    public readonly Road leftRoad;
    public readonly Road rightRoad;
}

public class PlumberMap : MonoBehaviour
{
    const int MapColumn = 10;
    const int MapRow = 5;

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

        roads = new Road[MapColumn, MapRow];

        for (int row = 0; row < MapRow; row++)
        {
            for (int col = 0; col < MapColumn; col++)
            {
                roads[col, row] = GameObject.Find(
                    "Road" + (row + 1).ToString() + "," + (col + 1).ToString()).GetComponent<Road>();
            }
        }
    }

    void Update()
    {

    }

    public NearRoads GetNearRoad(Road target) // 인접한 길 리턴
    {
        int targetRowIndex = 0;
        int targetColIndex = 0;

        for (int row = 0; row < MapRow; row++)
        {
            for (int col = 0; col < MapColumn; col++)
            {
                if (roads[col, row] == target)
                {
                    targetRowIndex = row;
                    targetColIndex = col;
                }
            }
        }

        Road top = null;
        Road bottom = null;
        Road left = null;
        Road right = null;

        if (targetRowIndex != 0) // top
            top = roads[targetColIndex, targetRowIndex - 1];
        if (targetRowIndex + 1 < MapRow) // bottom
            bottom = roads[targetColIndex, targetRowIndex + 1];
        if (targetColIndex != 0) // left
            left = roads[targetColIndex - 1, targetRowIndex];
        if (targetColIndex + 1 < MapColumn) // right
            right = roads[targetColIndex + 1, targetRowIndex];

        return new NearRoads(top, bottom, left, right);
    }

    public Vector2 GetRoadIndex(Road target)
    {
        Vector2 returnVec = new Vector2();

        for (int row = 0; row < MapRow; row++)
        {
            for (int col = 0; col < MapColumn; col++)
            {
                if (roads[col, row] == target)
                {
                    returnVec.x = col;
                    returnVec.y = row;
                }
            }
        }
        return returnVec;
    }

}