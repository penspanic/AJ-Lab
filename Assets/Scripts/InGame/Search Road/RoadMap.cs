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

public class RoadMap : MonoBehaviour
{
    const int MapColumn = 8;
    const int MapRow = 5;

    GameObject roadPrefab;

    public Road[][] roads
    {
        get;
        private set;
    }

    void Awake()
    {
        //roadPrefab = Resources.Load<GameObject>("Prefab/InGame/Search Road/Road");
        //GameObject map = new GameObject("Map");
        //for (int row = 0; row < MapRow; row++)
        //{
        //    GameObject rowParent = new GameObject("Row" + (row + 1).ToString());
        //    rowParent.transform.SetParent(map.transform);
        //    for (int col = 0; col < MapColumn; col++)
        //    {
        //        GameObject newRoad = Instantiate<GameObject>(roadPrefab);
        //        newRoad.name = "Road" + (row + 1).ToString() + "," + (col + 1).ToString();
        //        newRoad.transform.SetParent(rowParent.transform);
        //        newRoad.transform.position = new Vector2(col * 1 - 3.5f, row * -1 + 2);
        //    }
        //}


        roads = new Road[MapColumn][];

        for(int col = 0;col<MapColumn;col++)
        {
            roads[col] = new Road[MapRow];
            for(int row = 0;row<MapRow;row++)
            {
                roads[col][row] = GameObject.Find(
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
                if (roads[col][row] == target)
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
            top = roads[targetColIndex][targetRowIndex - 1];
        if (targetRowIndex + 1 < MapRow) // bottom
            bottom = roads[targetColIndex][targetRowIndex + 1];
        if (targetColIndex != 0) // left
            left = roads[targetColIndex - 1][targetRowIndex];
        if (targetColIndex + 1 < MapColumn) // right
            right = roads[targetColIndex + 1][targetRowIndex];

        return new NearRoads(top, bottom, left, right);
    }

    public Vector2 GetRoadIndex(Road target)
    {
        Vector2 returnVec = new Vector2();

        for (int row = 0; row < MapRow; row++)
        {
            for (int col = 0; col < MapColumn; col++)
            {
                if (roads[col][row] == target)
                {
                    returnVec.x = col;
                    returnVec.y = row;
                }
            }
        }
        return returnVec;
    }

    public Road GetStoppedRoad()
    {
        int column = Random.Range(0, MapColumn);
    
        Road[] stoppedRoads = roads[column];
        stoppedRoads = System.Array.FindAll<Road>(roads[column], (road) =>
         {
             return !road.isRotating;
         });
        return stoppedRoads[Random.Range(0, stoppedRoads.Length)];
    }

}