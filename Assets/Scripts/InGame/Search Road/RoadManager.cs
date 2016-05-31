using UnityEngine;
using System.Collections.Generic;



public class RoadManager : MonoBehaviour
{
    SearchRoad gameScene;
    RoadMap map;

    List<Road> beAnswerList = new List<Road>();
    List<RoadDirection> moveCommandList = new List<RoadDirection>();

    RoadDirection nextDirection;
    //RoadDirection endPrevAnswerDirection;
    Road start;
    Road end;

    Road road5_10;

    SR_Player player;

    void Awake()
    {
        gameScene = GameObject.FindObjectOfType<SearchRoad>();

        map = Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/InGame/Search Road/Map" + StageManager.instance.currStage.ToString()))
            .GetComponent<RoadMap>();

        start = GameObject.Find("Start").GetComponent<Road>();
        end = GameObject.Find("End").GetComponent<Road>();
        road5_10 = GameObject.Find("Road5,8").GetComponent<Road>();

        player = GameObject.FindObjectOfType<SR_Player>();
    }

    public void CheckClear() // 길이 바뀌었을 때 체크
    {
        Road newRoad;
        bool isCleared = false;
        beAnswerList.Clear();
        beAnswerList.Add(start);

        nextDirection = RoadDirection.Bottom;
        moveCommandList.Clear();
        moveCommandList.Add(nextDirection);
        while(true)
        {
            newRoad = GetConnectedRoad(beAnswerList[beAnswerList.Count - 1], nextDirection);
            if(newRoad != null)
            {
                if(newRoad == road5_10)
                {
                    if (road5_10.HasDirection(RoadDirection.Bottom))
                    {
                        isCleared = true;
                        break;
                    }
                }
                beAnswerList.Add(newRoad);
                nextDirection = GetAnotherDirection(newRoad, GetOppositeDirection(nextDirection));
                moveCommandList.Add(nextDirection);
            }
            else
            {
                break;
            }
        }

        if (isCleared)
        {
            moveCommandList.Add(RoadDirection.Bottom);
            player.Move(moveCommandList);
        }
    }

    Road GetConnectedRoad(Road target,RoadDirection dir)
    {
        NearRoads nearRoads = map.GetNearRoad(target);
        
        if(target == start)
        {
            Road road1_1 = GameObject.Find("Road1,1").GetComponent<Road>();
            if (road1_1.HasDirection(GetOppositeDirection(dir)))
                return road1_1;
            else
                return null;
        }
        switch(dir)
        {
            case RoadDirection.Bottom:
                if(nearRoads.bottomRoad != null)
                {
                    if (nearRoads.bottomRoad.HasDirection(RoadDirection.Top))
                        return nearRoads.bottomRoad;
                }
                break;
            case RoadDirection.Top:
                if (nearRoads.topRoad != null)
                {
                    if (nearRoads.topRoad.HasDirection(RoadDirection.Bottom))
                        return nearRoads.topRoad;
                }
                break;
            case RoadDirection.Left:
                if(nearRoads.leftRoad != null)
                {
                    if (nearRoads.leftRoad.HasDirection(RoadDirection.Right))
                        return nearRoads.leftRoad;
                }
                break;
            case RoadDirection.Right:
                if(nearRoads.rightRoad!= null)
                {
                    if (nearRoads.rightRoad.HasDirection(RoadDirection.Left))
                        return nearRoads.rightRoad;
                }
                break;
        }
        return null;

    }

    RoadDirection GetOppositeDirection(RoadDirection dir)
    {
        switch(dir)
        {
            case RoadDirection.Bottom:
                return RoadDirection.Top;
            case RoadDirection.Top:
                return RoadDirection.Bottom;
            case RoadDirection.Left:
                return RoadDirection.Right;
            case RoadDirection.Right:
                return RoadDirection.Left;
        }
        throw new UnityException("There's no opposite direction!");
    }

    RoadDirection GetAnotherDirection(Road target, RoadDirection dir)
    {
        RoadDirection[] dirs = target.GetDirection();
        
        if(target.type == RoadType.Crossed)
        {
            return GetOppositeDirection(dir);
        }
        int index = dirs[0] == dir ? 1 : 0;
        return dirs[index];
    }
    //RoadDirection GetConnectedDirection(Road target, Road operand)
    //{
    //    RoadDirection[] targetDirections = target.GetDirection();
    //    RoadDirection[] operandDirections = operand.GetDirection();

    //    for(int i = 0;i<targetDirections.Length;i++)
    //    {
    //        for(int j = 0;j<)
    //    }
    //}
}